drop table ColumnInfo
drop table TableInfo
create table TableInfo(
	id int identity(1,1) primary key,
	entityKey nvarchar(50),	--单据主键
	systemCode nvarchar(10),	--单据主键
	entityOrder nvarchar(20),	--单据的主，次
	entityColumnName nvarchar(50),	--子表在主表模型的名称
	tableName nvarchar(50),	--表名
	tableAliasName nvarchar(50),	--表别名
	tableAbbName nvarchar(50),	--表简称
	tableDesc nvarchar(255),	--表描述
	className nvarchar(50),	--json数据转化到实体的类名
	keyName nvarchar(50),	--主键名
	childTableEntityColumnName nvarchar(500),	--外键名
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

--alter table TableInfo add systemCode nvarchar(10)
create table ColumnInfo(
	id int identity(1,1) primary key,
	fid int REFERENCES TableInfo(id),
	columnName nvarchar(50),	--列名
	columnAbbName nvarchar(50),	--列简称
	columnType nvarchar(50),	--列类型
	length int default 0,	--数据长度
	primaryKey bit default 0,	--主键
	foreignKey bit default 0,	--外键
	relationshipType int default 0,	--关系类型，一对多，一对一,0 无，1 一对一，2 一对多，3 自已
	databaseGeneratedType int default 0,	--列的自增类型,0 不处理，1 自增长，2 计算所得，3 newid
	primaryKeyName  nvarchar(50),	--子表外键列是要写上主键名称
	primaryKeyDatabaseGenerated int default 0,	--子表外键列是要写上主键自增类型
	primaryKeyTableName nvarchar(50),	--子表外键列是要写上主表名
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

insert into TableInfo (entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, foreignKeyName,modifyDate) 
values ('entity1','master',null,'spring_user', 'user1', '用户表', null,'JsonModel', 'id',null, getdate())
insert into TableInfo (entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, foreignKeyName,modifyDate) 
values ('entity1','slave','datas','spring_book', 'book1', '书表', null,'JsonModel', 'id','fid', getdate())


insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated) 
select 1, 'id','主键','int',9,1,0,0,1,null,0
union all
select 1, 'name','名称', 'narchar',50,0,0,0,0,null,0
union all
select 1, 'password','密码','nvarchar', 255,0,0,0,0,null,0
union all
select 1, 'datas','数据集','collection', -1,0,0,2,0,null,0


insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated) 
select 2, 'id','主键','int',9,1,0,0,1,null,0
union all
select 2, 'fid','外键','int',9,0,1,0,0,'id',1
union all
select 2, 'name','名称', 'narchar',50,0,0,0,0,null,0

--SELECT fid, relationshipType, modifyDate, length, columnAbbName, columnType, primaryKeyDatabaseGenerated, databaseGeneratedType, id, primaryKeyName, foreignKey, columnName, primaryKey FROM  ColumnInfo WHERE fid = 1

drop table spring_book
drop table spring_user

create table spring_user(
	id int identity(1,1) primary Key not null,
	name nvarchar(255),
	password nvarchar(255)

)


create table spring_book(
	id int identity(1,1) primary Key not null,
	fid int REFERENCES spring_user(id),
	name nvarchar(255)

)



insert spring_user (name, password) values ('testtest', '%^&*(')
insert spring_book (fid, name) values (@@IDENTITY, '中国')
 --INSERT INTO spring_user (password,name)  VALUES ('^sf产','json') declare @id int  set @id= @@identity INSERT INTO spring_book (fid,name)  VALUES (@id,'jsonbook') INSERT INTO spring_book (fid,name)  VALUES (@id,'bookjson')
