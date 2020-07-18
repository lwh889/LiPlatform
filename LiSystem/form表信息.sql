drop table ColumnInfo
drop table TableInfo
create table TableInfo(
	id int identity(1,1) primary key,
	dataBaseName nvarchar(50),--���ݿ�����
	entityType nvarchar(50), --ʵ�����ͣ����ݣ�������
	entityKey nvarchar(50),	--��������
	entityName nvarchar(50),	--��������
	entityOrder nvarchar(20),	--���ݵ�������
	entityColumnName nvarchar(50),	--�ӱ�������ģ�͵����ƣ��������ظ�������
	tableName nvarchar(50),	--����
	tableAliasName nvarchar(50),	--�����
	tableAbbName nvarchar(50),	--����
	tableDesc nvarchar(255),	--������
	className nvarchar(50),	--json����ת����ʵ�������
	keyName nvarchar(50),	--������
	bDefaultBody bit default 0,	--Ĭ�ϱ���
	childTableEntityColumnName nvarchar(500),	--����� --��Ҫ
	
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)
--alter table TableInfo add bDefaultBody bit default 0

create table ColumnInfo(
	id int identity(1,1) primary key,
	fid int REFERENCES TableInfo(id),
	columnName nvarchar(50),	--����
	columnAbbName nvarchar(50),	--�м��
	columnType nvarchar(50),	--������
	controlType nvarchar(50),	--�ؼ�����
	length int default 0,	--���ݳ���
	primaryKey bit default 0,	--����
	foreignKey bit default 0,	--���
	relationshipType int default 0,	--��ϵ���ͣ�һ�Զ࣬һ��һ,0 �ޣ�1 һ��һ��2 һ�Զ࣬3 ����
	databaseGeneratedType int default 0,	--�е���������,0 ������1 ��������2 �������ã�3 newid
	
	primaryKeyName  nvarchar(50),	--�ӱ��������Ҫд����������
	primaryKeyDatabaseGenerated int default 0,	--�ӱ��������Ҫд��������������
	primaryKeyTableName nvarchar(50),	--�ӱ��������Ҫд��������

	columnOrder int default 0, --��˳��
	columnScale int default 0, --��С��λ
	columnIsNull bit default 1,--���Ƿ�Ϊ��
	columnDefaultValue nvarchar(max),--��Ĭ��ֵ
	columnWidth int default 0,--�п��

	bSearchColumns bit default 0,	--�����Ƿ������
	bDisplayColumn bit default 1,	--�����Ƿ���ʾ

	bVisible bit default 1, --�Ƿ���ʾ
	
	bExtendField bit default 0, --�Ƿ���չ�ֶα�
	extendTableName nvarchar(50),	--��չ�ֶα���
	extendTableKeyFieldName nvarchar(50),	--��չ�ֶα������
	extendRelationTableKeyFieldName nvarchar(50),	--��չ����������

	basicInfoType nvarchar(50),	--������������
	dictInfoType nvarchar(50),	--�ֵ�����
	basicInfoShowFieldName nvarchar(50),	--����������ʾ����
	basicInfoRelationFieldName nvarchar(50),	--�������������ֶ���
	basicInfoKeyFieldName nvarchar(50),	--�������������ֶ�
	gridlookUpEditShowModelJson nvarchar(4000),
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
)

--alter table ColumnInfo add basicInfoType nvarchar(50)	--������������
--alter table ColumnInfo add dictInfoType nvarchar(50)
--alter table ColumnInfo add basicInfoShowFieldName nvarchar(50)
--alter table ColumnInfo add basicInfoRelationFieldName nvarchar(50)
--alter table ColumnInfo add basicInfoKeyFieldName nvarchar(50)
--alter table ColumnInfo add controlType nvarchar(50)
--alter table ColumnInfo add columnWidth int default 0
--alter table ColumnInfo add gridlookUpEditShowModelJson nvarchar(4000)
--alter table ColumnInfo add bExtendField bit default 0
--alter table ColumnInfo add extendTableName nvarchar(50)
--alter table ColumnInfo add extendTableKeyFieldName nvarchar(50)
--alter table ColumnInfo add extendRelationTableKeyFieldName nvarchar(50)

