
drop table LiGeneralEvent
--�����¼�
create table LiGeneralEvent(
	id int identity(1,1) primary key not null,
	voucherType nvarchar(30),
	[eventType]  nvarchar(30),	--����
	[eventName] nvarchar(20),
	eventFullName nvarchar(300),
	eventAssemblyName nvarchar(50),
	dCreateDate datetime default getdate()
)

insert into LiGeneralEvent (voucherType,eventType,eventName,eventFullName,eventAssemblyName)
select '����','��ť','����','LiForm.Event.EventForm.LiEventNew','LiForm'
union all
select '����','��ť','�༭','LiForm.Event.EventForm.LiEventEdit','LiForm'
union all
select '����','��ť','����','LiForm.Event.EventForm.LiEventSave','LiForm'
union all
select '����','��ť','�˳�','LiForm.Event.EventForm.LiEventExit','LiForm'
union all
select '����','��ť','������','LiForm.Event.EventForm.LiEventNewRowData','LiForm'
union all
select '����','��ť','������','LiForm.Event.EventForm.LiEventInsertRowData','LiForm'
union all
select '����','��ť','ɾ����','LiForm.Event.EventForm.LiEventDeleteRowData','LiForm'
union all
select '�б�','��ť','��ѯ','LiForm.Event.EventListForm.LiEventListQuery','LiForm'
union all
select '�б�','��ť','��ȷ��ѯ','LiForm.Event.EventListForm.LiEventListPreciseQuery','LiForm'
union all
select '�б�','��ť','ˢ��','LiForm.Event.EventListForm.LiEventListRefresh','LiForm'
union all
select '�б�','��ť','����','LiForm.Event.EventListForm.LiEventListNew','LiForm'
union all
select '�б�','��ť','�༭','LiForm.Event.EventListForm.LiEventListEdit','LiForm'
union all
select '�б�','��ť','�˳�','LiForm.Event.EventListForm.LiEventListExit','LiForm'
union all
select '����','��ť','����','LiForm.Event.EventForm.LiEventRef','LiForm'
union all
select '�б�','��ť','����','LiForm.Event.EventListForm.LiEventListRef','LiForm'
union all
select '����','��ť','����','LiForm.Event.EventForm.LiEventPush','LiForm'
union all
select '�б�','��ť','����','LiForm.Event.EventListForm.LiEventListPush','LiForm'
union all
select '����','��ť','�ύ','LiForm.Event.EventForm.LiEventSubmit','LiForm'
union all
select '����','��ť','���ύ','LiForm.Event.EventForm.LiEventUnSubmit','LiForm'
union all
select '����','��ť','���','LiForm.Event.EventForm.LiEventAudit','LiForm'
union all
select '����','��ť','�����','LiForm.Event.EventForm.LiEventUnAudit','LiForm'
