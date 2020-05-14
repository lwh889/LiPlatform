create view V_SysDatabases
as
select dbid,name from master.dbo.SysDatabases 
