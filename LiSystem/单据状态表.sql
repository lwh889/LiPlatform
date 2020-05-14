drop table LiControlStatus
drop table LiStatus
drop table LiVoucherStatus

create table LiVoucherStatus(
	id int identity(1,1) primary key not null,
	code nvarchar(50),--���ݱ���
	name nvarchar(50),--��������
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
--alter table LiVoucherStatus add bNew bit default 0
--alter table LiVoucherStatus add bShow bit default 0

create table LiStatus(
	id int identity(1,1) primary key,
	fid int REFERENCES LiVoucherStatus(id),
	code nvarchar(50),--״̬����
	name nvarchar(50),--״̬����
	bNew bit default 0,--����
	bShow bit default 0,--��� 
	userFieldName nvarchar(30),	--�û��ؼ�
	dateFieldName nvarchar(30),	--���ڿؼ�
	statusFieldName nvarchar(30),	--״̬�ؼ�
	modifyDate datetime,	--�޸�ʱ��
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
	code nvarchar(50),--�ؼ�����
	name nvarchar(50),--�ؼ�����
	groupName nvarchar(50),--�ؼ�������
	bReadOnly bit default 0,	--ֻ��
	bVisibe bit default 1,	--����
	defaultValue nvarchar(max),	--Ĭ��ֵ
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)