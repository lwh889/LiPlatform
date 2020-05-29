
create table LiU8Voucher(
	id int identity(1,1) primary key not null,
	[code]  nvarchar(30),	--����
	[name]  nvarchar(50),	--����
	voucherType  nvarchar(10),	--��������
	dCreateDate datetime default getdate()
)


create table LiU8Operation(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Voucher(id),
	operationCode  nvarchar(30),	--��������
	operationName  nvarchar(50),	--��������
	operationSymbol  nvarchar(20),	--��������
	dCreateDate datetime default getdate()
)


create table LiU8Field(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Operation(id),
	fieldEntityType  nvarchar(30),	--ʵ������
	fieldName  nvarchar(50),	--�ֶ���
	fieldDesc  nvarchar(200),	--�ֶ�����
	fieldType  nvarchar(50),	--�ֶ�����
	fieldIsRequire  bit,	--�Ƿ����
	fieldMaxValue  nvarchar(200),	--���ֵ
	fieldMinValue  nvarchar(200),	--��Сֵ
	fieldDefaultValue  nvarchar(200),	--Ĭ��ֵ
	fieldbDefault  bit,	--�Ƿ�Ĭ��
	fieldLength  int,	--��󳤶�
	dCreateDate datetime default getdate()
)

--alter table LiU8Field add fieldDefaultValue  nvarchar(200)
--alter table LiU8Field add fieldbDefault  bit


create table LiU8Param(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Operation(id),
	paramName  nvarchar(30),	--������
	paramDesc  nvarchar(200),	--��������
	paramType  nvarchar(50),	--��������
	paramDirection  nvarchar(20),	--��������
	paramTransMode  nvarchar(20),	--���ݷ�ʽ
	paramIsRequire  bit,	--�Ƿ��ѡ
	paramBoObject  nvarchar(50),	--BO����
	paramBoType  nvarchar(50),	--�Ƿ�BO��ͷ
	paramDefaultValue  nvarchar(200),
	parambID  bit,	--�Ƿ���ID�ֶ���
	parambCode  bit,	--�Ƿ���Code�ֶ���
	dCreateDate datetime default getdate()
)
--alter table LiU8Param add paramDefaultValue  nvarchar(200)
--alter table LiU8Param add parambID  bit
--alter table LiU8Param add parambCode  bit


--API������
create table LiU8EnvContext(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Operation(id),
	contextName  nvarchar(30),	--����������
	contextDesc  nvarchar(200),	--	����
	contextType  nvarchar(50),	--	����������
	contextDefaultValue  nvarchar(200),
	dCreateDate datetime default getdate()
)
--alter table LiU8EnvContext add contextDefaultValue  nvarchar(200)