SET IDENTITY_Insert TableInfo ON
insert into TableInfo (id,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
--�����Ϣ��
--values ('entity1','master',null,'spring_user', 'user1', '�û���', null,'JsonModel', 'id',null, getdate())
select 1,'form1','master',null,'LiForm','liform1','Form��ܱ�', null,'JsonModel','id','panels,buttonGroups,controlGroups',getdate()
union all
select 2,'form1','slave', 'panels','LiPanel','lipanel1','����', null,'JsonModel','id','controlGroups,buttonGroups',getdate()
union all
select 3,'form1','slave','controlGroups','LiControlGroup','licontrolgroup1','�ؼ���', null,'JsonModel','id','controls',getdate()
union all
select 4,'form1','slave','buttonGroups','LiButtonGroup','libuttongroup1','��ť��', null,'JsonModel','id','buttons',getdate()
union all
select 5,'form1','slave', 'buttons','LiButton','libutton1','��ť', null,'JsonModel','id',null,getdate()
union all
select 6,'form1','slave','controls','LiControl','licontrol1','�ؼ�', null,'JsonModel','id',null,getdate()
union all
select 17,'form1','slave','events','LiEvent','liEvent1','�¼���', null,'JsonModel','id','events',getdate()
union all
select 61,'form1','slave','events','LiControlEvent','liEvent1','�ؼ��¼���', null,'JsonModel','id','controlEvents',getdate()
union all
select 19,'form1','slave','listButtons','LiListButton','liListButton1','�б�ť��', null,'JsonModel','id','listButtons',getdate()
union all
select 7,'liManageMeum','master',null,'LiManageMeum','limanagemeum1','ϵͳ����˵�', null,'JsonModel','id',null,getdate()
--���ݿ���
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 8,'LiSystem','Basic','sysDatabases','master',null,'V_SysDatabases','sysDatabases','���ݿ⵵��', null,'JsonModel','dbid',null,getdate()
--״̬
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 9,'LiSystem','Basic','liVoucherStatus','master',null,'LiVoucherStatus','liVoucherStatus','����״̬��', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 10,'LiSystem','Basic','liVoucherStatus','slave','dataStatuss','LiStatus','liStatus','״̬��', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 11,'LiSystem','Basic','liVoucherStatus','slave','dataControlStatuss','LiControlStatus','liControlStatus','�ؼ�״̬��', null,'JsonModel','id',null,getdate()

--�ֵ�
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 12,'LiSystem','Basic','liDictGroup','master',null,'LiDictGroup','liDictGroup','�ֵ���', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 13,'LiSystem','Basic','liDict','master',null,'LiDict','liDict','�ֵ�', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 14,'LiSystem','Basic','liTableInfo','master',null,'TableInfo','liTableInfo','����Ϣ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 15,'LiSystem','Basic','liTableInfo','slave','datas','ColumnInfo','liColumnInfo','����Ϣ', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 16,'liAdminMeum','master',null,'LiAdminMeum','liAdminmeum','ҵ�����˵�', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 18,'liGeneralEvent','master',null,'LiGeneralEvent','liGeneralEvent','�����¼�', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 20,'LiSystem','Basic','liQueryScheme','master',null,'LiQueryScheme','liQueryScheme','��ѯ����', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 21,'LiSystem','Basic','liQueryScheme','slave','querys','LiQuery','liQuery','��ѯ����', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 22,'LiSystem','Basic','liQueryScheme','slave','entitys','LiEntity','liEntity','ʵ�弯��', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 23,'LiSystem','Basic','liQueryScheme','slave','fields','LiField','liField','�ֶμ���', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 24,'LiSystem','Basic','liUsers','master',null,'LiUsers','liUsers','�û���', null,'JsonModel','userCode',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 25,'LiSystem','Basic','liRoles','master',null,'LiRoles','liRoles','��ɫ��', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 26,'LiSystem','Basic','liAuthData','master',null,'LiAuthData','liAuthData','����Ȩ�ޱ�', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 27,'LiSystem','Basic','liAuth','master',null,'LiAuth','liAuth','Ȩ�ޱ�', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 28,'LiSystem','Basic','liUserRole','master',null,'LiUserRole','liUserRole','�û���ɫ��', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 29,'LiSystem','Basic','liVoucherCode','master',null,'LiVoucherCode','liVoucherCode','���ݱ��', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 30,'LiSystem','Basic','liConvert','master',null,'LiConvertHead','liConvertHead','ת����ͷ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 31,'LiSystem','Basic','liConvert','slave','datas','LiConvertBody','liConvertBody','ת������', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 32,'LiSystem','Basic','liConvert','slave','queryFields','LiField','liField','�ֶμ���', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 33,'LiSystem','Basic','liFlow','master',null,'LiFlow','liFlow','���̱�ͷ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 34,'LiSystem','Basic','liFlow','slave','nodes','LiFlowNode','liFlowNode','���̽ڵ�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 35,'LiSystem','Basic','liFlow','slave','users','LiFlowUser','liFlowUser','�����û�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 36,'LiSystem','Basic','liFlow','slave','connectors','LiFlowConnector','liFlowConnector','����ȥ��', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 37,'LiSystem','Basic','liFlow','slave','conditions','LiFlowCondition','liFlowCondition','��������', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 38,'LiSystem','Basic','liVersionFlow','master',null,'LiVersionFlow','liVersionFlow','���̰汾��ͷ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 39,'LiSystem','Basic','liVersionFlow','slave','nodes','LiVersionFlowNode','liVersionFlowNode','���̰汾�ڵ�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 40,'LiSystem','Basic','liVersionFlow','slave','users','LiVersionFlowUser','liVersionFlowUser','���̰汾�û�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 41,'LiSystem','Basic','liVersionFlow','slave','connectors','LiVersionFlowConnector','liVersionFlowConnector','���̰汾ȥ��', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 42,'LiSystem','Basic','liVersionFlow','slave','conditions','LiVersionFlowCondition','liVersionFlowCondition','���̰汾����', null,'JsonModel','id',null,getdate()
--update TableInfo set entityColumnName = 'queryFields' where 

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 43,'LiSystem','Basic','liVoucherFlow','master',null,'LiVoucherFlow','liVoucherFlow','��������', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 44,'LiSystem','Basic','liVoucherFlow','slave','datas','LiVoucherFlowStep','liVoucherFlowStep','���̲���', null,'JsonModel','id',null,getdate()



insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 45,'LiSystem','Basic','liVersionFlowNode','master',null,'LiVersionFlowNode','liVersionFlowNode','���̰汾�ڵ�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 46,'LiSystem','Basic','liVersionFlowNode','slave','users','LiVersionFlowUser','liVersionFlowUser','���̰汾�û�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 47,'LiSystem','Basic','liVersionFlowNode','slave','connectors','LiVersionFlowConnector','liVersionFlowConnector','���̰汾ȥ��', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 48,'LiSystem','Basic','liVersionFlowNode','slave','conditions','LiVersionFlowCondition','liVersionFlowCondition','���̰汾����', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 49,'LiSystem','Basic','liMessage','master',null,'LiMessage','liMessage','��Ϣ', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 50,'LiSystem','Basic','liProcedure','master',null,'ProcedureInfo','liProcedure','�洢���̽ӿ���Ϣ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 51,'LiSystem','Basic','liProcedure','slave','datas','ParamInfo','liParamInfo','�洢���̲�����Ϣ', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 52,'LiSystem','Basic','liSystemInfo','master',null,'LiSystemInfo','liSystemInfo','ϵͳ��Ϣ��', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 53,'LiSystem','Basic','liU8Voucher','master',null,'LiU8Voucher','liU8Voucher','U8API����', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 54,'LiSystem','Basic','liU8Voucher','slave','operations','LiU8Operation','liU8Operation','���ݲ�������', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 55,'LiSystem','Basic','liU8Voucher','slave','paramModels','LiU8Param','liU8Param','���ݲ���', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 56,'LiSystem','Basic','liU8Voucher','slave','fields','LiU8Field','liU8Field','�����ֶ�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 57,'LiSystem','Basic','liU8Voucher','slave','contexts','LiU8EnvContext','iU8EnvContext','�����ֶ�', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 58,'LiSystem','Basic','liPushForm','master',null,'LiPushForm','liPushForm','���Ʊ�ͷ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 59,'LiSystem','Basic','liPushForm','slave','pushListbuttons','LiPushListButton','liPushListButton','�б�ť', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 60,'LiSystem','Basic','liPushForm','slave','pushEvents','LiPushEvent','liPushEvent','�����¼�', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 62,'LiSystem','Basic','liReport','master',null,'LiReport','liPushForm','������Ϣ', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 63,'LiSystem','Basic','liReport','slave','datas','LiReportField','liReportField','�����ֶ�', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 64,'LiSystem','Basic','liReport','slave','buttons','LiReportButton','liReportButton','����ť', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 65,'LiSystem','Basic','liReport','slave','events','LiReportEvent','liReportEvent','����ť�¼�', null,'JsonModel','id',null,getdate()


SET IDENTITY_Insert TableInfo OFF

--LiForm
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 1, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 1, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'text','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 1, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 1, 'keyFieldName','�����ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'codeFieldName','�����ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'statusFieldName','״̬�ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'formType','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'systemCode','ϵͳ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 1, 'panels','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 1, 'buttonGroups','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 1, 'events','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 1, 'listButtons','���ݼ�','collection', -1,0,0,2,0,null,0,null



--LiPanel
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 2, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 2, 'formModelId','���','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 2, 'dock','ͣ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'type','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'text','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 2, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 2, 'tableName','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'primaryKeyName','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'foreigntKeyName','�������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'entityColumnName','�ӱ�������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'childEntityColumnNames','���м�������', 'narchar',300,0,0,0,0,null,0,null
union all
select 2, 'keyType','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'parentTableName','������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'parentPrimaryKeyName','����������', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'controlGroups','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 2, 'buttonGroups','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 2, 'events','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiControlGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 3, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 3, 'panelModelId','���','int',9,0,1,0,0,'id',1,'LiPanel'
union all
select 3, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 3, 'text','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 3, 'rowFieldName','�к��ֶ���', 'narchar',20,0,0,0,0,null,0,null
union all
select 3, 'autoAllocation','�Ƿ��Զ�����', 'bit',9,0,0,0,0,null,0,null
union all
select 3, 'controls','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 3, 'buttonGroups','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 3, 'events','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiButtonGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 4, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 4, 'formId','���','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 4, 'panelModelId','���','int',9,0,1,0,0,'id',1,'LiPanel'
union all
select 4, 'controlGroupId','���','int',9,0,1,0,0,'id',1,'LiControlGroup'
union all
select 4, 'text','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 4, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 4, 'allowMinimize','allowMinimize', 'bit',9,0,0,0,0,null,0,null
union all
select 4, 'allowTextClipping','allowTextClipping', 'bit',9,0,0,0,0,null,0,null
union all
select 4, 'buttons','���ݼ�','collection', -1,0,0,2,0,null,0,null

--select * from ColumnInfo where columnName = 'iIndex'
--LiButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 5, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 5, 'buttonGroupId','���','int',9,0,1,0,0,'id',1,'LiButtonGroup'
union all
select 5, 'iIndex','����', 'int',9,0,0,0,0,null,0,null
union all
select 5, 'caption','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'iconsize','ͼ���С', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'categoryGuid','���ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 5, 'icon','ͼ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'voucherStatus','����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'previousVoucherStatus','�ϸ�����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'statusFieldName','״̬�ֶ�', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'bClearStatus','���״̬', 'bit',30,0,0,0,0,null,0,null
union all
select 5, 'entityKey','����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'events','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiControl
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 6, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 6, 'controlGroupId','���','int',9,0,1,0,0,'id',1,'LiControlGroup'
union all
select 6, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 6, 'text','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 6, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'col','�к�', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'row','�к�', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'colIndex','�б�˳��', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'controltype','�ؼ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 6, 'length','����', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'scale','����', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'bIsNull','�Ƿ�Ϊ��', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'bReadOnly','�Ƿ�ֻ��', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'bRequired','�Ƿ����', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'defaultVaule','Ĭ��ֵ', 'nvarchar',5000,0,0,0,0,null,0,null
union all
select 6, 'controlDefaultVaule','�ؼ�Ĭ��ֵ', 'nvarchar',5000,0,0,0,0,null,0,null
union all
select 6, 'basicInfoKey','��������Kdy', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoTableKey','�������������ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoShowFieldName','����������ʾ�ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoAssistType','����������������', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoAssistFieldName','���������������ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'dictInfoType','�ֵ䵵������', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'bVisible','�����Ƿ���ʾ', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'bVisibleInList','�б��Ƿ���ʾ', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'gridlookUpEditShowModelJson','Json�ֶ�', 'narchar',200,0,0,0,0,null,0,null
union all
select 6, 'basicInfoShowMode','����������ʾģʽ', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'controlEvents','���ݼ�','collection', -1,0,0,2,0,null,0,null



--LiManageMeum
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 7, 'ID','����','int',9,1,0,0,1,null,0,null
union all
select 7, 'Code','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 7, 'Name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 7, 'systemCode','ϵͳ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 7, 'isGroup','�Ƿ���', 'bit',30,0,0,0,0,null,0,null
union all
select 7, 'isSystem','�Ƿ�ϵͳ', 'bit',30,0,0,0,0,null,0,null
union all
select 7, 'GroupId','��ID', 'int',30,0,0,0,0,null,0,null
union all
select 7, 'ParentID','��ID', 'int',9,0,0,0,0,null,0,null
union all
select 7, 'imageIndex','ͼ������', 'int',9,0,0,0,0,null,0,null
union all
select 7, 'iOrder','����','int',9,0,0,0,0,null,0,null


--V_SysDatabases
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
,columnOrder,columnScale,columnIsNull,columnDefaultValue,bSearchColumns,bDisplayColumn,bVisible) 
select 8, 'dbid','����','int',9,1,0,0,1,null,0,null
,1,0,0,null,0,0,0
union all
select 8, 'name','���ݿ�����','narchar',200,1,0,0,1,null,0,null
,2,0,0,null,1,1,1


--LiVoucherStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 9, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 9, 'code','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 9, 'name','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 9, 'dataStatuss','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 10, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 10, 'fid','���','int',9,0,1,0,0,'id',1,'LiVoucherStatus'
union all
select 10, 'code','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'name','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'bNew','����', 'bit',9,0,0,0,0,null,0,null
union all
select 10, 'bShow','���', 'bit',9,0,0,0,0,null,0,null
union all
select 10, 'userFieldName','�û��ؼ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'dateFieldName','���ڿؼ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'statusFieldName','״̬�ؼ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'dataControlStatuss','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiControlStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 11, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 11, 'fid','���','int',9,0,1,0,0,'id',1,'LiStatus'
union all
select 11, 'code','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 11, 'name','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 11, 'groupName','������', 'narchar',50,0,0,0,0,null,0,null
union all
select 11, 'bReadOnly','ֻ��', 'bit',9,0,0,0,0,null,0,null
union all
select 11, 'bVisibe','����', 'bit',9,0,0,0,0,null,0,null
union all
select 11, 'defaultValue','������', 'narchar',5000,0,0,0,0,null,0,null


--LiDictGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 12, 'ID','����','int',9,1,0,0,1,null,0,null
union all
select 12, 'Code','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 12, 'Name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 12, 'isGroup','�Ƿ���', 'bit',30,0,0,0,0,null,0,null
union all
select 12, 'isSystem','�Ƿ�ϵͳ', 'bit',30,0,0,0,0,null,0,null
union all
select 12, 'GroupId','��ID', 'int',30,0,0,0,0,null,0,null
union all
select 12, 'ParentID','��ID', 'int',9,0,0,0,0,null,0,null
union all
select 12, 'systemCode','ϵͳ����', 'narchar',9,0,0,0,0,null,0,null
union all
select 12, 'imageIndex','ͼ������', 'int',9,0,0,0,0,null,0,null
union all
select 12, 'iOrder','����','int',9,0,0,0,0,null,0,null
union all
select 12, 'dModifyDate','�޸�ʱ��', 'datetime',50,0,0,0,0,null,0,null

