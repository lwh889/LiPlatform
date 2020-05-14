
drop table LiGeneralEvent
--常用事件
create table LiGeneralEvent(
	id int identity(1,1) primary key not null,
	voucherType nvarchar(30),
	[eventType]  nvarchar(30),	--名称
	[eventName] nvarchar(20),
	eventFullName nvarchar(300),
	eventAssemblyName nvarchar(50),
	dCreateDate datetime default getdate()
)

insert into LiGeneralEvent (voucherType,eventType,eventName,eventFullName,eventAssemblyName)
select '单据','按钮','新增','LiForm.Event.EventForm.LiEventNew','LiForm'
union all
select '单据','按钮','编辑','LiForm.Event.EventForm.LiEventEdit','LiForm'
union all
select '单据','按钮','保存','LiForm.Event.EventForm.LiEventSave','LiForm'
union all
select '单据','按钮','退出','LiForm.Event.EventForm.LiEventExit','LiForm'
union all
select '单据','按钮','增加行','LiForm.Event.EventForm.LiEventNewRowData','LiForm'
union all
select '单据','按钮','插入行','LiForm.Event.EventForm.LiEventInsertRowData','LiForm'
union all
select '单据','按钮','删除行','LiForm.Event.EventForm.LiEventDeleteRowData','LiForm'
union all
select '列表','按钮','查询','LiForm.Event.EventListForm.LiEventListQuery','LiForm'
union all
select '列表','按钮','精确查询','LiForm.Event.EventListForm.LiEventListPreciseQuery','LiForm'
union all
select '列表','按钮','刷新','LiForm.Event.EventListForm.LiEventListRefresh','LiForm'
union all
select '列表','按钮','新增','LiForm.Event.EventListForm.LiEventListNew','LiForm'
union all
select '列表','按钮','编辑','LiForm.Event.EventListForm.LiEventListEdit','LiForm'
union all
select '列表','按钮','退出','LiForm.Event.EventListForm.LiEventListExit','LiForm'
union all
select '单据','按钮','引用','LiForm.Event.EventForm.LiEventRef','LiForm'
union all
select '列表','按钮','引用','LiForm.Event.EventListForm.LiEventListRef','LiForm'
union all
select '单据','按钮','下推','LiForm.Event.EventForm.LiEventPush','LiForm'
union all
select '列表','按钮','下推','LiForm.Event.EventListForm.LiEventListPush','LiForm'
union all
select '单据','按钮','提交','LiForm.Event.EventForm.LiEventSubmit','LiForm'
union all
select '单据','按钮','反提交','LiForm.Event.EventForm.LiEventUnSubmit','LiForm'
union all
select '单据','按钮','审核','LiForm.Event.EventForm.LiEventAudit','LiForm'
union all
select '单据','按钮','反审核','LiForm.Event.EventForm.LiEventUnAudit','LiForm'
