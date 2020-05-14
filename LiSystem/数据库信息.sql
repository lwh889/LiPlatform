drop table ColumnInfo
drop table TableInfo
create table TableInfo(
	id int identity(1,1) primary key,
	entityKey nvarchar(50),	--��������
	systemCode nvarchar(10),	--��������
	entityOrder nvarchar(20),	--���ݵ�������
	entityColumnName nvarchar(50),	--�ӱ�������ģ�͵�����
	tableName nvarchar(50),	--����
	tableAliasName nvarchar(50),	--�����
	tableAbbName nvarchar(50),	--����
	tableDesc nvarchar(255),	--������
	className nvarchar(50),	--json����ת����ʵ�������
	keyName nvarchar(50),	--������
	childTableEntityColumnName nvarchar(500),	--�����
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

--alter table TableInfo add systemCode nvarchar(10)
create table ColumnInfo(
	id int identity(1,1) primary key,
	fid int REFERENCES TableInfo(id),
	columnName nvarchar(50),	--����
	columnAbbName nvarchar(50),	--�м��
	columnType nvarchar(50),	--������
	length int default 0,	--���ݳ���
	primaryKey bit default 0,	--����
	foreignKey bit default 0,	--���
	relationshipType int default 0,	--��ϵ���ͣ�һ�Զ࣬һ��һ,0 �ޣ�1 һ��һ��2 һ�Զ࣬3 ����
	databaseGeneratedType int default 0,	--�е���������,0 ������1 ��������2 �������ã�3 newid
	primaryKeyName  nvarchar(50),	--�ӱ��������Ҫд����������
	primaryKeyDatabaseGenerated int default 0,	--�ӱ��������Ҫд��������������
	primaryKeyTableName nvarchar(50),	--�ӱ��������Ҫд��������
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

insert into TableInfo (entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, foreignKeyName,modifyDate) 
values ('entity1','master',null,'spring_user', 'user1', '�û���', null,'JsonModel', 'id',null, getdate())
insert into TableInfo (entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, foreignKeyName,modifyDate) 
values ('entity1','slave','datas','spring_book', 'book1', '���', null,'JsonModel', 'id','fid', getdate())


insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated) 
select 1, 'id','����','int',9,1,0,0,1,null,0
union all
select 1, 'name','����', 'narchar',50,0,0,0,0,null,0
union all
select 1, 'password','����','nvarchar', 255,0,0,0,0,null,0
union all
select 1, 'datas','���ݼ�','collection', -1,0,0,2,0,null,0


insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated) 
select 2, 'id','����','int',9,1,0,0,1,null,0
union all
select 2, 'fid','���','int',9,0,1,0,0,'id',1
union all
select 2, 'name','����', 'narchar',50,0,0,0,0,null,0

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
insert spring_book (fid, name) values (@@IDENTITY, '�й�')
 --INSERT INTO spring_user (password,name)  VALUES ('^sf��','json') declare @id int  set @id= @@identity INSERT INTO spring_book (fid,name)  VALUES (@id,'jsonbook') INSERT INTO spring_book (fid,name)  VALUES (@id,'bookjson')