--LiDict
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 13, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 13, 'dictParentID','���', 'int',30,0,0,0,0,null,0,null
--select 13, 'dictParentID','���','int',9,0,1,0,0,'id',1,'LiDictGroup'
union all
select 13, 'dictCode','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 13, 'dictName','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 13, 'dictOrder','����', 'int',50,0,0,0,0,null,0,null
union all
select 13, 'dictMemo','��ע', 'narchar',50,0,0,0,0,null,0,null
union all
select 13, 'dModifyDate','�޸�ʱ��', 'datetime',50,0,0,0,0,null,0,null


--LiVoucherStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 14, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 14, 'dataBaseName','�������ݿ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityType','ʵ������', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'systemCode','ϵͳ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 14, 'entityKey','����Key', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityOrder','�Ƿ�������master', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityColumnName','��Ӧ�����ϵ��ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableName','������', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableAliasName','�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableAbbName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableDesc','������', 'narchar',5000,0,0,0,0,null,0,null
union all
select 14, 'className','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'keyName','������', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'bDefaultBody','Ĭ�ϱ���', 'bit',1,0,0,0,0,null,0,null
union all
select 14, 'childTableEntityColumnName','�ӱ�ʵ����', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'modifyDate','�޸�ʱ��', 'dateTime',50,0,0,0,0,null,0,null
union all
select 14, 'datas','���ݼ�','collection', -1,0,0,2,0,null,0,null

--ColumnInfo
--select * from ColumnInfo where fid = 15
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 15, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 15, 'fid','���','int',9,0,1,0,0,'id',1,'TableInfo'
union all
select 15, 'columnName','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnAbbName','�������', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnType','������', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'length','����', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKey','�Ƿ�����', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'foreignKey','�Ƿ����', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'relationshipType','��ϵ����', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'databaseGeneratedType','��������', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKeyName','��Ӧ��������������', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKeyDatabaseGenerated','��Ӧ����������', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKeyTableName','��Ӧ���������', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnOrder','��˳��', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'columnScale','��С��λ', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'columnIsNull','���Ƿ�Ϊ��', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'columnDefaultValue','��Ĭ��ֵ', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'bSearchColumns','�����Ƿ������', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'bDisplayColumn','�����Ƿ���ʾ', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'bVisible','�Ƿ���ʾ', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'modifyDate','�޸�ʱ��', 'dateTime',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoType','������������', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'dictInfoType','�ֵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoShowFieldName','����������ʾ����', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoRelationFieldName','�������������ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoKeyFieldName','�������������ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnWidth','�п��', 'int',9,0,0,0,0,null,0,null
union all
select 15, 'controlType','�ؼ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'gridlookUpEditShowModelJson','��ʾ����', 'narchar',4000,0,0,0,0,null,0,null
union all
select 15, 'bExtendField','�Ƿ���չ�ֶα�', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'extendTableName','��չ�ֶα�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'extendTableKeyFieldName','��չ�ֶα������', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'extendRelationTableKeyFieldName','��չ����������', 'narchar',50,0,0,0,0,null,0,null

--LiAdminMeum
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 16, 'ID','����','int',9,1,0,0,1,null,0,null
union all
select 16, 'Code','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 16, 'Name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 16, 'isGroup','�Ƿ���', 'bit',30,0,0,0,0,null,0,null
union all
select 16, 'isSystem','�Ƿ�ϵͳ', 'bit',30,0,0,0,0,null,0,null
union all
select 16, 'GroupId','��ID', 'int',30,0,0,0,0,null,0,null
union all
select 16, 'ParentID','��ID', 'int',9,0,0,0,0,null,0,null
union all
select 16, 'imageIndex','ͼ������', 'int',9,0,0,0,0,null,0,null
union all
select 16, 'iOrder','����','int',9,0,0,0,0,null,0,null


--LiButtonGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 17, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 17, 'formId','���','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 17, 'panelModelId','���','int',9,0,1,0,0,'id',1,'LiPanel'
union all
select 17, 'controlGroupId','���','int',9,0,1,0,0,'id',1,'LiControlGroup'
union all
select 17, 'buttonId','���','int',9,0,1,0,0,'id',1,'LiButton'
union all
select 17, 'listButtonId','���','int',9,0,1,0,0,'id',1,'LiListButton'

union all
select 17, 'fullName','ȫ����', 'narchar',300,0,0,0,0,null,0,null
union all
select 17, 'assemblyName','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 17, 'eventMemo','ȫ����', 'narchar',300,0,0,0,0,null,0,null


--LiManageMeum
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 18, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 18, 'eventType','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 18, 'eventName','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 18, 'eventFullName','ȫ����', 'narchar',300,0,0,0,0,null,0,null
union all
select 18, 'eventAssemblyName','����', 'narchar',50,0,0,0,0,null,0,null


--LiListButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 19, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 19, 'formId','���','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 19, 'iIndex','˳��', 'int',9,0,0,0,0,null,0,null
union all
select 19, 'caption','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'iconsize','ͼ���С', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'categoryGuid','���ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 19, 'icon','ͼ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'voucherStatus','����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'events','���ݼ�','collection', -1,0,0,2,0,null,0,null



--LiQueryScheme
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 20, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 20, 'entityKey','����Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 20, 'userCode','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 20, 'querySchemeName','��ѯ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 20, 'querys','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 20, 'entitys','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 20, 'fields','���ݼ�','collection', -1,0,0,2,0,null,0,null



--LiQuery
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 21, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 21, 'querySchemeId','���','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 21, 'sBracketsBefore','ǰ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'sFieldName','�ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 21, 'sJudgmentSymbol','�жϷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'oQueryValue','��ѯֵ', 'narchar',4000,0,0,0,0,null,0,null
union all
select 21, 'sJoin','���ӷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'sBracketsAfter','������', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'sFieldType','�ؼ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 21, 'sBasicCode','��������', 'narchar',30,0,0,0,0,null,0,null


