drop table LiVoucherFlowStep
drop table LiVoucherFlow
--流程
create table LiVoucherFlow(
	id int identity(1,1) primary key not null,
	flowVersionId int not null,--流程版本ID
	flowVersionNumber nvarchar(50),	--流程版本号
	flowCode nvarchar(30),	--流程编码
	flowName nvarchar(30),	--流程名称
	entityKey nvarchar(30),	--单据编码
	entityName nvarchar(30),	--单据名称
	voucherId int,	--单据ID
	voucherCode nvarchar(30),	--单据编号
	flowStatus nvarchar(30),	--流程状态
	flowTitle nvarchar(30),	--流程标题
	flowStartDate datetime2,	--流程开始时间
	flowEndDate datetime2,	--流程结束时间
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
	)

--流程步骤
create table LiVoucherFlowStep(
	id int identity(1,1) primary key,
	flowVoucherId int REFERENCES LiVoucherFlow(id),
	flowSeq int,--流程顺序
	flowVersionNodeId int,--流程下一步
	flowVersionNextStepNodeId int,--流程下一步
	flowUserCode nvarchar(30),	--流程用户编码
	flowUserName nvarchar(30),	--流程用户编码
	flowContent nvarchar(30),	--流程内容
	flowApprovalType nvarchar(20),	--流程内容
	flowStatus nvarchar(30),	--流程状态
	flowDate nvarchar(30),	--流程时间

	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

--alter table LiVoucherFlowStep add flowApprovalType nvarchar(20)
--alter table LiVoucherFlowStep add flowVersionNextStepNodeId int