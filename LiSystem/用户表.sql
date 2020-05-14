drop table LiAuthData
drop table LiAuth
drop table LiUserRole
drop table LiRoles
drop table LiUsers

create table LiUsers(
	id int identity(1,1) primary key,
	userCode nvarchar(20) not null,
	userName nvarchar(20) not null,
	userPassword nvarchar(300) not null,
	skinName nvarchar(50) null,
	bAdmin bit default 0,
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
alter table LiUsers add skinName nvarchar(50) null
insert into LiUsers (userCode,userName, userPassword,bAdmin,modifyDate) 
values ('demo','demo','demo', 1, getdate())

create table LiRoles(
	id int identity(1,1) primary key,
	roleCode nvarchar(20) not null,
	roleName nvarchar(30) not null,
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

create table LiAuth(
	id int identity(1,1) primary key,
	roleCode nvarchar(20) not null,
	entityKey nvarchar(30) not null,
	code nvarchar(20) not null,
	[name] nvarchar(30) ,
	bShow bit default 0,
	bEdit bit default 0,
	createDate datetime default getdate()
)


create table LiAuthData(
	id int identity(1,1) primary key,
	entityKey nvarchar(30) not null,
	roleCode nvarchar(20) not null,
	code nvarchar(20) not null,
	[name] nvarchar(30) ,
	bShow bit default 0,
	bEdit bit default 0,
	createDate datetime default getdate()
)

create table LiUserRole(
	id int identity(1,1) primary key,
	userCode  nvarchar(20),
	roleCode  nvarchar(20),
	createDate datetime default getdate()
)