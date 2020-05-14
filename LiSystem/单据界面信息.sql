
drop table LiListButton
drop table LiEvent
drop table LiButton
drop table LiControl
drop table LiButtonGroup
drop table LiControlGroup
drop table LiPanel
drop table LiForm

create table LiForm(
	id int identity(1,1) primary key not null,
	[name]  nvarchar(30),	--名称
	[text] nvarchar(20),
	height int default 0,
	width int default 0,
	keyFieldName nvarchar(30),
	codeFieldName nvarchar(30),
	statusFieldName nvarchar(30),
	formType nvarchar(20),
	systemCode nvarchar(20),
	dCreateDate datetime default getdate()
)
--alter table LiForm add statusFieldName nvarchar(30)
--alter table LiForm add keyFieldName nvarchar(30)
--alter table LiForm add codeFieldName nvarchar(30)
--alter table LiForm add formType nvarchar(20)
--alter table LiForm add systemCode nvarchar(20)

SET IDENTITY_Insert LiForm ON 
insert into LiForm (id,name, text, height, width)
select 1,'单据1','单据标题1',800,800
SET IDENTITY_Insert LiForm OFF

create table LiPanel(
	id int identity(1,1) primary key not null,
	formModelId int REFERENCES LiForm(id),
	dock nvarchar(10),
	[type] nvarchar(10),
	[name]  nvarchar(30),	--名称
	[text] nvarchar(20),
	height int default 0,
	width int default 0,

	tableName nvarchar(30),
	primaryKeyName nvarchar(30),
	foreigntKeyName nvarchar(30),
	entityColumnName nvarchar(30),
	childEntityColumnNames nvarchar(300),
	keyType nvarchar(20),
	
	parentTableName nvarchar(30),
	parentPrimaryKeyName nvarchar(30),

	dCreateDate datetime default getdate()
)
--alter table LiPanel add parentTableName nvarchar(30)
--alter table LiPanel add parentPrimaryKeyName nvarchar(30)
--alter table LiPanel add entityColumnName nvarchar(30)
--alter table LiPanel add childEntityColumnNames nvarchar(300)

SET IDENTITY_Insert LiPanel ON 
insert into LiPanel (id,formModelId, dock, [type], name, text, height, width)
select 1,1,'top','Basic','表头1','表头标题1',100,800
union all
select 2,1,'fill','Grid','表体1','表体标题1',200,800
union all
select 3,1,'bottom','Basic','表脚1','表脚标题1',40,800
SET IDENTITY_Insert LiPanel OFF

create table LiControlGroup(
	id int identity(1,1) primary key not null,
	panelModelId int REFERENCES LiPanel(id),
	[text] nvarchar(20),
	[name] nvarchar(30),
	autoAllocation bit default 0,
	dCreateDate datetime default getdate()
)

SET IDENTITY_Insert LiControlGroup ON 
insert LiControlGroup (id, panelModelId, text, name, autoAllocation)
select 1,1,'表头控件组标题1','表头控件组1',0
union all
select 2,1,'表头控件组标题2','表头控件组2',0
union all
select 3,2,'表体控件组标题1','datas',0
union all
select 4,3,'表脚控件组标题1','表脚控件组1',0
SET IDENTITY_Insert LiControlGroup OFF

create table LiButtonGroup(
	id int identity(1,1) primary key not null,
	formId int REFERENCES LiForm(id),
	panelModelId int REFERENCES LiPanel(id),
	controlGroupId int REFERENCES LiControlGroup(id),
	[text] nvarchar(20),
	[name] nvarchar(30),
	allowMinimize bit default 0,
	allowTextClipping bit default 0,
	dCreateDate datetime default getdate()
)

SET IDENTITY_Insert LiButtonGroup ON
insert into LiButtonGroup (id, formId,panelModelId,controlGroupId, text, name, allowMinimize, allowTextClipping)
select 1,1,null,null,'单据按钮标题1','单据按钮1',0,0
union all
select 2,null,null,3,'表体按钮标题1','表体按钮1',0,0
SET IDENTITY_Insert LiButtonGroup OFF

