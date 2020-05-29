--exec sp_executesql N'exec GetTableInfo @a,@b ',N' @a varchar(80) ,@b varchar(80)','UFDATA_999_2014','Customer'
drop table ParamInfo
drop table ProcedureInfo
create table ProcedureInfo(
	id int identity(1,1) primary key,
	dataBaseName nvarchar(50),--数据库名称
	systemCode nvarchar(20),--系统代码
	entityKey nvarchar(50),	--单据主键
	procedureName nvarchar(50),	--表名

	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
alter table ProcedureInfo add systemCode nvarchar(20)
create table ParamInfo(
	id int identity(1,1) primary key,
	fid int REFERENCES ProcedureInfo(id),
	paramName nvarchar(50),	--列名
	paramType nvarchar(50),	--列类型
	paramLength int default 0,	--数据长度

	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
SET IDENTITY_Insert ProcedureInfo ON
insert into ProcedureInfo (id,dataBaseName,entityKey,procedureName,systemCode,modifyDate)
select 1,'LiSystem','GetTableInfo','GetTableInfo','LiSystem',GETDATE()
insert into ProcedureInfo (id,dataBaseName,entityKey,procedureName,systemCode,modifyDate)
select 2,'LiSystem','sp_QueryList','sp_QueryList','LiSystem',GETDATE()
insert into ProcedureInfo (id,dataBaseName,entityKey,procedureName,systemCode,modifyDate)
select 3,'LiSystem','sp_QueryList_Count','sp_QueryList_Count','LiSystem',GETDATE()
insert into ProcedureInfo (id,dataBaseName,entityKey,procedureName,systemCode,modifyDate)
select 4,'LiSystem','sp_getVoucherCode','sp_getVoucherCode','999',GETDATE()
insert into ProcedureInfo (id,dataBaseName,entityKey,procedureName,systemCode,modifyDate)
select 5,'LiSystem','sp_CreateTable','sp_CreateTable','LiSystem',GETDATE()
insert into ProcedureInfo (id,dataBaseName,entityKey,procedureName,systemCode,modifyDate)
select 6,'LiSystem','sp_turnPage','sp_turnPage','LiSystem',GETDATE()
SET IDENTITY_Insert ProcedureInfo OFF


insert into ParamInfo(fid, paramName, paramType, paramLength, modifyDate)
select 1,'dataBaseName', 'nvarchar', 5000,GETDATE()
union all
select 1,'tableName', 'nvarchar', 60,GETDATE()

insert into ParamInfo(fid, paramName, paramType, paramLength, modifyDate)
select 2,'childTableName', 'nvarchar', 50,GETDATE()
union all
select 2,'entityKey', 'nvarchar', 50,GETDATE()
union all
select 2,'systemCode', 'nvarchar', 10,GETDATE()
union all
select 2,'whereSql', 'nvarchar', 5000,GETDATE()
union all
select 2,'orderBySql', 'nvarchar', 50,GETDATE()
union all
select 2,'rangeSql', 'nvarchar', 50,GETDATE()

select * from ParamInfo where paramName = 'systemCode'
insert into ParamInfo(fid, paramName, paramType, paramLength, modifyDate)
select 3,'childTableName', 'nvarchar', 50,GETDATE()
union all
select 3,'systemCode', 'nvarchar', 10,GETDATE()
union all
select 3,'entityKey', 'nvarchar', 50,GETDATE()
union all
select 3,'whereSql', 'nvarchar', 5000,GETDATE()

insert into ParamInfo(fid, paramName, paramType, paramLength, modifyDate)
select 4,'entityKey', 'nvarchar', 50,GETDATE()
union all
select 4,'fieldTextValue', 'nvarchar', 50,GETDATE()
union all
select 4,'fieldDateValue', 'nvarchar', 50,GETDATE()
union all
select 4,'dateValue', 'datetime', 9,GETDATE()


insert into ParamInfo(fid, paramName, paramType, paramLength, modifyDate)
select 5,'formId', 'int', 9,GETDATE()


insert into ParamInfo(fid, paramName, paramType, paramLength, modifyDate)
select 6,'entityKey', 'nvarchar', 30,GETDATE()
union all
select 6,'turnPageType', 'nvarchar', 10,GETDATE()
union all
select 6,'voucherId', 'nvarchar', 50,GETDATE()
union all
select 6,'systemCode', 'nvarchar', 10,GETDATE()