--LiEntity
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 22, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 22, 'querySchemeId','���','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 22, 'sEntityType','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'sEntityCode','ʵ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'sEntityName','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'sTableName','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'iShow','�Ƿ���ʾ', 'bit',30,0,0,0,0,null,0,null


--LiField
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 23, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 23, 'querySchemeId','���','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 23, 'convertId','���','int',9,0,1,0,0,'id',1,'LiConvertHead'
union all
select 23, 'code','�б��ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'fieldName','ʵ���ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'columnFieldName','ʵ���ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'name','�б��ֶ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'sEntityCode','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'sEntityName','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'iColumnWidth','�п�', 'int',9,0,0,0,0,null,0,null
union all
select 23, 'bColumnDisplay','�Ƿ���ʾ', 'bit',30,0,0,0,0,null,0,null
union all
select 23, 'bQuery','���ٲ�ѯ', 'bit',30,0,0,0,0,null,0,null
union all
select 23, 'bRange','����', 'bit',30,0,0,0,0,null,0,null
union all
select 23, 'sColumnControlType','�ؼ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'sRefTypeCode','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'sJudgeSymbol','�жϷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 23, 'basicInfoKey','��������Kdy', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'dictInfoType','�ֵ䵵������', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'gridlookUpEditShowModelJson','Json�ֶ�', 'narchar',200,0,0,0,0,null,0,null


--LiUsers
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 24, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 24, 'skinName','Ƥ��', 'narchar',50,0,0,0,0,null,0,null
union all
select 24, 'bAdmin','�Ƿ����Ա', 'bit',10,0,0,0,0,null,0,null
union all
select 24, 'modifyDate','�޸�ʱ��', 'dateTime',50,0,0,0,0,null,0,null

--LiRoles
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 25, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 25, 'roleCode','��ɫ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 25, 'roleName','��ɫ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 25, 'modifyDate','�޸�ʱ��', 'dateTime',50,0,0,0,0,null,0,null

--LiAuthData
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 26, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 26, 'entityKey','ʵ��Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'roleCode','��ɫ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'code','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'bShow','�Ƿ���ʾ', 'bit',10,0,0,0,0,null,0,null
union all
select 26, 'bEdit','�Ƿ�༭', 'bit',10,0,0,0,0,null,0,null

--LiAuth
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 27, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 27, 'entityKey','ʵ��Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'roleCode','��ɫ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'code','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'bShow','�Ƿ���ʾ', 'bit',10,0,0,0,0,null,0,null


--LiAuth
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 28, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 28, 'userCode','�û�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 28, 'roleCode','��ɫ����', 'narchar',30,0,0,0,0,null,0,null

--LiVoucherCode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 29, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 29, 'entityKey','ʵ��Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'bDefault','Ĭ�Ϸ���', 'bit',10,0,0,0,0,null,0,null
union all
select 29, 'name','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'prefixName','ǰ׺', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'fieldTextName','�����ı��ֶ�', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'fieldDateName','���������ֶ�', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'dateTimeFormat','���ڸ�ʽ', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'iZero','��߲�0', 'int',9,0,0,0,0,null,0,null
union all
select 29, 'iStep','��ˮ�Ų�λ', 'int',9,0,0,0,0,null,0,null
union all
select 29, 'flowNoRange','��ˮ�ŷ�Χ', 'narchar',10,0,0,0,0,null,0,null


--LiConvertHead
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 30, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 30, 'convertCode','ת������', 'narchar',30,0,0,0,0,null,0,null
union all
select 30, 'convertName','ת������', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertDestType','Ŀ������', 'narchar',20,0,0,0,0,null,0,null
union all
select 30, 'convertDest','Ŀ��', 'narchar',100,0,0,0,0,null,0,null
union all
select 30, 'convertSourceType','Դ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 30, 'convertSource','Դ', 'narchar',100,0,0,0,0,null,0,null
union all
select 30, 'convertRelation','ת����ϵ', 'narchar',20,0,0,0,0,null,0,null
union all
select 30, 'convertRelationField','ת����ϵ�ֶ�', 'narchar',100,0,0,0,0,null,0,null
union all
select 30, 'convertDestHeadName','Ŀ���ͷ����', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertDestBodyName','Ŀ���������', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeField','�ۼ��ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertPushField','���������ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertPushTableName','���Ʊ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'bSourceTableFiliter','Դ�����', 'bit',10,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeTextField','�ۼ��ı��ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeIDField','�ۼ�ID�ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeTableName','�ۼƱ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeDatabaseName','�ۼƱ������ݿ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'datas','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 30, 'queryFields','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiConvertBody

insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 31, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 31, 'fid','���','int',9,0,1,0,0,'id',1,'LiConvertHead'
union all
select 31, 'convertDestType','Ŀ������', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertDCollection','Ŀ�꼯������', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertDestField','Ŀ���ֶ�', 'narchar',100,0,0,0,0,null,0,null
union all
select 31, 'convertSourceType','Դ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertSCollection','Դ��������', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertSourceField','Դ�ֶ�', 'narchar',100,0,0,0,0,null,0,null
union all
select 31, 'bRef','�Ƿ�����', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'refBasicInfoType','���û�������', 'narchar',30,0,0,0,0,null,0,null
union all
select 31, 'refBasicInfoField','���ö����ֶ�', 'narchar',100,0,0,0,0,null,0,null
union all
select 31, 'refBasicInfoValueField','���ö����ֶ�', 'narchar',50,0,0,0,0,null,0,null
union all
select 31, 'bDefault','�Ƿ�ʹ��Ĭ��ֵ', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'defaultValue','Ĭ��ֵ�ֶ�', 'narchar',4000,0,0,0,0,null,0,null
union all
select 31, 'reverseIdFieldName','��дID', 'narchar',50,0,0,0,0,null,0,null
union all
select 31, 'reverseCodeFieldName','��д����', 'narchar',50,0,0,0,0,null,0,null
union all
select 31, 'bReverseType','��д����', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'bCumulativeRelationQty','�ۼƹ��������ֶ�', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'bCumulativeRelationID','�ۼƹ���ID�ֶ�', 'bit',10,0,0,0,0,null,0,null

--select * from LiField

insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 32, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 32, 'querySchemeId','���','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 32, 'code','�б��ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'name','�б��ֶ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'convertId','���','int',9,0,1,0,0,'id',1,'LiConvertHead'
union all
select 32, 'fieldName','ʵ���ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'columnFieldName','ʵ���ֶ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'sEntityCode','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'sEntityName','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'iColumnWidth','�п�', 'int',9,0,0,0,0,null,0,null
union all
select 32, 'bColumnDisplay','�Ƿ���ʾ', 'bit',30,0,0,0,0,null,0,null
union all
select 32, 'bQuery','���ٲ�ѯ', 'bit',30,0,0,0,0,null,0,null
union all
select 32, 'bRange','����', 'bit',30,0,0,0,0,null,0,null
union all
select 32, 'sColumnControlType','�ؼ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 32, 'sRefTypeCode','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 32, 'sJudgeSymbol','�жϷ���', 'narchar',10,0,0,0,0,null,0,null


--LiFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 33, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 33, 'flowCode','���̱���', 'narchar',30,0,0,0,0,null,0,null
union all
select 33, 'flowName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 33, 'entityKey','���ݱ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 33, 'entityName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 33, 'bDefault','�Ƿ�Ĭ��', 'bit',1,0,0,0,0,null,0,null
union all
select 33, 'nodes','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiFlowNode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 34, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 34, 'flowId','���','int',9,0,1,0,0,'id',1,'LiFlow'
union all
select 34, 'flowNodeCode','���̽ڵ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 34, 'flowNodeName','���̽ڵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 34, 'flowNodeType','���̽ڵ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 34, 'flowNodeInformation','������Ϣ', 'narchar',500,0,0,0,0,null,0,null
union all
select 34, 'X','X', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'Y','Y', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'users','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 34, 'connectors','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiFlowUser
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 35, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 35, 'flowNodeId','���','int',9,0,1,0,0,'id',1,'LiFlowNode'
union all
select 35, 'userCode','�û�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 35, 'userName','�û�����', 'narchar',50,0,0,0,0,null,0,null


--LiFlowConnector
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 36, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 36, 'flowNodeId','���','int',9,0,1,0,0,'id',1,'LiFlowNode'
union all
select 36, 'flowNodeCodeTo','�ڵ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 36, 'flowNodeNameTo','�ڵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 36, 'flowNodeFormIndex','Դ�ڵ��ĸ�ê��', 'int',9,0,0,0,0,null,0,null
union all
select 36, 'flowNodeToIndex','Ŀ��ڵ��ĸ�ê��', 'int',9,0,0,0,0,null,0,null
union all
select 36, 'conditions','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiFlowCondition
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 37, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 37, 'flowConnectorId','���','int',9,0,1,0,0,'id',1,'LiFlowConnector'
union all
select 37, 'sBracketsBefore','ǰ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'sFieldName','�ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 37, 'sJudgmentSymbol','�жϷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'oQueryValue','��ѯֵ', 'narchar',4000,0,0,0,0,null,0,null
union all
select 37, 'sJoin','���ӷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'sBracketsAfter','������', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'sFieldType','�ؼ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 37, 'sBasicCode','��������', 'narchar',30,0,0,0,0,null,0,null




--LiVersionFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 38, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 38, 'flowVersionNumber','���̰汾��', 'narchar',30,0,0,0,0,null,0,null
union all
select 38, 'flowCode','���̱���', 'narchar',30,0,0,0,0,null,0,null
union all
select 38, 'flowName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 38, 'entityKey','���ݱ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 38, 'entityName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 38, 'openStatus','����״̬', 'narchar',50,0,0,0,0,null,0,null
union all
select 38, 'bDefault','�Ƿ�Ĭ��', 'bit',1,0,0,0,0,null,0,null
union all
select 38, 'flowVersionDate','��������', 'dateTime',50,0,0,0,0,null,0,null
union all
select 38, 'nodes','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiVersionFlowNode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 39, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 39, 'flowId','���','int',9,0,1,0,0,'id',1,'LiVersionFlow'
union all
select 39, 'flowNodeCode','���̽ڵ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 39, 'flowNodeName','���̽ڵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 39, 'flowNodeType','���̽ڵ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 39, 'flowNodeInformation','������Ϣ', 'narchar',500,0,0,0,0,null,0,null
union all
select 39, 'X','X', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'Y','Y', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'users','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 39, 'connectors','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiVersionFlowUser
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 40, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 40, 'flowNodeId','���','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 40, 'userCode','�û�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 40, 'userName','�û�����', 'narchar',50,0,0,0,0,null,0,null


--LiVersionFlowConnector
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 41, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 41, 'flowNodeId','���','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 41, 'flowNodeCodeTo','�ڵ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 41, 'flowNodeNameTo','�ڵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 41, 'flowNodeFormIndex','Դ�ڵ��ĸ�ê��', 'int',9,0,0,0,0,null,0,null
union all
select 41, 'flowNodeToIndex','Ŀ��ڵ��ĸ�ê��', 'int',9,0,0,0,0,null,0,null
union all
select 41, 'conditions','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiVersionFlowCondition
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 42, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 42, 'flowConnectorId','���','int',9,0,1,0,0,'id',1,'LiVersionFlowConnector'
union all
select 42, 'sBracketsBefore','ǰ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'sFieldName','�ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 42, 'sJudgmentSymbol','�жϷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'oQueryValue','��ѯֵ', 'narchar',4000,0,0,0,0,null,0,null
union all
select 42, 'sJoin','���ӷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'sBracketsAfter','������', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'sFieldType','�ؼ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 42, 'sBasicCode','��������', 'narchar',30,0,0,0,0,null,0,null


--LiVoucherFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 43, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 43, 'flowVersionId','���̰汾��ID', 'int',30,0,0,0,0,null,0,null
union all
select 43, 'flowVersionNumber','���̰汾��', 'narchar',50,0,0,0,0,null,0,null
union all
select 43, 'flowCode','���̱���', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowName','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'entityKey','���ݱ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'entityName','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'voucherId','����ID', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'voucherCode','���ݱ��', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowStatus','����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowTitle','���̱���', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowStartDate','���̿�ʼʱ��', 'dateTime',30,0,0,0,0,null,0,null
union all
select 43, 'flowEndDate','���̽���ʱ��', 'dateTime',30,0,0,0,0,null,0,null
union all
select 43, 'datas','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiVoucherFlowStep
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 44, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 44, 'flowVoucherId','���','int',9,0,1,0,0,'id',1,'LiVoucherFlow'
union all
select 44, 'flowSeq','����˳��', 'int',10,0,0,0,0,null,0,null
union all
select 44, 'flowVersionNextStepNodeId','������һ��', 'int',10,0,0,0,0,null,0,null
union all
select 44, 'flowVersionNodeId','������һ��', 'int',10,0,0,0,0,null,0,null
union all
select 44, 'flowUserCode','�����û�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowUserName','�����û�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowApprovalType','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowContent','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowStatus','����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowDate','����ʱ��', 'dateTime',30,0,0,0,0,null,0,null