create table LiButton(
	id int identity(1,1) primary key not null,
	buttonGroupId int REFERENCES LiButtonGroup(id),
	caption nvarchar(20),	--标题
	[name]  nvarchar(30),	--名称
	iconsize nvarchar(20),	--图标大小
	categoryGuid nvarchar(50),	--类别ID newid form工具栏需要
	icon nvarchar(30),	--图标名称
	voucherStatus nvarchar(30),	--单据状态
	previousVoucherStatus nvarchar(30),	--上个单据状态
	statusFieldName nvarchar(30),	--状态字段
	bClearStatus bit default 0,--清除状态
	entityKey nvarchar(30),	--表体Key
	dCreateDate datetime default getdate()
)
--alter table LiButton add previousVoucherStatus nvarchar(30)
--alter table LiButton add statusFieldName nvarchar(50)
--alter table LiButton add bClearStatus bit default 0
--alter table LiButton add voucherStatus nvarchar(30)
--alter table LiButton add entityKey nvarchar(30)
--alter table LiButton add userFieldName nvarchar(30)
--alter table LiButton add dateFieldName nvarchar(30)
--alter table LiButton alter column icon nvarchar(100)

SET IDENTITY_Insert LiButton ON
declare @newid nvarchar(50)
set @newid = cast(newid() as nvarchar(50))
insert into LiButton (id, buttonGroupId, caption, name, iconsize, categoryGuid,icon)
select 1,1,'新增1','new','Large',@newid,'icon'
union all
select 2,1,'保存1','save','SmallWithText',@newid,'icon'
union all
select 3,1,'修改1','modify','SmallWithText',@newid,'icon'
union all
select 4,1,'退出1','exit','Large',@newid,'icon'

insert into LiButton (id, buttonGroupId, caption, name, iconsize, categoryGuid,icon)
select 5,2,'新增行1','newrow',null,null,'icon'
union all
select 6,2,'删除行1','deleterow',null,null,'icon'
union all
select 7,2,'插入行1','insertrow',null,null,'icon'
SET IDENTITY_Insert LiButton OFF

create table LiControl(
	id int identity(1,1) primary key not null,
	controlGroupId int REFERENCES LiControlGroup(id),
	[name]  nvarchar(30),	--名称
	[text] nvarchar(20),
	[length] int default 0,
	[scale] int default 0,
	[height] int default 0,
	[width] int default 0,
	col int default 0 ,
	[row] int default 0,
	controltype nvarchar(100),
	bIsNull bit default 0,
	bReadOnly bit default 0,
	bRequired bit default 0,
	defaultVaule nvarchar(max),
	basicInfoKey nvarchar(50),
	basicInfoTableKey nvarchar(50),
	basicInfoShowFieldName nvarchar(50),
	basicInfoAssistType nvarchar(50),
	basicInfoAssistFieldName nvarchar(50),
	gridlookUpEditShowModelJson nvarchar(2000),
	basicInfoShowMode nvarchar(20),
	dictInfoType nvarchar(50),
	bVisible bit default 1,
	bVisibleInList bit default 1,
	dCreateDate datetime default getdate()
)
--alter table LiControl add bVisible bit default 1
--alter table LiControl add bVisibleInList bit default 1
--alter table LiControl add basicInfoShowFieldName nvarchar(50)
--alter table LiControl add basicInfoAssistType nvarchar(50)
--alter table LiControl add dictInfoType nvarchar(50)
--alter table LiControl add gridlookUpEditShowModelJson nvarchar(2000)
--alter table LiControl add basicInfoShowMode nvarchar(20)
--alter table LiControl add bReadOnly bit default 0
--alter table LiControl add bRequired bit default 0

create table LiEvent(
	id int identity(1,1) primary key not null,
	formId int REFERENCES LiForm(id),
	panelModelId int REFERENCES LiPanel(id),
	controlGroupId int REFERENCES LiControlGroup(id),
	buttonId int REFERENCES LiButton(id),
	fullName nvarchar(300),
	assemblyName nvarchar(50),
	eventMemo nvarchar(300),
	dCreateDate datetime default getdate()
)
--alter table LiEvent add listButtonId int REFERENCES LiListButton(id)

