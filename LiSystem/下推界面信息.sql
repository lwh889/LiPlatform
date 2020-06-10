
drop table LiPushListButton
drop table LiPushEvent
drop table LiPushForm

create table LiPushForm(
	id int identity(1,1) primary key not null,
	[name]  nvarchar(30),	--����
	[text] nvarchar(20),
	height int default 0,
	width int default 0,
	systemCode nvarchar(20),
	bTemplate bit default 0, --�Ƿ���ģ��
	dCreateDate datetime default getdate()
)


create table LiPushListButton(
	id int identity(1,1) primary key not null,
	pushFormId int REFERENCES LiPushForm(id),
	iIndex int default 0,
	caption nvarchar(20),	--����
	[name]  nvarchar(30),	--����
	iconsize nvarchar(20),	--ͼ���С
	categoryGuid nvarchar(50),	--���ID newid form��������Ҫ
	icon nvarchar(30),	--ͼ������
	voucherStatus nvarchar(30),	--����״̬
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
