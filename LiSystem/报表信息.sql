
drop table LiReportEvent
drop table LiReportButton
drop table LiReportField
drop table LiReport

create table LiReport(
	id int identity(1,1) primary key,
	reportKey nvarchar(30) not null,
	reportName nvarchar(30),
	systemCode nvarchar(10),
	menuCode int,
	reportSql nvarchar(max),
	reportCountSql nvarchar(max),
	createDate datetime default getdate()
)

--alter table LiReport add systemCode nvarchar(10)
--alter table LiReport add menuCode int

create table LiReportField(
	id int identity(1,1) primary key,
	reportId int REFERENCES LiReport(id),
	
	columnName nvarchar(50),	--�ֶ���
	columnCaption nvarchar(50),	--������
	columnType nvarchar(100),	--������
	iColumnWidth int,	--�п�
	bColumnDisplay bit,	--�Ƿ���ʾ��
	iColumnIndex int,	--������
	bQuery bit,	--�Ƿ��ѯ
	
	iDisplayFormatType  int default 0,--ʱ�䣬���֣��Զ��壿
	displayFormat nvarchar(30),	--��ʾ��ʽ
	bColumnGroup bit,	--�Ƿ����
	columnGroupFormat nvarchar(30),	--���ܸ�ʽ
	
	createDate datetime default getdate()
)


create table LiReportButton(
	id int identity(1,1) primary key,
	reportId int REFERENCES LiReport(id),
	
	caption nvarchar(20),	--����
	name nvarchar(20),	--����
	iconsize nvarchar(50),	--��ť����
	categoryGuid nvarchar(50),	--���ID
	icon nvarchar(100),	--ͼ��
	iIndex int,--˳��
	createDate datetime default getdate()
)


create table LiReportEvent(
	id int identity(1,1) primary key,
	reportButtonId int REFERENCES LiReportButton(id),
	
	fullName nvarchar(100),	--ȫ����
	assemblyName nvarchar(100),	--����
	eventMemo nvarchar(100),	--��ע
	)