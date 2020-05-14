
drop table LiVersionFlowCondition
drop table LiVersionFlowConnector
drop table LiVersionFlowUser
drop table LiVersionFlowNode
drop table LiVersionFlow

create table LiVersionFlow(
	id int identity(1,1) primary key not null,
	flowVersionNumber nvarchar(30),	--���̱���
	flowCode nvarchar(30),	--���̱���
	flowName nvarchar(50),	--��������
	entityKey nvarchar(30),	--���ݱ���
	entityName nvarchar(50),	--��������
	bDefault bit default 0,	--�Ƿ�Ĭ��
	openStatus nvarchar(30) default '��ֹ',--����״̬
	flowVersionDate datetime2,	--��������
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

--alter table LiVersionFlow add openStatus nvarchar(30) default '��ֹ'
create table LiVersionFlowNode(
	id int identity(1,1) primary key not null,
	flowId int REFERENCES LiVersionFlow(id),
	flowNodeCode nvarchar(30),	--���̽ڵ����
	flowNodeName nvarchar(50),	--���̽ڵ�����
	flowNodeType nvarchar(30),	--���̽ڵ�����
	flowNodeInformation nvarchar(500),	--������Ϣ
	X int default 0,
	Y int default 0,
	width int default 0,
	height int default 0,
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
--alter table LiVersionFlowNode add flowNodeInformation nvarchar(500)


create table LiVersionFlowUser(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiVersionFlowNode(id),
	userCode nvarchar(30),	--�û�����
	userName nvarchar(50),	--�û�����
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)


create table LiVersionFlowConnector(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiVersionFlowNode(id),
	flowNodeCodeTo nvarchar(30),	--�ڵ����
	flowNodeNameTo nvarchar(50),	--�ڵ�����
	flowNodeFormIndex int default 0,
	flowNodeToIndex int default 3,

	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
--alter table LiFlowConnector add flowNodeFormIndex int default 0
--alter table LiFlowConnector add flowNodeToIndex int default 3


create table LiVersionFlowCondition(
	id int identity(1,1) primary key,
	flowConnectorId int REFERENCES LiVersionFlowConnector(id),
	sBracketsBefore nvarchar(10) ,	--ǰ����
	sFieldName nvarchar(50) ,	--�ֶ�����
	sJudgmentSymbol nvarchar(10) ,	--��ѯ����
	oQueryValue nvarchar(4000) ,	--��ѯֵ
	sJoin nvarchar(10) ,	--��������
	sBracketsAfter nvarchar(10) ,	--������
	sFieldType nvarchar(30) ,	--�ֶ�����
	sBasicCode nvarchar(30) ,	--������������
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
