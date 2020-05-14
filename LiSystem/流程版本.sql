
drop table LiVersionFlowCondition
drop table LiVersionFlowConnector
drop table LiVersionFlowUser
drop table LiVersionFlowNode
drop table LiVersionFlow

create table LiVersionFlow(
	id int identity(1,1) primary key not null,
	flowVersionNumber nvarchar(30),	--流程编码
	flowCode nvarchar(30),	--流程编码
	flowName nvarchar(50),	--流程名称
	entityKey nvarchar(30),	--单据编码
	entityName nvarchar(50),	--单据名称
	bDefault bit default 0,	--是否默认
	openStatus nvarchar(30) default '禁止',--开启状态
	flowVersionDate datetime2,	--流程日期
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

--alter table LiVersionFlow add openStatus nvarchar(30) default '禁止'
create table LiVersionFlowNode(
	id int identity(1,1) primary key not null,
	flowId int REFERENCES LiVersionFlow(id),
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
--alter table LiVersionFlowNode add flowNodeInformation nvarchar(500)


create table LiVersionFlowUser(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiVersionFlowNode(id),
	userCode nvarchar(30),	--用户编码
	userName nvarchar(50),	--用户名称
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)


create table LiVersionFlowConnector(
	id int identity(1,1) primary key not null,
	flowNodeId int REFERENCES LiVersionFlowNode(id),
	flowNodeCodeTo nvarchar(30),	--节点编码
	flowNodeNameTo nvarchar(50),	--节点名称
	flowNodeFormIndex int default 0,
	flowNodeToIndex int default 3,

	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
--alter table LiFlowConnector add flowNodeFormIndex int default 0
--alter table LiFlowConnector add flowNodeToIndex int default 3


create table LiVersionFlowCondition(
	id int identity(1,1) primary key,
	flowConnectorId int REFERENCES LiVersionFlowConnector(id),
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
