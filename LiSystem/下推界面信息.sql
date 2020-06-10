
drop table LiPushListButton
drop table LiPushEvent
drop table LiPushForm

create table LiPushForm(
	id int identity(1,1) primary key not null,
	[name]  nvarchar(30),	--名称
	[text] nvarchar(20),
	height int default 0,
	width int default 0,
	systemCode nvarchar(20),
	bTemplate bit default 0, --是否是模板
	dCreateDate datetime default getdate()
)


create table LiPushListButton(
	id int identity(1,1) primary key not null,
	pushFormId int REFERENCES LiPushForm(id),
	iIndex int default 0,
	caption nvarchar(20),	--标题
	[name]  nvarchar(30),	--名称
	iconsize nvarchar(20),	--图标大小
	categoryGuid nvarchar(50),	--类别ID newid form工具栏需要
	icon nvarchar(30),	--图标名称
	voucherStatus nvarchar(30),	--单据状态
	dCreateDate datetime default getdate()
)


create table LiPushEvent(
	id int identity(1,1) primary key not null,
	pushFormId int REFERENCES LiPushForm(id),
	pushListButtonId int REFERENCES LiPushListButton(id),
	fullName nvarchar(300),
	assemblyName nvarchar(50),
	eventMemo nvarchar(300),
	dCreateDate datetime default getdate()
)
