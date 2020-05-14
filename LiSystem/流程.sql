
drop table LiFlowCondition
drop table LiFlowConnector
drop table LiFlowUser
drop table LiFlowNode
drop table LiFlow

create table LiFlow(
	id int identity(1,1) primary key not null,
	flowCode nvarchar(30),	--���̱���
	flowName nvarchar(50),	--��������
	entityKey nvarchar(30),	--���ݱ���
	entityName nvarchar(50),	--��������
	bDefault bit default 0,	--�Ƿ�Ĭ��
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

create table LiFlowNode(
	id int identity(1,1) primary key not null,
	flowId int REFERENCES LiFlow(id),
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
--alter table LiFlowNode add flowNodeInformation nvarchar(500)

create table LiFlowUser(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiFlowNode(id),
	userCode nvarchar(30),	--�û�����
	userName nvarchar(50),	--�û�����
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)


create table LiFlowConnector(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiFlowNode(id),
	flowNodeCodeTo nvarchar(30),	--�ڵ����
	flowNodeNameTo nvarchar(50),	--�ڵ�����
	flowNodeFormIndex int default 0,
	flowNodeToIndex int default 3,

	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
--alter table LiFlowConnector add flowNodeFormIndex int default 0
--alter table LiFlowConnector add flowNodeToIndex int default 3


create table LiFlowCondition(
	id int identity(1,1) primary key,
	flowConnectorId int REFERENCES LiFlowConnector(id),
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
