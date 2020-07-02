alter procedure sp_QueryList(
@childTableName nvarchar(50),
@entityKey nvarchar(50),
@whereSql nvarchar(max),
@orderBySql nvarchar(500),
@rangeSql nvarchar(500),
@systemCode nvarchar(10)
)
as
--ROW_NUMBER() over (order by cinvcode  ) as iRow,
begin

if(@rangeSql is null or @rangeSql = '')
BEGIN
	set @rangeSql = ' AND iPageRow >= 0 AND iPageRow<=1000 '
END

declare @querySql nvarchar(max)
set @querySql = ''
declare @fieldSql nvarchar(max)
set @fieldSql = ''
declare @leftjoinSql nvarchar(max)
set @leftjoinSql = ''

declare @tableId int
declare @primaryKeyName nvarchar(50)
declare @foreignKeyName nvarchar(50)

declare @parentDataBaseName nvarchar(50)--数据库名称
declare @dataBaseName nvarchar(50)--数据库名称
declare @entityType nvarchar(50) --实体类型，单据？档案？
declare @entityOrder nvarchar(20)	--单据的主，次
declare @entityColumnName nvarchar(50)	--子表在主表模型的名称，不能有重复的名字
declare @parentTableName nvarchar(50)	--表名
declare @parentAttrTableName nvarchar(50)	--表名
declare @tableName nvarchar(50)	--表名
declare @tableAliasName nvarchar(50)	--表别名
declare @keyName nvarchar(50)	--主键名

--主表信息
SELECT  @tableId = id
	  ,@dataBaseName = ISNULL([dataBaseName], 'LiSystem')
      ,@entityType = [entityType]
      ,@entityOrder = [entityOrder]
      ,@entityColumnName = [entityColumnName]
      ,@tableName = [tableName]
	  ,@parentDataBaseName = dataBaseName
	  ,@parentTableName = [tableName]
      ,@tableAliasName = [tableAliasName]
      ,@keyName = [keyName]
  FROM [TableInfo]
  WHERE [entityKey] = @entityKey
  AND [entityOrder] = 'master'
  AND systemCode = @systemCode

if(@orderBySql = '')
BEGIN
 set @orderBySql = 'Li' + @tableName + '_' + @keyName + ' desc '
END

--父表关联
set @leftjoinSql = @leftjoinSql + @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName 


declare @columnName nvarchar(50)
declare @columnType nvarchar(50)
declare @controlType nvarchar(50)
declare @basicInfoShowFieldName nvarchar(50)
declare @basicInfoRelationFieldName nvarchar(50)
declare @basicInfoKeyFieldName nvarchar(50)
declare @extendTableKeyFieldName nvarchar(50)
declare @extendRelationTableKeyFieldName nvarchar(50)

declare liCursorColumn cursor for select [columnName],[columnType],[controlType] from ColumnInfo CI WHERE CI.fid = @tableId and CI.controlType not in ('GridLookUpEditRefAssist','Collection') and ISNULL(CI.bExtendField,0) <> 1
open liCursorColumn
fetch next from liCursorColumn into @columnName,@columnType,@controlType
while(@@FETCH_STATUS = 0)
begin

	if(@controlType = 'GridLookUpEditComboBox' or @controlType = 'StatusEdit' )
	begin
		set @fieldSql = @fieldSql + ' LiDict_' + @tableName + '_' + @columnName + '.dictCode Li' + @tableName + '_' + @columnName +  ','
		set @fieldSql = @fieldSql + ' LiDict_' + @tableName + '_' + @columnName + '.dictCode Li' + @tableName + '_' + @columnName +  '_Code,'
		set @fieldSql = @fieldSql + ' LiDict_' + @tableName + '_' + @columnName + '.dictName Li' + @tableName + '_' + @columnName +  '_Name,'
	end
	else
	begin
		set @fieldSql = @fieldSql + ' Li' + @tableName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
	end

	fetch next from liCursorColumn into @columnName,@columnType,@controlType
end
close liCursorColumn
Deallocate liCursorColumn


--辅助属性
declare liCursorColumn cursor for 
	select [columnName],[columnType],basicInfoShowFieldName,TI.dataBaseName,TI.tableName,CI.basicInfoRelationFieldName,basicInfoKeyFieldName
	from ColumnInfo CI
	left join TableInfo TI on CI.basicInfoType = TI.entityKey 
	WHERE CI.fid = @tableId and controlType in ('GridLookUpEditRefAssist')
open liCursorColumn
fetch next from liCursorColumn into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName,@basicInfoRelationFieldName,@basicInfoKeyFieldName
while(@@FETCH_STATUS = 0)
begin
--Li表名_字段名(档案里的显示字段).列名(档案里的显示字段) Li表名_字段名(档案里的显示字段)
	set @fieldSql = @fieldSql + ' Li' + @tableName + '_' + @columnName + '.' + @basicInfoShowFieldName + ' Li' + @parentTableName + '_' + @columnName +  ','
	--父表辅助关联
	set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + '_' + @columnName + ' ON'+ ' Li' + @tableName + '_' + @columnName + '.' + @basicInfoKeyFieldName + ' = Li' + @parentTableName + '.' + @basicInfoRelationFieldName

	fetch next from liCursorColumn into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName,@basicInfoRelationFieldName,@basicInfoKeyFieldName
