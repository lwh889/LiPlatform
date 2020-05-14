drop table LiMessage

create table LiMessage(
	id int identity(1,1) primary key not null,
	messageType nvarchar(20),	--��Ϣ����
	messageDate datetime2,	--��Ϣʱ��
	messageContent nvarchar(500),	--��Ϣ����
	messageRead bit default 0,--�Ƿ�����
	voucherFlowId int,
	flowVersionId int,
	flowVersionNumber nvarchar(30),	--���̱���
	flowCode nvarchar(30),	--���̱���
	flowName nvarchar(50),	--��������
	entityKey nvarchar(30),	--���ݱ���
	entityName nvarchar(50),	--��������
	
	voucherId nvarchar(30),
	voucherCode nvarchar(30),	--���ݱ��

	userCode nvarchar(30),
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

--alter table LiMessage add messageRead bit default 0
