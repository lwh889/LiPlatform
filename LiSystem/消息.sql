drop table LiMessage

create table LiMessage(
	id int identity(1,1) primary key not null,
	messageType nvarchar(20),	--消息类型
	messageDate datetime2,	--消息时间
	messageContent nvarchar(500),	--消息内容
	messageRead bit default 0,--是否已阅
	voucherFlowId int,
	flowVersionId int,
	flowVersionNumber nvarchar(30),	--流程编码
	flowCode nvarchar(30),	--流程编码
	flowName nvarchar(50),	--流程名称
	entityKey nvarchar(30),	--单据编码
	entityName nvarchar(50),	--单据名称
	
	voucherId nvarchar(30),
	voucherCode nvarchar(30),	--单据编号

	userCode nvarchar(30),
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

--alter table LiMessage add messageRead bit default 0