end
close liCursorColumn
Deallocate liCursorColumn



--字典属性
declare liCursorColumnDict cursor for 
	select [columnName],[columnType],basicInfoShowFieldName,TI.dataBaseName,TI.tableName
		from ColumnInfo CI
		left join TableInfo TI on CI.dictInfoType = TI.entityKey 
		WHERE CI.fid = @tableId and controlType in ('GridLookUpEditComboBox','StatusEdit')
open liCursorColumnDict
fetch next from liCursorColumnDict into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName
while(@@FETCH_STATUS = 0)
begin
	set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @parentDataBaseName + '.dbo.LiDict LiDict_' + @parentTableName + '_' + @columnName + ' ON'+ ' LiDict_' + @parentTableName + '_' + @columnName + '.dictCode = Li' + @parentTableName + '.' + @columnName

	fetch next from liCursorColumnDict into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName
end
close liCursorColumnDict
Deallocate liCursorColumnDict

--扩展字段
declare liCursorColumnExtend cursor for 
	select [columnName],[columnType],TI.dataBaseName,CI.extendTableName,CI.extendTableKeyFieldName,CI.extendRelationTableKeyFieldName
		from ColumnInfo CI
		left join TableInfo TI on CI.fid = TI.id 
		WHERE CI.fid = @tableId and ISNULL(bExtendField,0) = 1
open liCursorColumnExtend
fetch next from liCursorColumnExtend into @columnName,@columnType,@dataBaseName,@tableName,@extendTableKeyFieldName,@extendRelationTableKeyFieldName
while(@@FETCH_STATUS = 0)
begin
	set @fieldSql = @fieldSql + ' Li' + @tableName + '_' + @columnName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
	set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + '_' + @columnName + ' ON'+ ' Li' + @tableName + '_' + @columnName + '.' + @extendTableKeyFieldName + ' = Li' + @parentTableName + '.' + @extendRelationTableKeyFieldName

	fetch next from liCursorColumnExtend into @columnName,@columnType,@dataBaseName,@tableName,@extendTableKeyFieldName,@extendRelationTableKeyFieldName
end
close liCursorColumnExtend
Deallocate liCursorColumnExtend


