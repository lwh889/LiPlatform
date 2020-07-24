
alter procedure sp_DeployReport(
@reportId int
)
as
begin
	declare @menuCode int
	declare @dataBaseName nvarchar(30)

	select top 1 @dataBaseName=dataBaseName,@menuCode = menuCode from LiReport where id = @reportId
	
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
	set @menuId = ' + @menuCode + '
	set @menuParentId = 1
	
	select @menuCode = Code from LiManageMeum where Id = @menuId
	
	while(@menuId > 1)
	begin
		insert #LiAdminMeum ([ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder])
		select [ID],[Code],[Name],[isGroup],[isSystem],[GroupId],[ParentID],[imageIndex],[iOrder] from LiManageMeum where id=@menuId 
		 
		select @menuId = ParentID from LiManageMeum where id=@menuId 
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

	select * from LiDATA_999_2020.dbo.LiField
select * from LiDATA_999_2020.dbo.LiQuery
select * from LiDATA_999_2020.dbo.LiQueryScheme
select * from LiReportField
--select * from UFDATA_999_2014.dbo.Inventory

declare @entityKey nvarchar(30)
set @entityKey = 'LiReportInventory'

--删除
delete from LiDATA_999_2020.dbo.LiField
where querySchemeId in (select id from LiDATA_999_2020.dbo.LiQueryScheme where entityKey = @entityKey) and fieldName not in (select columnName from LiReportField)

--更新
update LiDATA_999_2020.dbo.LiField set name = A.columnCaption,sColumnControlType=A.controlType,dictInfoType=A.dictInfoType,basicInfoKey=A.basicInfoKey,gridlookUpEditShowModelJson=A.gridlookUpEditShowModelJson 
from (select * from LiReportField where reportId = 2 and columnName in (select code from LiDATA_999_2020.dbo.LiField where querySchemeId in (select id from LiDATA_999_2020.dbo.LiQueryScheme where entityKey = @entityKey)) ) A 
where A.columnName = LiDATA_999_2020.dbo.LiField.code 

declare @querySchemeId int

	declare licursorQueryScheme cursor for select id from LiDATA_999_2020.dbo.LiQueryScheme where entityKey = @entityKey
	open licursorQueryScheme
	fetch next from licursorQueryScheme into @querySchemeId
	while(@@FETCH_STATUS = 0)
	begin
	
		--插入
		insert into LiDATA_999_2020.dbo.LiField 
		(querySchemeId,code,name,fieldName,columnFieldName,iColumnWidth,bColumnDisplay,bQuery,bRange,sColumnControlType,sRefTypeCode,sJudgeSymbol,dictInfoType,basicInfoKey,gridlookUpEditShowModelJson,iColumnIndex) 
		select @querySchemeId,columnName,columnCaption,columnName,columnName,iColumnIndex,0,0,0,controlType,'',5,dictInfoType,basicInfoKey,gridlookUpEditShowModelJson,iColumnIndex 
		from LiReportField
		where reportId = 2 and columnName not in (select code from LiDATA_999_2020.dbo.LiField where querySchemeId = @querySchemeId)

	end
	close licursorQueryScheme
	Deallocate licursorQueryScheme
end