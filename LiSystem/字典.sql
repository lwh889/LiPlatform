
drop table LiDict
drop table LiDictGroup

create table LiDictGroup(
	ID int identity(1,1) primary key,
	Code nvarchar(30) not null,
	Name nvarchar(30) not null,
	isGroup bit not null,
	isSystem bit default 0,
	GroupId int not null,
	ParentID int not null,
	imageIndex int not null,
	iOrder int not null,
	dModifyDate datetime,	--修改时间
	dCreateDate datetime default getdate()
)

create table LiDict(
	id int identity(1,1) primary key,
	dictParentID int REFERENCES LiDictGroup(id),
	dictCode nvarchar(50) not null,--状态编码
	dictName nvarchar(50),--状态名称
	dictOrder int,--状态名称
	dictMemo nvarchar(max),--状态名称
	dModifyDate datetime,	--修改时间
	dCreateDate datetime default getdate()
)


