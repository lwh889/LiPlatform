
alter procedure sp_turnPage(
@entityKey nvarchar(30),
@turnPageType nvarchar(10),
@voucherId nvarchar(50) = '-1',
@systemCode nvarchar(10)
)
as
begin
	--±‰¡ø
	declare @SqlAll nvarchar(max)
	set @SqlAll = ''
	declare @dataBaseName nvarchar(30)
	declare @keyName nvarchar(30)
	declare @tableName nvarchar(30)

	select top 1 @dataBaseName = dataBaseName,@keyName = keyName,@tableName = tableName from TableInfo where entityKey =@entityKey and systemCode = @systemCode and entityOrder = 'master'
	
	SET @SqlAll = '
		if(''' + @turnPageType + ''' = ''First'')
		begin
			select MIN(' + @keyName + ') as id from ' + @dataBaseName +'.dbo.' + @tableName +'
		end
		else if(''' + @turnPageType + ''' = ''Previous'')
		begin
			select MAX(' + @keyName + ') as id from ' + @dataBaseName +'.dbo.' + @tableName +' where ' + @keyName + ' < ' + @voucherId + '
		end
		else if(''' + @turnPageType + ''' = ''Next'')
		begin
			select MIN(' + @keyName + ') as id from ' + @dataBaseName +'.dbo.' + @tableName +' where ' + @keyName + ' > ' + @voucherId + '
		end
		else if(''' + @turnPageType + ''' = ''Last'')
		begin
			select MAX(' + @keyName + ') as id from ' + @dataBaseName +'.dbo.' + @tableName +'
		end
		else
		begin
			select 0 as id
		end
	'
	
	PRINT @SqlAll
	EXEC(@SqlAll)

end