drop table LiControlStatus
drop table LiStatus
drop table LiVoucherStatus

create table LiVoucherStatus(
	id int identity(1,1) primary key not null,
	code nvarchar(50),--单据编码
	name nvarchar(50),--单据名称
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
--alter table LiVoucherStatus add bNew bit default 0
--alter table LiVoucherStatus add bShow bit default 0

create table LiStatus(
	id int identity(1,1) primary key,
	fid int REFERENCES LiVoucherStatus(id),
	code nvarchar(50),--状态编码
	name nvarchar(50),--状态名称
	bNew bit default 0,--新增
	bShow bit default 0,--浏览 
	userFieldName nvarchar(30),	--用户控件
	dateFieldName nvarchar(30),	--日期控件
	statusFieldName nvarchar(30),	--状态控件
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
--alter table LiStatus add statusFieldName  nvarchar(30)
--alter table LiStatus add userFieldName  nvarchar(30)
--alter table LiStatus add dateFieldName  nvarchar(30)
--alter table LiStatus add bNew bit default 0
--alter table LiStatus add bShow bit default 0



create table LiControlStatus(
	id int identity(1,1) primary key,
	fid int REFERENCES LiStatus(id),
	code nvarchar(50),--控件编码
	name nvarchar(50),--控件名称
	groupName nvarchar(50),--控件组名称
	bReadOnly bit default 0,	--只读
	bVisibe bit default 1,	--可视
	defaultValue nvarchar(max),	--默认值
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)