
drop table LiVoucherCode
create table LiVoucherCode(
	id int identity(1,1) primary key,
	
	entityKey nvarchar(50),--ʵ��Key
	bDefault bit default 0,--Ĭ�Ϸ���
	[name] nvarchar(50),--��������
	prefixName nvarchar(10),--ǰ׺
	fieldTextName nvarchar(50),--�����ı��ֶ�
	fieldDateName nvarchar(50),--���������ֶ�
	dateTimeFormat nvarchar(50),	--���ڸ�ʽ
	iZero int default 1,	--��߲�0
	iStep int default 1,	--��ˮ�Ų�λ
	flowNoRange  nvarchar(10) default 'DAY', --��ˮ�ŷ�Χ
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

drop table LiFlowNo
create table LiFlowNo(
	id int identity(1,1) primary key,
	
	entityKey nvarchar(50),--ʵ��Key
	voucherCodeName nvarchar(50),
	
	iYear int,
	iMonth int,
	iDay int,
	iFlow int,

	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