--LiVersionFlowNode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 45, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 45, 'flowId','���','int',9,0,1,0,0,'id',1,'LiVersionFlow'
union all
select 45, 'flowNodeCode','���̽ڵ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 45, 'flowNodeName','���̽ڵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 45, 'flowNodeType','���̽ڵ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 45, 'flowNodeInformation','������Ϣ', 'narchar',500,0,0,0,0,null,0,null
union all
select 45, 'X','X', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'Y','Y', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'users','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 45, 'connectors','���ݼ�','collection', -1,0,0,2,0,null,0,null


--LiVersionFlowUser
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 46, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 46, 'flowNodeId','���','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 46, 'userCode','�û�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 46, 'userName','�û�����', 'narchar',50,0,0,0,0,null,0,null


--LiVersionFlowConnector
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 47, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 47, 'flowNodeId','���','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 47, 'flowNodeCodeTo','�ڵ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 47, 'flowNodeNameTo','�ڵ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 47, 'flowNodeFormIndex','Դ�ڵ��ĸ�ê��', 'int',9,0,0,0,0,null,0,null
union all
select 47, 'flowNodeToIndex','Ŀ��ڵ��ĸ�ê��', 'int',9,0,0,0,0,null,0,null
union all
select 47, 'conditions','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiVersionFlowCondition
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 48, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 48, 'flowConnectorId','���','int',9,0,1,0,0,'id',1,'LiVersionFlowConnector'
union all
select 48, 'sBracketsBefore','ǰ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'sFieldName','�ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 48, 'sJudgmentSymbol','�жϷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'oQueryValue','��ѯֵ', 'narchar',4000,0,0,0,0,null,0,null
union all
select 48, 'sJoin','���ӷ���', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'sBracketsAfter','������', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'sFieldType','�ؼ�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 48, 'sBasicCode','��������', 'narchar',30,0,0,0,0,null,0,null

--LiFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 49, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 49, 'messageType','��Ϣ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 49, 'messageDate','��Ϣ����', 'dateTime',9,0,0,0,0,null,0,null
union all
select 49, 'messageContent','��Ϣ����', 'bit',1,0,0,0,0,null,0,null
union all
select 49, 'messageRead','�Ƿ�����', 'narchar',500,0,0,0,0,null,0,null
union all
select 49, 'voucherFlowId','��������ID', 'int',9,0,0,0,0,null,0,null
union all
select 49, 'flowVersionId','���̰汾ID', 'int',9,0,0,0,0,null,0,null
union all
select 49, 'flowVersionNumber','���̰汾��', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'flowCode','���̱���', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'flowName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 49, 'entityKey','���ݱ���', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'entityName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 49, 'voucherId','����ID', 'int',9,0,0,0,0,null,0,null
union all
select 49, 'voucherCode','���ݱ��', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'userCode','�û�����', 'narchar',30,0,0,0,0,null,0,null


--ProcedureInfo
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 50, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 50, 'dataBaseName','���ݿ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 50, 'systemCode','ϵͳ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 50, 'entityKey','ʵ��Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 50, 'procedureName','�洢��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 50, 'datas','���ݼ�','collection', -1,0,0,2,0,null,0,null

--ParamInfo
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 51, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 51, 'fid','���','int',9,0,1,0,0,'id',1,'ProcedureInfo'
union all
select 51, 'paramName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 51, 'paramType','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 51, 'paramLength','��������', 'int',9,0,0,0,0,null,0,null


--LiSystemInfo
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 52, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 52, 'systemCode','ϵͳ����', 'narchar',10,0,0,0,0,null,0,null
union all
select 52, 'systemDataBaseName','ϵͳ���ݿ�', 'narchar',20,0,0,0,0,null,0,null
union all
select 52, 'systemName','ϵͳ����', 'narchar',30,0,0,0,0,null,0,null
union all
select 52, 'systemU8MenuId','ϵͳU8�˵�ID', 'narchar',10,0,0,0,0,null,0,null
union all
select 52, 'systemTitle','ϵͳ����', 'narchar',50,0,0,0,0,null,0,null
union all
select 52, 'companyName','��˾����', 'narchar',50,0,0,0,0,null,0,null
union all
select 52, 'companyLogo','��˾Logo', 'narchar',5000,0,0,0,0,null,0,null
union all
select 52, 'bDefault','Ĭ����Ϣ', 'bit',1,0,0,0,0,null,0,null


--LiU8Voucher
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 53, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 53, 'code','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 53, 'name','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 53, 'voucherType','��������', 'narchar',10,0,0,0,0,null,0,null
union all
select 53, 'operations','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiU8Operation
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 54, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 54, 'fid','���','int',9,0,1,0,0,'id',1,'LiU8Voucher'
union all
select 54, 'operationCode','��������', 'narchar',30,0,0,0,0,null,0,null
union all
select 54, 'operationName','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 54, 'operationSymbol','��������', 'narchar',20,0,0,0,0,null,0,null
union all
select 54, 'paramModels','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 54, 'fields','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiU8Param
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 55, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 55, 'fid','���','int',9,0,1,0,0,'id',1,'LiU8Operation'
union all
select 55 'paramName','������', 'narchar',30,0,0,0,0,null,0,null
union all
select 55, 'paramDesc','��������', 'narchar',200,0,0,0,0,null,0,null
union all
select 55, 'paramType','��������', 'narchar',50,0,0,0,0,null,0,null
union all
select 55, 'paramDirection','��������', 'narchar',20,0,0,0,0,null,0,null
union all
select 55, 'paramTransMode','���ݷ�ʽ', 'narchar',20,0,0,0,0,null,0,null
union all
select 55, 'paramIsRequire','�Ƿ��ѡ', 'bit',1,0,0,0,0,null,0,null
union all
select 55, 'paramBoObject','BO����', 'narchar',50,0,0,0,0,null,0,null
union all
select 55, 'paramBoType','�Ƿ�BO��ͷ', 'nvarchar',50,0,0,0,0,null,0,null
union all
select 55, 'paramDefaultValue','Ĭ��ֵ', 'narchar',200,0,0,0,0,null,0,null
union all
select 55, 'parambID','�Ƿ���ID�ֶ���', 'bit',1,0,0,0,0,null,0,null
union all
select 55, 'parambCode','�Ƿ���Code�ֶ���', 'bit',1,0,0,0,0,null,0,null


