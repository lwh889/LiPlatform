
drop table LiVoucherCode
create table LiVoucherCode(
	id int identity(1,1) primary key,
	
	entityKey nvarchar(50),--实体Key
	bDefault bit default 0,--默认方案
	[name] nvarchar(50),--方案名称
	prefixName nvarchar(10),--前缀
	fieldTextName nvarchar(50),--单据文本字段
	fieldDateName nvarchar(50),--单据日期字段
	dateTimeFormat nvarchar(50),	--日期格式
	iZero int default 1,	--左边补0
	iStep int default 1,	--流水号步位
	flowNoRange  nvarchar(10) default 'DAY', --流水号范围
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

drop table LiFlowNo
create table LiFlowNo(
	id int identity(1,1) primary key,
	
	entityKey nvarchar(50),--实体Key
	voucherCodeName nvarchar(50),
	
	iYear int,
	iMonth int,
	iDay int,
	iFlow int,

	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
