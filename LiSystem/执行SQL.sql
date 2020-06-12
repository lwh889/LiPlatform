create procedure sp_ExecSql(
@execSql nvarchar(max)
)
as
begin
	if(ISNULL(@execSql,'') <> '')
	begin
		exec(@execSql)
	end
end