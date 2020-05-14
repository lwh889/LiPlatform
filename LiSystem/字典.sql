
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
	dModifyDate datetime,	--�޸�ʱ��
	dCreateDate datetime default getdate()
)

create table LiDict(
	id int identity(1,1) primary key,
	dictParentID int REFERENCES LiDictGroup(id),
	dictCode nvarchar(50) not null,--״̬����
	dictName nvarchar(50),--״̬����
	dictOrder int,--״̬����
	dictMemo nvarchar(max),--״̬����
	dModifyDate datetime,	--�޸�ʱ��
	dCreateDate datetime default getdate()
)