create table LiListButton(
	id int identity(1,1) primary key not null,
	formId int REFERENCES LiForm(id),
	caption nvarchar(20),	--标题
	[name]  nvarchar(30),	--名称
	iconsize nvarchar(20),	--图标大小
	categoryGuid nvarchar(50),	--类别ID newid form工具栏需要
	icon nvarchar(30),	--图标名称
	voucherStatus nvarchar(30),	--单据状态
	dCreateDate datetime default getdate()
)
--alter table LiListButton add userFieldName nvarchar(30)
--alter table LiListButton add dateFieldName nvarchar(30)
--alter table LiListButton alter column icon nvarchar(100)

--alter table LiControl add [width] int default 0
--alter table LiControl add [bIsNull] bit default 0
--alter table LiControl add defaultVaule nvarchar(max)
--alter table LiControl add scale int default 0

SET IDENTITY_Insert LiControl ON
insert into LiControl(id, controlGroupId, name, text, length, height, col, row, controltype,basicInfoKey,basicInfoTableKey,basicInfoAssistFieldName)
select 1,1,'cCode','编号1',300,24,1,1,'TextEdit',null,null,null
union all
select 2,1,'dDate','日期1',300,24,2,1,'DateEdit',null,null,null
union all
select 3,1,'cStatus','状态1',300,24,3,1,'TextEdit',null,null,null
union all
select 4,1,'cCustomer','客户1',300,24,1,2,'GridLookUpEditRef','Customer','cCustomerCode',null
union all
select 5,1,'cSaler','销售员1',300,24,3,2,'GridLookUpEditRef','Person','cPersonCode',null
union all
select 6,2,'iQtySum','总数量1',300,24,1,2,'CalcEdit',null,null,null
union all
select 7,2,'iAmountSum','总金额1',300,24,2,2,'CalcEdit',null,null,null
union all

select 8,3,'iRow','行号1',300,24,1,1,'TextEdit',null,null,null
union all
select 9,3,'iMaterial','物料1',300,24,2,1,'GridLookUpEditRef','Material','cMaterialCode',null
union all
select 10,3,'iQty','数量1',300,24,4,1,'CalcEdit',null,null,null
union all
select 11,3,'iAmount','金额1',300,24,5,1,'CalcEdit',null,null,null
union all


select 12,4,'cMaker','制单人1',300,24,1,1,'TextEdit',null,null,null
union all
select 13,4,'dMakeDate','制单日期1',300,24,2,1,'DateEdit',null,null,null
union all
select 14,4,'cModifer','修改人1',300,24,3,1,'TextEdit',null,null,null
union all
select 15,4,'dModifyDate','修改日期1',300,24,4,1,'DateEdit',null,null,null
union all
select 16,1,'cCustomerName','客户名称1',300,24,2,2,'GridLookUpEditRefAssist','Customer',null,'cCustomerName'
union all
select 17,3,'cMaterialName','物料名称1',300,24,3,1,'GridLookUpEditRefAssist','Material',null,'cMaterialName'
SET IDENTITY_Insert LiControl OFF





--delete LiEvent where listButtonId  in (select id from LiListButton where formId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11))
--delete LiListButton where formId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)

--delete LiControl where controlGroupId in (select id from LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)))

--delete LiEvent where buttonId in (select id from LiButton where buttonGroupId in (select id from LiButtonGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11))))
--delete LiEvent where buttonId in (select id from LiButton where buttonGroupId in (select id from LiButtonGroup where formId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)))
--delete LiEvent where buttonId in (select id from LiButton where buttonGroupId in (select id from LiButtonGroup where controlGroupId in (select id from LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)))))

--delete LiButton where buttonGroupId in (select id from LiButtonGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)))
--delete LiButton where buttonGroupId in (select id from LiButtonGroup where formId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11))
--delete LiButton where buttonGroupId in (select id from LiButtonGroup where controlGroupId in (select id from LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11))))

--delete LiButtonGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11))
--delete LiButtonGroup where formId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)
--delete LiButtonGroup where controlGroupId in (select id from LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)))
--delete LiControlGroup where panelModelId in (select id from LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11))
--delete LiPanel where formModelId in (select id from LiForm where [name] = 'SaleInvoice' and id != 11)
--delete LiForm where [name] = 'SaleInvoice' and id != 11