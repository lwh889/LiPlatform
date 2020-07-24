
drop table LiReportEvent
drop table LiReportButton
drop table LiReportField
drop table LiReport

create table LiReport(
	id int identity(1,1) primary key,
	reportKey nvarchar(30) not null,
	reportName nvarchar(30),
	bColumnGroup bit default 0,
	systemCode nvarchar(10),
	menuCode int,
	dataBaseName nvarchar(30),
	reportSql nvarchar(max),
	reportCountSql nvarchar(max),
	createDate datetime default getdate()
)

--alter table LiReport add dataBaseName nvarchar(30)
--alter table LiReport add systemCode nvarchar(10)
--alter table LiReport add menuCode int
--alter table LiReport add bColumnGroup bit default 0

create table LiReportField(
	id int identity(1,1) primary key,
	reportId int REFERENCES LiReport(id),
	
	columnName nvarchar(50),	--字段名
	columnCaption nvarchar(50),	--列名称
	columnType nvarchar(100),	--列名称
	iColumnWidth int,	--列宽
	bColumnDisplay bit,	--是否显示列
	iColumnIndex int,	--列索引
	bQuery bit,	--是否查询

	bOrderBy bit,	--是否排序
	orderBy nvarchar(10),	--排序升降
	iOrderByIndex int,	--排序顺序
	
	iDisplayFormatType  int default 0,--时间，数字，自定义？
	displayFormat nvarchar(30),	--显示格式
	bColumnGroup bit,	--是否汇总
	columnGroupFormat nvarchar(30),	--汇总格式
	
	controlType nvarchar(50),	--控件类型
	basicInfoKey nvarchar(30),	--引用档案Key
	basicInfoTableKey nvarchar(50),	--引用档案显示主键
	basicInfoShowFieldName nvarchar(50),	--引用档案显示名称
	basicInfoShowMode nvarchar(30),	--引用档案显示模式
	dictInfoType nvarchar(30),	--字典档案Key
	gridlookUpEditShowModelJson nvarchar(1000),	--引用档案配置信息

	createDate datetime default getdate()
)
--alter table LiReportField add bOrderBy bit
--alter table LiReportField add orderBy nvarchar(10)
--alter table LiReportField add iOrderByIndex int
--alter table LiReportField add controlType nvarchar(50)
--alter table LiReportField add basicInfoKey nvarchar(30)
--alter table LiReportField add basicInfoShowFieldName nvarchar(50)
--alter table LiReportField add basicInfoShowMode nvarchar(30)
--alter table LiReportField add dictInfoType nvarchar(30)
--alter table LiReportField add gridlookUpEditShowModelJson nvarchar(1000)
--alter table LiReportField add basicInfoTableKey nvarchar(50)


create table LiReportButton(
	id int identity(1,1) primary key,
	reportId int REFERENCES LiReport(id),
	
	caption nvarchar(20),	--标题
	name nvarchar(20),	--名称
	iconsize nvarchar(50),	--按钮类型
	categoryGuid nvarchar(50),	--类别ID
	icon nvarchar(100),	--图标
	iIndex int,--顺序
	createDate datetime default getdate()
)


create table LiReportEvent(
	id int identity(1,1) primary key,
	reportButtonId int REFERENCES LiReportButton(id),
	
	fullName nvarchar(100),	--全名称
	assemblyName nvarchar(100),	--程序集
	eventMemo nvarchar(100),	--备注
	)