alter procedure sp_QueryList_Count(
@childTableName nvarchar(50),
@entityKey nvarchar(50),
@whereSql nvarchar(max)
)
as
--ROW_NUMBER() over (order by cinvcode  ) as iRow,
begin

declare @querySql nvarchar(max)
set @querySql = ''
declare @fieldSql nvarchar(max)
set @fieldSql = ''
declare @leftjoinSql nvarchar(max)
set @leftjoinSql = ''

declare @tableId int
declare @primaryKeyName nvarchar(50)
declare @foreignKeyName nvarchar(50)

declare @dataBaseName nvarchar(50)--���ݿ�����
declare @entityType nvarchar(50) --ʵ�����ͣ����ݣ�������
declare @entityOrder nvarchar(20)	--���ݵ�������
declare @entityColumnName nvarchar(50)	--�ӱ�������ģ�͵����ƣ��������ظ�������
declare @parentTableName nvarchar(50)	--����
declare @tableName nvarchar(50)	--����
declare @tableAliasName nvarchar(50)	--�����
declare @keyName nvarchar(50)	--������

--������Ϣ
SELECT  @tableId = id
	  ,@dataBaseName = ISNULL([dataBaseName], 'LiSystem')
      ,@entityType = [entityType]
      ,@entityOrder = [entityOrder]
      ,@entityColumnName = [entityColumnName]
      ,@tableName = [tableName]
	  ,@parentTableName = [tableName]
      ,@tableAliasName = [tableAliasName]
      ,@keyName = [keyName]
  FROM [TableInfo]
  WHERE [entityKey] = @entityKey
  AND [entityOrder] = 'master'
  
--�������
set @leftjoinSql = @leftjoinSql + @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName 

--������
declare @columnName nvarchar(30)
declare @columnType nvarchar(30)

declare liCursorColumn cursor for SELECT [columnName],[columnType] FROM [ColumnInfo] WHERE fid = @tableId
open liCursorColumn
fetch next from liCursorColumn into @columnName,@columnType
while(@@FETCH_STATUS = 0)
begin
	if(@columnType <> 'collection')
	begin
		set @fieldSql = @fieldSql + ' Li' + @tableName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
	end

	fetch next from liCursorColumn into @columnName,@columnType
end
close liCursorColumn
Deallocate liCursorColumn

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
	open liCursorTable
	fetch next from liCursorTable into @tableId,@dataBaseName,@entityType,@entityOrder,@entityColumnName,@tableName,@tableAliasName,@keyName
	while(@@FETCH_STATUS = 0)
	begin
		--�ӱ����
		select @primaryKeyName = primaryKeyName, @foreignKeyName = [columnName] from [ColumnInfo] where foreignKey = 1 and primaryKeyTableName = @parentTableName and fid = @tableId
		set @leftjoinSql = @leftjoinSql + ' LEFT JOIN ' + @dataBaseName + '.dbo.' + @tableName + ' Li' + @tableName + ' ON'+ ' Li' + @tableName + '.' + @foreignKeyName + ' = Li' + @parentTableName + '.' + @primaryKeyName

		--�ӱ���
		declare liCursorColumn cursor for SELECT [columnName],[columnType] FROM [ColumnInfo] WHERE fid = @tableId
		open liCursorColumn
		fetch next from liCursorColumn into @columnName,@columnType
		while(@@FETCH_STATUS = 0)
		begin
			if(@columnType <> 'collection')
			begin
				set @fieldSql = @fieldSql + ' Li' + @tableName + '.' + @columnName + ' Li' + @tableName + '_' + @columnName +  ','
			end

			fetch next from liCursorColumn into @columnName,@columnType
		end
		close liCursorColumn
		Deallocate liCursorColumn


		fetch next from liCursorTable into @tableId,@dataBaseName,@entityType,@entityOrder,@entityColumnName,@tableName,@tableAliasName,@keyName

	end
	close liCursorTable
	Deallocate liCursorTable
END

set @querySql = ' SELECT COUNT(1) as iCount FROM ( SELECT ' + LEFT(@fieldSql, LEN(@fieldSql)-1) + ' FROM ' + @leftjoinSql + ') A WHERE 1=1 ' + @whereSql 
exec (@querySql )

end