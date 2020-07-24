alter procedure sp_TestReport_Count(
@whereSql nvarchar(max)
)
as
--ROW_NUMBER() over (order by cinvcode  ) as iRow,
begin

declare @querySql nvarchar(max)
set @querySql = ''
declare @selectSql nvarchar(max)
set @selectSql = 'select cInvCode,cInvName,cInvStd iCount from UFDATA_999_2014.dbo.Inventory '

set @querySql = ' SELECT COUNT(1) as iCount FROM ( ' + @selectSql + ') A WHERE 1=1 ' + @whereSql 
exec (@querySql )

end