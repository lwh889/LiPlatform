
drop table LiFlowCondition
drop table LiFlowConnector
drop table LiFlowUser
drop table LiFlowNode
drop table LiFlow

create table LiFlow(
	id int identity(1,1) primary key not null,
	flowCode nvarchar(30),	--流程编码
	flowName nvarchar(50),	--流程名称
	entityKey nvarchar(30),	--单据编码
	entityName nvarchar(50),	--单据名称
	bDefault bit default 0,	--是否默认
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

create table LiFlowNode(
	id int identity(1,1) primary key not null,
	flowId int REFERENCES LiFlow(id),
	flowNodeCode nvarchar(30),	--流程节点编码
	flowNodeName nvarchar(50),	--流程节点名称
	flowNodeType nvarchar(30),	--流程节点类型
	flowNodeInformation nvarchar(500),	--流程信息
	X int default 0,
	Y int default 0,
	width int default 0,
	height int default 0,
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
--alter table LiFlowNode add flowNodeInformation nvarchar(500)

create table LiFlowUser(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiFlowNode(id),
	userCode nvarchar(30),	--用户编码
	userName nvarchar(50),	--用户名称
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)


create table LiFlowConnector(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiFlowNode(id),
	flowNodeCodeTo nvarchar(30),	--节点编码
	flowNodeNameTo nvarchar(50),	--节点名称
	flowNodeFormIndex int default 0,
	flowNodeToIndex int default 3,

	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
--alter table LiFlowConnector add flowNodeFormIndex int default 0
--alter table LiFlowConnector add flowNodeToIndex int default 3


create table LiFlowCondition(
	id int identity(1,1) primary key,
	flowConnectorId int REFERENCES LiFlowConnector(id),
	sBracketsBefore nvarchar(10) ,	--前括号
	sFieldName nvarchar(50) ,	--字段名称
	sJudgmentSymbol nvarchar(10) ,	--查询符号
	oQueryValue nvarchar(4000) ,	--查询值
	sJoin nvarchar(10) ,	--连接条件
	sBracketsAfter nvarchar(10) ,	--后括号
	sFieldType nvarchar(30) ,	--字段类型
	sBasicCode nvarchar(30) ,	--基础档案编码
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
