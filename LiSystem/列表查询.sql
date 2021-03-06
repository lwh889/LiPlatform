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
set @parentAttrTableName = ''
declare @tableName nvarchar(50)	--表名
declare @tableAliasName nvarchar(50)	--表别名
declare @keyName nvarchar(50)	--主键名

--辅助属性
SELECT C.[basicInfoKey],C.name [fieldName],C.basicInfoAssistFieldName [columnName],T.tableName,T.dataBaseName,P.type,P.entityColumnName,T.keyName attrKeyName,C.basicInfoAssistType
INTO #TempAttr
FROM [LiControl] C 
left join LiControlGroup CG on C.controlGroupId = CG.id
left join LiPanel P on CG.panelModelId = P.id
left join LiForm F on P.formModelId = F.id
left join TableInfo T on T.entityKey = C.basicInfoKey
where   controltype = 'GridLookUpEditRefAssist' and F.[name] = @entityKey and T.systemCode = @systemCode

--字典
SELECT C.[basicInfoKey],C.name [fieldName],C.basicInfoAssistFieldName [columnName],P.type,P.entityColumnName,C.basicInfoAssistType
INTO #TempDict
FROM [LiControl] C 
left join LiControlGroup CG on C.controlGroupId = CG.id
left join LiPanel P on CG.panelModelId = P.id
left join LiForm F on P.formModelId = F.id
where   controltype in ( 'GridLookUpEditComboBox','StatusEdit') and F.[name] = @entityKey and F.systemCode = @systemCode

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


--父表列
declare @attrKeyName nvarchar(50)
declare @basicInfoAssistType nvarchar(50)
declare @basicInfoKey nvarchar(50)
declare @fieldName nvarchar(50)
declare @columnName nvarchar(50)
declare @columnType nvarchar(50)
declare @controlType nvarchar(50)

declare liCursorColumn cursor for SELECT [columnName],[columnType],C.controltype
									FROM [LiControl] C 
									left join LiControlGroup CG on C.controlGroupId = CG.id
									left join LiPanel P on CG.panelModelId = P.id
									left join LiForm F on P.formModelId = F.id
									left join TableInfo TI on TI.systemCode = F.systemCode and F.name = TI.entityKey and TI.tableName = P.tableName  and TI.systemCode = @systemCode
									left join ColumnInfo CI on TI.id =CI.fid and CI.columnName = C.name WHERE CI.fid = @tableId
									union all
									select [columnName],[columnType],null controltype from ColumnInfo CI WHERE CI.fid = @tableId and (primaryKey = 1)
open liCursorColumn
fetch next from liCursorColumn into @columnName,@columnType,@controlType
while(@@FETCH_STATUS = 0)
begin
	if(@columnType <> 'collection')
	begin
	
		if(@controlType = 'StatusEdit' or @controlType = 'GridLookUpEditComboBox')
		begin
			set @fieldSql = @fieldSql + ' LiDict_' + @tableName + '_' + @columnName + '.dictCode Li' + @tableName + '_' + @columnName +  '_Code,'
			set @fieldSql = @fieldSql + ' LiDict_' + @tableName + '_' + @columnName + '.dictName Li' + @tableName + '_' + @columnName +  '_Name,'
		end
		else
		begin
			set @fieldSql = @fieldSql + ' Li' + @tableName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
		end
	end

	fetch next from liCursorColumn into @columnName,@columnType,@controlType
end
close liCursorColumn
Deallocate liCursorColumn

--辅助属性
declare liCursorColumnAttr cursor for SELECT [basicInfoKey],[fieldName],[columnName],[tableName],[dataBaseName],attrKeyName,basicInfoAssistType FROM #TempAttr WHERE [type] = 'Basic'
open liCursorColumnAttr
fetch next from liCursorColumnAttr into @basicInfoKey,@fieldName,@columnName,@tableName,@dataBaseName,@attrKeyName,@basicInfoAssistType
while(@@FETCH_STATUS = 0)
begin
	set @fieldSql = @fieldSql + ' Li' + @tableName + '_' + @columnName + '.' + @columnName + ' Li' + @parentTableName + '_' + @fieldName +  ','
	--父表辅助关联
	set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + '_' + @columnName + ' ON'+ ' Li' + @tableName + '_' + @columnName + '.' + @basicInfoAssistType + ' = Li' + @parentTableName + '.' + @attrKeyName

	fetch next from liCursorColumnAttr into @basicInfoKey,@fieldName,@columnName,@tableName,@dataBaseName,@attrKeyName,@basicInfoAssistType
end
close liCursorColumnAttr
Deallocate liCursorColumnAttr

--字典属性
declare liCursorColumnDict cursor for SELECT [basicInfoKey],[fieldName],[columnName],basicInfoAssistType FROM #TempDict WHERE [type] = 'Basic'
open liCursorColumnDict
fetch next from liCursorColumnDict into @basicInfoKey,@fieldName,@columnName,@basicInfoAssistType
while(@@FETCH_STATUS = 0)
begin
	set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @parentDataBaseName + '.dbo.LiDict LiDict_' + @parentTableName + '_' + @fieldName + ' ON'+ ' LiDict_' + @parentTableName + '_' + @fieldName + '.dictCode = Li' + @parentTableName + '.' + @fieldName

	fetch next from liCursorColumnDict into @basicInfoKey,@fieldName,@columnName,@basicInfoAssistType
end
close liCursorColumnDict
Deallocate liCursorColumnDict

