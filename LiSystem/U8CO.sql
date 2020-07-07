
create table LiU8COVoucher(
	id int identity(1,1) primary key not null,
	[code]  nvarchar(30),	--����
	[name]  nvarchar(50),	--����
	voucherType  nvarchar(10),	--��������
	voucherClassify  nvarchar(10),	--���ݷ���
	domHeadSql  nvarchar(4000),	--���ݷ���
	domBodySql  nvarchar(4000),	--���ݷ���
	dCreateDate datetime default getdate()
)


create table LiU8COField(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8COVoucher(id),
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