if(ISNULL(@childTableName, '') <> '')
BEGIN
	declare liCursorTable cursor for 
	--子表信息
	SELECT id
		  ,ISNULL([dataBaseName], 'LiSystem')
		  , [entityType]
		  , [entityOrder]
		  ,[entityColumnName]
		  , [tableName]
		  ,dataBaseName
		  , [tableAliasName]
		  ,[keyName]
	  FROM [TableInfo]
	  WHERE [entityKey] = @entityKey
	  AND [tableName] = @childTableName
	  AND [entityOrder] = 'slave'
	  AND systemCode = @systemCode
	open liCursorTable
	fetch next from liCursorTable into @tableId,@dataBaseName,@entityType,@entityOrder,@entityColumnName,@tableName,@parentDataBaseName,@tableAliasName,@keyName
	while(@@FETCH_STATUS = 0)
	begin
	  
		set @parentAttrTableName = @tableName
	  		--子表关联
		select @primaryKeyName = primaryKeyName, @foreignKeyName = [columnName] from [ColumnInfo] where foreignKey = 1 and primaryKeyTableName = @parentTableName and fid = @tableId
		
		set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' + @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + ' ON'+ ' Li' + @tableName + '.' + @foreignKeyName + ' = Li' + @parentTableName + '.' + @primaryKeyName
		
		---如果没查询出来，controlType可能为NULL
		declare liCursorColumn cursor for select [columnName],[columnType] from ColumnInfo CI WHERE CI.fid = @tableId and controlType not in ('GridLookUpEditRefAssist','Collection') and ISNULL(CI.bExtendField,0) <> 1
		open liCursorColumn
		fetch next from liCursorColumn into @columnName,@columnType
			while(@@FETCH_STATUS = 0)
			begin
		
				if(@controlType = 'GridLookUpEditComboBox' or @controlType = 'StatusEdit' )
				begin
					set @fieldSql = @fieldSql + ' LiDict_' + @parentAttrTableName + '_' + @columnName + '.dictCode Li' + @parentAttrTableName + '_' + @columnName +  ','
					set @fieldSql = @fieldSql + ' LiDict_' + @parentAttrTableName + '_' + @columnName + '.dictCode Li' + @parentAttrTableName + '_' + @columnName +  '_Code,'
					set @fieldSql = @fieldSql + ' LiDict_' + @parentAttrTableName + '_' + @columnName + '.dictName Li' + @parentAttrTableName + '_' + @columnName +  '_Name,'
				end
				else
				begin
					set @fieldSql = @fieldSql + ' Li' + @tableName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
				end

				fetch next from liCursorColumn into @columnName,@columnType
			end
		close liCursorColumn
		Deallocate liCursorColumn


		--辅助属性
		declare liCursorColumnAttr cursor for 
			select [columnName],[columnType],basicInfoShowFieldName,TI.dataBaseName,TI.tableName,CI.basicInfoRelationFieldName,basicInfoKeyFieldName
			from ColumnInfo CI
			left join TableInfo TI on CI.basicInfoType = TI.entityKey 
			WHERE CI.fid = @tableId and controlType in ('GridLookUpEditRefAssist')
		open liCursorColumnAttr
		fetch next from liCursorColumnAttr into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName,@basicInfoRelationFieldName,@basicInfoKeyFieldName
		while(@@FETCH_STATUS = 0)
		begin
		--Li表名_字段名(档案里的显示字段).列名(档案里的显示字段) Li表名_字段名(档案里的显示字段)
			set @fieldSql = @fieldSql + ' Li' + @tableName + '_' + @columnName + '.' + @basicInfoShowFieldName + ' Li' + @parentAttrTableName + '_' + @columnName +  ','
			--父表辅助关联
			set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + '_' + @columnName + ' ON'+ ' Li' + @tableName + '_' + @columnName + '.' + @basicInfoKeyFieldName + ' = Li' + @parentAttrTableName + '.' + @basicInfoRelationFieldName

			fetch next from liCursorColumnAttr into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName,@basicInfoRelationFieldName,@basicInfoKeyFieldName
		end
		close liCursorColumnAttr
		Deallocate liCursorColumnAttr
		
	
		--字典属性
		declare liCursorColumnDict cursor for 
		select [columnName],[columnType],basicInfoShowFieldName,TI.dataBaseName,TI.tableName
			from ColumnInfo CI
			left join TableInfo TI on CI.dictInfoType = TI.entityKey 
			WHERE CI.fid = @tableId and controlType in ('GridLookUpEditComboBox','StatusEdit')
		open liCursorColumnDict
		fetch next from liCursorColumnDict into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName
						while(@@FETCH_STATUS = 0)
		begin
			set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @parentDataBaseName + '.dbo.LiDict LiDict_' + @parentAttrTableName + '_' + @columnName + ' ON'+ ' LiDict_' + @parentAttrTableName + '_' + @columnName + '.dictCode = Li' + @parentAttrTableName + '.' + @columnName

			fetch next from liCursorColumnDict into @columnName,@columnType,@basicInfoShowFieldName,@dataBaseName,@tableName
		end
		close liCursorColumnDict
		Deallocate liCursorColumnDict
		

		--扩展字段
		declare liCursorColumnExtend cursor for 
			select [columnName],[columnType],TI.dataBaseName,CI.extendTableName,CI.extendTableKeyFieldName,CI.extendRelationTableKeyFieldName
			from ColumnInfo CI
			left join TableInfo TI on CI.fid = TI.id 
			WHERE CI.fid = @tableId and ISNULL(bExtendField,0) = 1
		open liCursorColumnExtend
		fetch next from liCursorColumnExtend into @columnName,@columnType,@dataBaseName,@tableName,@extendTableKeyFieldName,@extendRelationTableKeyFieldName
		while(@@FETCH_STATUS = 0)
		begin
			set @fieldSql = @fieldSql + ' Li' + @tableName + '_' + @columnName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
			set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + '_' + @columnName + ' ON'+ ' Li' + @tableName + '_' + @columnName + '.' + @extendTableKeyFieldName + ' = Li' + @parentAttrTableName + '.' + @extendRelationTableKeyFieldName

			fetch next from liCursorColumnExtend into @columnName,@columnType,@dataBaseName,@tableName,@extendTableKeyFieldName,@extendRelationTableKeyFieldName
		end
		close liCursorColumnExtend
		Deallocate liCursorColumnExtend
		

		fetch next from liCursorTable into @tableId,@dataBaseName,@entityType,@entityOrder,@entityColumnName,@tableName,@parentDataBaseName,@tableAliasName,@keyName

	end
	close liCursorTable
	Deallocate liCursorTable
END

set @querySql = 'SELECT * FROM ( SELECT cast(0 as bit) sel,*,ROW_NUMBER() over (order by '+ @orderBySql + '  ) as iPageRow FROM ( SELECT ' + LEFT(@fieldSql, LEN(@fieldSql)-1) + ' FROM ' + @leftjoinSql + ') A WHERE 1=1 ' + @whereSql + ' ) AA WHERE 1=1 ' + @rangeSql


	insert ShowResult (resultText,dDate) values (@querySql,getdate())

exec(@querySql)

print @querySql
end