print @leftjoinSql
if(ISNULL(@childTableName, '') <> '')
BEGIN
	declare liCursorTable cursor for SELECT 
			id,ISNULL([dataBaseName], 'LiSystem')
		  ,[entityType]
		  ,[entityOrder]
		  ,[entityColumnName]
		  ,[tableName]
		  ,[tableAliasName]
		  ,[keyName]
	  FROM [LiSystem].[dbo].[TableInfo]
	  WHERE [entityKey] = @entityKey
	  AND [tableName] = @childTableName
	  AND [entityOrder] = 'slave'
	  AND systemCode = @systemCode
	open liCursorTable
	fetch next from liCursorTable into @tableId,@dataBaseName,@entityType,@entityOrder,@entityColumnName,@tableName,@tableAliasName,@keyName
	while(@@FETCH_STATUS = 0)
	begin
		set @parentAttrTableName = @tableName
		--子表关联
		select @primaryKeyName = primaryKeyName, @foreignKeyName = [columnName] from [ColumnInfo] where foreignKey = 1 and primaryKeyTableName = @parentTableName and fid = @tableId
		set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' + @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + ' ON'+ ' Li' + @tableName + '.' + @foreignKeyName + ' = Li' + @parentTableName + '.' + @primaryKeyName

		--子表列
		declare liCursorColumn cursor for SELECT [columnName],[columnType] FROM [ColumnInfo] WHERE fid = @tableId
		open liCursorColumn
		fetch next from liCursorColumn into @columnName,@columnType
		while(@@FETCH_STATUS = 0)
		begin

			if(@columnType <> 'collection')
			begin
			
				if(@controlType = 'StatusEdit' and @controlType = 'GridLookUpEditComboBox')
				begin
					set @fieldSql = @fieldSql + ' LiDict_' + @parentAttrTableName + '_' + @columnName + '.dictCode Li' + @parentAttrTableName + '_' + @columnName +  '_Code,'
					set @fieldSql = @fieldSql + ' LiDict_' + @parentAttrTableName + '_' + @columnName + '.dictName Li' + @parentAttrTableName + '_' + @columnName +  '_Name,'
				end
				else
				begin
					set @fieldSql = @fieldSql + ' Li' + @tableName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
				end

			end

			fetch next from liCursorColumn into @columnName,@columnType
		end
		close liCursorColumn
		Deallocate liCursorColumn

		
		--辅助属性
		declare liCursorColumnAttr cursor for SELECT [basicInfoKey],[fieldName],[columnName],[tableName],[dataBaseName],attrKeyName,basicInfoAssistType FROM #TempAttr WHERE entityColumnName = [entityColumnName]
		open liCursorColumnAttr
		fetch next from liCursorColumnAttr into @basicInfoKey,@fieldName,@columnName,@tableName,@dataBaseName,@attrKeyName,@basicInfoAssistType
		while(@@FETCH_STATUS = 0)
		begin
			set @fieldSql = @fieldSql + ' Li' + @tableName + '_' + @columnName + '.' + @columnName + ' Li' + @parentAttrTableName + '_' + @fieldName +  ','
			--父表辅助关联
			set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + '_' + @columnName + ' ON'+ ' Li' + @tableName + '_' + @columnName + '.' + @basicInfoAssistType + ' = Li' + @parentAttrTableName + '.' + @attrKeyName

			----父表辅助关联
			--set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @dataBaseName + '.dbo.LiDict LiDict_' + @tableName + '_' + @columnName + ' ON'+ ' LiDict_' + @tableName + '_' + @columnName + '.dictCode = Li' + @tableName + '.' + @columnName

			fetch next from liCursorColumnAttr into @basicInfoKey,@fieldName,@columnName,@tableName,@dataBaseName,@attrKeyName,@basicInfoAssistType
		end
		close liCursorColumnAttr
		Deallocate liCursorColumnAttr
		
		--字典属性
		declare liCursorColumnDict cursor for SELECT [basicInfoKey],[fieldName],[columnName],basicInfoAssistType FROM #TempDict WHERE entityColumnName = [entityColumnName]
		open liCursorColumnDict
		fetch next from liCursorColumnDict into @basicInfoKey,@fieldName,@columnName,@basicInfoAssistType
		while(@@FETCH_STATUS = 0)
		begin
			set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' +  @parentDataBaseName + '.dbo.LiDict LiDict_' + @parentAttrTableName + '_' + @fieldName + ' ON'+ ' LiDict_' + @parentAttrTableName + '_' + @fieldName + '.dictCode = Li' + @parentAttrTableName + '.' + @fieldName 

			fetch next from liCursorColumnDict into @basicInfoKey,@fieldName,@columnName,@basicInfoAssistType
		end
		close liCursorColumnDict
		Deallocate liCursorColumnDict

		fetch next from liCursorTable into @tableId,@dataBaseName,@entityType,@entityOrder,@entityColumnName,@tableName,@tableAliasName,@keyName

	end
	close liCursorTable
	Deallocate liCursorTable
END
--print '@querySql:'+ 'SELECT * FROM ( SELECT cast(0 as bit) sel,*,ROW_NUMBER() over (order by '+ @orderBySql + '  ) as iPageRow FROM ( SELECT ' + LEFT(@fieldSql, LEN(@fieldSql)-1) + ' FROM '+ @leftjoinSql
set @querySql = 'SELECT * FROM ( SELECT cast(0 as bit) sel,*,ROW_NUMBER() over (order by '+ @orderBySql + '  ) as iPageRow FROM ( SELECT ' + LEFT(@fieldSql, LEN(@fieldSql)-1) + ' FROM ' + @leftjoinSql + ') A WHERE 1=1 ' + @whereSql + ' ) AA WHERE 1=1 ' + @rangeSql
print @querySql
	--insert ShowResult (resultText) values (@querySql)
--print '@orderBySql:'+ @orderBySql
--print '@fieldSql:'+ @fieldSql
--print '@leftjoinSql:'+ @leftjoinSql
--print '@whereSql:'+ @whereSql
exec (@querySql )

end