--LiU8Field
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 56, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 56, 'fid','���','int',9,0,1,0,0,'id',1,'LiU8Operation'
union all
select 56, 'fieldEntityType','ʵ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 56, 'fieldName','�ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 56, 'fieldDesc','�ֶ�����', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldType','�ֶ�����', 'narchar',50,0,0,0,0,null,0,null
union all
select 56, 'fieldIsRequire','�Ƿ����', 'bit',1,0,0,0,0,null,0,null
union all
select 56, 'fieldMaxValue','���ֵ', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldMinValue','��Сֵ', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldDefaultValue','Ĭ��ֵ', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldbDefault','�Ƿ�Ĭ��', 'bit',1,0,0,0,0,null,0,null
union all
select 56, 'fieldLength','��󳤶�', 'int',9,0,0,0,0,null,0,null


--LiU8EnvContext
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 57, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 57, 'fid','���','int',9,0,1,0,0,'id',1,'LiU8Operation'
union all
select 57 'contextName','����������', 'narchar',30,0,0,0,0,null,0,null
union all
select 57, 'contextDesc','����', 'narchar',200,0,0,0,0,null,0,null
union all
select 57, 'contextType','����������', 'narchar',50,0,0,0,0,null,0,null
union all
select 57, 'contextDefaultValue','Ĭ��ֵ', 'narchar',200,0,0,0,0,null,0,null


--LiPushForm
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 58, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 58, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 58, 'text','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 58, 'height','�߶�', 'int',9,0,0,0,0,null,0,null
union all
select 58, 'width','���', 'int',9,0,0,0,0,null,0,null
union all
select 58, 'systemCode','ϵͳ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 58, 'pushEvents','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 58, 'pushListButtons','���ݼ�','collection', -1,0,0,2,0,null,0,null



--LiPushListButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 59, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 59, 'pushFormId','���','int',9,0,1,0,0,'id',1,'LiPushForm'
union all
select 59, 'iIndex','˳��', 'int',9,0,0,0,0,null,0,null
union all
select 59, 'caption','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'name','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'iconsize','ͼ���С', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'categoryGuid','���ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 59, 'icon','ͼ������', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'voucherStatus','����״̬', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'pushEvents','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiPushEvent
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 60, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 60, 'pushFormId','���','int',9,0,1,0,0,'id',1,'LiPushForm'
union all
select 60, 'pushListButtonId','���','int',9,0,1,0,0,'id',1,'LiPushListButton'

union all
select 60, 'fullName','ȫ����', 'narchar',300,0,0,0,0,null,0,null
union all
select 60, 'assemblyName','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 60, 'eventMemo','ȫ����', 'narchar',300,0,0,0,0,null,0,null


--LiButtonGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 61, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 61, 'controlId','���','int',9,0,1,0,0,'id',1,'LiControl'
union all
select 61, 'eventType','�¼�����', 'narchar',30,0,0,0,0,null,0,null
union all
select 61, 'eventExpression','�¼����ʽ', 'narchar',5000,0,0,0,0,null,0,null
union all
select 61, 'bEnable','�Ƿ�����', 'bit',1,0,0,0,0,null,0,null
union all
select 61, 'eventMemo','�¼�����', 'narchar',300,0,0,0,0,null,0,null




--LiReport
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 62, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 62, 'reportKey','����', 'narchar',30,0,0,0,0,null,0,null
union all
select 62, 'reportName','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 62, 'systemCode','ϵͳ���׺�', 'narchar',10,0,0,0,0,null,0,null
union all
select 62, 'reportSql','����SQL', 'narchar',5000,0,0,0,0,null,0,null
union all
select 62, 'reportCountSql','��������SQL', 'narchar',5000,0,0,0,0,null,0,null
union all
select 62, 'datas','���ݼ�','collection', -1,0,0,2,0,null,0,null
union all
select 62, 'buttons','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiReportField
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 63, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 63, 'reportId','���','int',9,0,1,0,0,'id',1,'LiReport'
union all
select 63, 'columnName','�ֶ���', 'narchar',50,0,0,0,0,null,0,null
union all
select 63, 'columnCaption','����', 'narchar',50,0,0,0,0,null,0,null
union all
select 63, 'columnType','������', 'narchar',100,0,0,0,0,null,0,null
union all
select 63, 'iColumnWidth','�п�', 'int',9,0,0,0,0,null,0,null
union all
select 63, 'bColumnDisplay','�Ƿ���ʾ��', 'bit',1,0,0,0,0,null,0,null
union all
select 63, 'iColumnIndex','������', 'int',9,0,0,0,0,null,0,null
union all
select 63, 'bQuery','�Ƿ��ѯ', 'bit',1,0,0,0,0,null,0,null
union all
select 63, 'iDisplayFormatType','��ʾ��ʽ����', 'int',9,0,0,0,0,null,0,null
union all
select 63, 'displayFormat','��ʾ��ʽ', 'narchar',30,0,0,0,0,null,0,null
union all
select 63, 'bColumnGroup','�Ƿ����', 'bit',1,0,0,0,0,null,0,null
union all
select 63, 'columnGroupFormat','���ܸ�ʽ', 'narchar',30,0,0,0,0,null,0,null


--LiReportButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 64, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 64, 'reportId','���','int',9,0,1,0,0,'id',1,'LiReport'
union all
select 64, 'caption','����', 'narchar',20,0,0,0,0,null,0,null
union all
select 64, 'name','����', 'narchar',20,0,0,0,0,null,0,null
union all
select 64, 'iconsize','��ť����', 'narchar',50,0,0,0,0,null,0,null
union all
select 64, 'categoryGuid','���ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 64, 'icon','ͼ��', 'narchar',50,0,0,0,0,null,0,null
union all
select 64, 'iIndex','˳��', 'int',9,0,0,0,0,null,0,null
union all
select 64, 'events','���ݼ�','collection', -1,0,0,2,0,null,0,null

--LiReportEvent
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 65, 'id','����','int',9,1,0,0,1,null,0,null
union all
select 65, 'reportButtonId','���','int',9,0,1,0,0,'id',1,'LiReportButton'
union all
select 65, 'fullName','ȫ����', 'narchar',20,0,0,0,0,null,0,null
union all
select 65, 'assemblyName','����', 'narchar',20,0,0,0,0,null,0,null
union all
select 65, 'eventMemo','��ע', 'narchar',20,0,0,0,0,null,0,null
GO

exec sp_CreateTable 4
exec sp_CreateTable 11

--select * from LiForm
--select * from TableInfo
--select * from TestHead