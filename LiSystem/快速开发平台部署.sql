
alter procedure sp_DeployLiSystem(
@formId int
)
as
begin


	declare @dataBaseName nvarchar(30)
	declare @formType nvarchar(30)
		
	select @dataBaseName = A3.systemDataBaseName,@formType = A1.formType
	from LiForm A1 
	left join LiPanel A2 on A1.id = A2.formModelId 
	left join LiSystemInfo A3 on A3.systemCode = A1.systemCode
	where A1.id = @formId
	
	if(@formType != '下推')
	begin
		exec sp_CreateTable @formId
	end

	exec('
	
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

	declare @menuCode nvarchar(50)
	declare @menuParentId int
	declare @menuId int
	set @menuId = 0
	set @menuParentId = 1

	select @menuCode = [name] from LiForm where id = ' + @formId + '

	select @menuId = Id from LiManageMeum where Code = @menuCode
	
	insert #LiAdminMeum ([ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder])
	select [ID],[Code] + ''List'',[Name] + ''列表'',[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder] from LiManageMeum where id=@menuId 

	
	if(''' + @formType + ''' != ''下推'')
	begin
		while(@menuId > 1)
		begin
			insert #LiAdminMeum ([ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder])
			select [ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder] from LiManageMeum where id=@menuId 
		 
			select @menuId = ParentID from LiManageMeum where id=@menuId 
		end
	end
	
	declare licursorMenu cursor for select ID,Code from #LiAdminMeum order by ParentID
	open licursorMenu
	fetch next from licursorMenu into @menuId,@menuCode
	while(@@FETCH_STATUS = 0)
	begin
	
		 if not exists( select 1 from ' +@dataBaseName + '.dbo.LiAdminMeum where Code = @menuCode )
		 begin
			insert ' +@dataBaseName + '.dbo.LiAdminMeum ([Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder])
			select [Code],[Name],[isGroup],[isSystem],[GroupId],@menuParentId,[imageIndex],[iOrder] from #LiAdminMeum where Code=@menuCode 
			
			 if exists( select 1 from #LiAdminMeum where [isGroup] = 1 and Code = @menuCode )
			 begin
				set @menuParentId = @@identity
			 end

		 end
		 else
		 begin
			update ' +@dataBaseName + '.dbo.LiAdminMeum set [Code] = A.[Code],[Name] = A.[Name],[isGroup] = A.[isGroup],[isSystem] = A.[isSystem],[GroupId] = A.[GroupId],[imageIndex] = A.[imageIndex],[iOrder] = A.[iOrder]
			from (select [Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder] from #LiAdminMeum where Code=@menuCode ) A where A.[Code] = ' +@dataBaseName + '.dbo.LiAdminMeum.[Code]
			
			if exists( select 1 from #LiAdminMeum where [isGroup] = 1 and Code = @menuCode )
			begin
			select @menuParentId = id from ' +@dataBaseName + '.dbo.LiAdminMeum where Code = @menuCode
			end
		 end
		
		fetch next from licursorMenu into @menuId,@menuCode

	end
	close licursorMenu
	Deallocate licursorMenu
	
	')

end