alter procedure sp_TestReport(
@whereSql nvarchar(max),
@orderBySql nvarchar(max),
@rangeSql nvarchar(max)
)
as
--ROW_NUMBER() over (order by cinvcode  ) as iRow,
begin

declare @querySql nvarchar(max)
set @querySql = ''
declare @selectSql nvarchar(max)
if(@orderBySql is null or @orderBySql = '')
begin
set @orderBySql = 'cInvCode,cInvName,cInvStd'
end

set @selectSql = 'select cInvCode,cInvName,cInvStd,cInvCCode from UFDATA_999_2014.dbo.Inventory'
set @querySql = 'SELECT * FROM ( SELECT cast(0 as bit) sel,*,ROW_NUMBER() over (order by '+ @orderBySql + '  ) as iPageRow FROM (  ' + @selectSql + ') A WHERE 1=1 ' + @whereSql + ' ) AA WHERE 1=1 ' + @rangeSql

exec(@querySql)

print @querySql

end