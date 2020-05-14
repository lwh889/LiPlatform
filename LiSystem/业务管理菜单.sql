
drop table LiAdminMeum
create table LiAdminMeum(
	ID int identity(1,1) primary key,
	Code nvarchar(30) not null,
	Name nvarchar(30) not null,
	isGroup bit not null,
	isSystem bit default 0,
	GroupId int not null,
	ParentID int not null,
	imageIndex int not null,
	iOrder int not null,
	dCreateDate datetime default getdate()
)

insert into LiAdminMeum (code, name, isGroup,isSystem, groupId, parentId, imageIndex, iOrder)
select 'LiBusinessManage','业务管理',1,1,1,0,1,1
union all
select 'LiBasicInfoManage','基础管理',1,1,2,0,2,2
union all
select 'LiSystemManage','系统管理',1,1,3,0,3,3