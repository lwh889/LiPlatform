
alter procedure sp_DeployU8(
@formId int
)
as
begin

	declare @menuSql nvarchar(max)
	set @menuSql = ''
	declare @dataBaseName nvarchar(30)
	declare @systemU8MenuId nvarchar(30)
	declare @systemTitle nvarchar(30)
	declare @systemCode nvarchar(30)
	declare @formType nvarchar(30)
		
	select top 1 @dataBaseName = A3.systemDataBaseName,@systemU8MenuId=A3.systemU8MenuId,@systemTitle=A3.systemTitle,@systemCode=A3.systemCode,@formType = A1.formType
	from LiForm A1 
	left join LiPanel A2 on A1.id = A2.formModelId 
	left join LiSystemInfo A3 on A3.systemCode = A1.systemCode
	where A1.id = @formId

	
	if(@formType != '下推')
	begin
		exec sp_CreateTable @formId
	end

	set @menuSql = '
	declare @menuCode nvarchar(50)
	declare @menuName nvarchar(50)
	declare @menuParentId nvarchar(50)
	declare @menuId int
	declare @iOrder int
	set @menuId = 0
	set @menuParentId = 1

	--根节点
	IF NOT EXISTS(SELECT * FROM UFSystem.dbo.UA_Menu WHERE cMenu_Id = ''' + @systemU8MenuId + ''')
		INSERT INTO UFSystem.dbo.UA_Menu (cMenu_Id, cMenu_Name, cMenu_Eng, cSub_Id, IGrade, cSupMenu_Id, bEndGrade, cAuth_Id, iOrder, iImgIndex, Paramters, Depends, Flag)
		select systemU8MenuId,systemTitle, Null, null , 0, ''#1'', 0, NULL, -9999, 0, null, null, null from LiSystemInfo where systemCode = ''' + @systemCode + '''

	IF NOT EXISTS(SELECT * FROM UFSystem.dbo.uA_auth WHERE cAuth_Id = ''' + @systemU8MenuId + ''')
		INSERT INTO UFSystem.dbo.uA_auth(cAuth_Id,cAuth_Name,cSub_Id,iGrade,cSupAuth_Id,bEndGrade,iOrder,cAcc_Id,cAuthType,cAllSupAuths)
		select systemU8MenuId,systemTitle, ''DP'', 2 , systemU8MenuId, 0,9999,NULL,NULL,NULL from LiSystemInfo where systemCode = ''' + @systemCode + '''
	--菜单
	create table #LiAdminMeum(
		ID int not null,
		Code nvarchar(30) not null,
		Name nvarchar(30) not null,
		isGroup bit not null,
		isSystem bit default 0,
		GroupId int not null,
		ParentID int not null,
		imageIndex int not null,
		iOrder int not null
	)

	select @menuCode = [name] from LiForm where id = ' + cast(@formId as nvarchar(9)) + '
	select @menuId = Id from LiManageMeum where Code = @menuCode
	
	insert #LiAdminMeum ([ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder])
	select [ID],[Code] + ''List'',[Name] + ''列表'',[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder]+1 from LiManageMeum where id=@menuId 

	
	if(''' + @formType + ''' != ''下推'')
	begin
		while(@menuId > 1)
		begin
			insert #LiAdminMeum ([ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder])
			select [ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder] from LiManageMeum where id=@menuId 
		 
			select @menuId = ParentID from LiManageMeum where id=@menuId 
		end
	end
	'
	
	--组菜单
	set @menuSql = @menuSql + '
	set @menuParentId = ''' + @systemU8MenuId + '''

	declare licursorMenu cursor for select ID,Code,Name,iOrder from #LiAdminMeum where isGroup = 1 order by ParentID
	open licursorMenu
	fetch next from licursorMenu into @menuId,@menuCode,@menuName,@iOrder
	while(@@FETCH_STATUS = 0)
	begin
	
		IF NOT EXISTS(SELECT * FROM UFSystem.dbo.UA_Menu WHERE cMenu_Id = @menuCode)
			INSERT INTO UFSystem.dbo.UA_Menu (cMenu_Id, cMenu_Name, cMenu_Eng, cSub_Id, IGrade, cSupMenu_Id, bEndGrade, cAuth_Id, iOrder, iImgIndex, Paramters, Depends, Flag)
			VALUES (@menuCode, @menuName, Null, null , 0, @menuParentId, 0, NULL, @iOrder, 1,null	, null, null)
		ELSE
		UPDATE UFSystem.dbo.UA_Menu SET cAuth_Id=NULL  WHERE cMenu_Id = @menuCode
		
		IF NOT EXISTS(SELECT * FROM UFSystem.dbo.uA_auth WHERE cAuth_Id = @menuCode)
			INSERT INTO UFSystem.dbo.uA_auth(cAuth_Id,cAuth_Name,cSub_Id,iGrade,cSupAuth_Id,bEndGrade,iOrder,cAcc_Id,cAuthType,cAllSupAuths)
			VALUES(@menuCode,@menuName,''DP'',3,@menuParentId,0,@iOrder,NULL,NULL,NULL)
	
		set @menuParentId = @menuCode

		fetch next from licursorMenu into @menuId,@menuCode,@menuName,@iOrder

	end
	close licursorMenu
	Deallocate licursorMenu
	'
	---节点菜单
	set @menuSql = @menuSql + '
	declare licursorMenu cursor for select ID,Code,Name,iOrder from #LiAdminMeum where isGroup = 0 order by iOrder
	open licursorMenu
	fetch next from licursorMenu into @menuId,@menuCode,@menuName,@iOrder
	while(@@FETCH_STATUS = 0)
	begin

		IF NOT EXISTS(SELECT * FROM UFSystem..ua_idt WHERE id=@menuCode)
		BEGIN
			INSERT INTO UFSystem..ua_idt(id,[assembly],catalogtype,type) 
			VALUES(@menuCode,''LiSystemPlugin.ClsIntface'',0,0)
		END
		

		IF NOT EXISTS(SELECT * FROM UFSystem.dbo.UA_Menu WHERE CMenu_Id = @menuCode)
		INSERT INTO UFSystem.dbo.UA_Menu (CMenu_Id, cMenu_Name, cMenu_Eng, cSub_Id, IGrade, cSupMenu_Id, bEndGrade, cAuth_Id, iOrder, iImgIndex, Paramters, Depends, Flag)
						VALUES (@menuCode, @menuName, NULL,NULL , 2, @menuParentId, 1, @menuParentId, @iOrder, 4, null, null, null)
		
		IF NOT EXISTS(SELECT * FROM UFSystem.dbo.uA_auth WHERE cAuth_Id = @menuCode)
		INSERT INTO UFSystem.dbo.uA_auth (cAuth_Id, cAuth_Name, cSub_Id, iGrade, cSupAuth_Id, bEndGrade, iOrder, cAcc_Id, cAuthType)
						  VALUES (@menuCode, @menuName,''DP'', 4,  @menuParentId,0,@iOrder,NULL,null)
		   update UFSystem.dbo.uA_auth set cAllSupauths= @menuParentId WHERE cAuth_Id = @menuCode
		
		fetch next from licursorMenu into @menuId,@menuCode,@menuName,@iOrder

	end
	close licursorMenu
	Deallocate licursorMenu
	
	'
	exec(@menuSql)
	print len(@menuSql)
	print @menuSql
end