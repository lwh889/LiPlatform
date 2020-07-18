drop table ColumnInfo
drop table TableInfo
create table TableInfo(
	id int identity(1,1) primary key,
	dataBaseName nvarchar(50),--数据库名称
	entityType nvarchar(50), --实体类型，单据？档案？
	entityKey nvarchar(50),	--单据主键
	entityName nvarchar(50),	--单据名称
	entityOrder nvarchar(20),	--单据的主，次
	entityColumnName nvarchar(50),	--子表在主表模型的名称，不能有重复的名字
	tableName nvarchar(50),	--表名
	tableAliasName nvarchar(50),	--表别名
	tableAbbName nvarchar(50),	--表简称
	tableDesc nvarchar(255),	--表描述
	className nvarchar(50),	--json数据转化到实体的类名
	keyName nvarchar(50),	--主键名
	bDefaultBody bit default 0,	--默认表体
	childTableEntityColumnName nvarchar(500),	--外键名 --不要
	
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)
--alter table TableInfo add bDefaultBody bit default 0

create table ColumnInfo(
	id int identity(1,1) primary key,
	fid int REFERENCES TableInfo(id),
	columnName nvarchar(50),	--列名
	columnAbbName nvarchar(50),	--列简称
	columnType nvarchar(50),	--列类型
	controlType nvarchar(50),	--控件类型
	length int default 0,	--数据长度
	primaryKey bit default 0,	--主键
	foreignKey bit default 0,	--外键
	relationshipType int default 0,	--关系类型，一对多，一对一,0 无，1 一对一，2 一对多，3 自已
	databaseGeneratedType int default 0,	--列的自增类型,0 不处理，1 自增长，2 计算所得，3 newid
	
	primaryKeyName  nvarchar(50),	--子表外键列是要写上主键名称
	primaryKeyDatabaseGenerated int default 0,	--子表外键列是要写上主键自增类型
	primaryKeyTableName nvarchar(50),	--子表外键列是要写上主表名

	columnOrder int default 0, --列顺序
	columnScale int default 0, --列小数位
	columnIsNull bit default 1,--列是否为空
	columnDefaultValue nvarchar(max),--列默认值
	columnWidth int default 0,--列宽度

	bSearchColumns bit default 0,	--该列是否可搜索
	bDisplayColumn bit default 1,	--该列是否显示

	bVisible bit default 1, --是否显示
	
	bExtendField bit default 0, --是否扩展字段表
	extendTableName nvarchar(50),	--扩展字段表名
	extendTableKeyFieldName nvarchar(50),	--扩展字段表名外键
	extendRelationTableKeyFieldName nvarchar(50),	--扩展关联表主键

	basicInfoType nvarchar(50),	--基础档案类型
	dictInfoType nvarchar(50),	--字典类型
	basicInfoShowFieldName nvarchar(50),	--基础档案显示属性
	basicInfoRelationFieldName nvarchar(50),	--基础档案关联字段名
	basicInfoKeyFieldName nvarchar(50),	--基础档案主键字段
	gridlookUpEditShowModelJson nvarchar(4000),
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

--alter table ColumnInfo add basicInfoType nvarchar(50)	--基础档案类型
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
--框架信息组
--values ('entity1','master',null,'spring_user', 'user1', '用户表', null,'JsonModel', 'id',null, getdate())
select 1,'form1','master',null,'LiForm','liform1','Form框架表', null,'JsonModel','id','panels,buttonGroups,controlGroups',getdate()
union all
select 2,'form1','slave', 'panels','LiPanel','lipanel1','容器', null,'JsonModel','id','controlGroups,buttonGroups',getdate()
union all
select 3,'form1','slave','controlGroups','LiControlGroup','licontrolgroup1','控件组', null,'JsonModel','id','controls',getdate()
union all
select 4,'form1','slave','buttonGroups','LiButtonGroup','libuttongroup1','按钮组', null,'JsonModel','id','buttons',getdate()
union all
select 5,'form1','slave', 'buttons','LiButton','libutton1','按钮', null,'JsonModel','id',null,getdate()
union all
select 6,'form1','slave','controls','LiControl','licontrol1','控件', null,'JsonModel','id',null,getdate()
union all
select 17,'form1','slave','events','LiEvent','liEvent1','事件组', null,'JsonModel','id','events',getdate()
union all
select 61,'form1','slave','events','LiControlEvent','liEvent1','控件事件组', null,'JsonModel','id','controlEvents',getdate()
union all
select 19,'form1','slave','listButtons','LiListButton','liListButton1','列表按钮组', null,'JsonModel','id','listButtons',getdate()
union all
select 7,'liManageMeum','master',null,'LiManageMeum','limanagemeum1','系统管理菜单', null,'JsonModel','id',null,getdate()
--数据库名
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 8,'LiSystem','Basic','sysDatabases','master',null,'V_SysDatabases','sysDatabases','数据库档案', null,'JsonModel','dbid',null,getdate()
--状态
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 9,'LiSystem','Basic','liVoucherStatus','master',null,'LiVoucherStatus','liVoucherStatus','单据状态表', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 10,'LiSystem','Basic','liVoucherStatus','slave','dataStatuss','LiStatus','liStatus','状态表', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 11,'LiSystem','Basic','liVoucherStatus','slave','dataControlStatuss','LiControlStatus','liControlStatus','控件状态表', null,'JsonModel','id',null,getdate()

--字典
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 12,'LiSystem','Basic','liDictGroup','master',null,'LiDictGroup','liDictGroup','字典组', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 13,'LiSystem','Basic','liDict','master',null,'LiDict','liDict','字典', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 14,'LiSystem','Basic','liTableInfo','master',null,'TableInfo','liTableInfo','表信息', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 15,'LiSystem','Basic','liTableInfo','slave','datas','ColumnInfo','liColumnInfo','列信息', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 16,'liAdminMeum','master',null,'LiAdminMeum','liAdminmeum','业务管理菜单', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 18,'liGeneralEvent','master',null,'LiGeneralEvent','liGeneralEvent','常用事件', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 20,'LiSystem','Basic','liQueryScheme','master',null,'LiQueryScheme','liQueryScheme','查询方案', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 21,'LiSystem','Basic','liQueryScheme','slave','querys','LiQuery','liQuery','查询条件', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 22,'LiSystem','Basic','liQueryScheme','slave','entitys','LiEntity','liEntity','实体集合', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 23,'LiSystem','Basic','liQueryScheme','slave','fields','LiField','liField','字段集合', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 24,'LiSystem','Basic','liUsers','master',null,'LiUsers','liUsers','用户表', null,'JsonModel','userCode',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 25,'LiSystem','Basic','liRoles','master',null,'LiRoles','liRoles','角色表', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 26,'LiSystem','Basic','liAuthData','master',null,'LiAuthData','liAuthData','数据权限表', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 27,'LiSystem','Basic','liAuth','master',null,'LiAuth','liAuth','权限表', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 28,'LiSystem','Basic','liUserRole','master',null,'LiUserRole','liUserRole','用户角色表', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 29,'LiSystem','Basic','liVoucherCode','master',null,'LiVoucherCode','liVoucherCode','单据编号', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 30,'LiSystem','Basic','liConvert','master',null,'LiConvertHead','liConvertHead','转换表头', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 31,'LiSystem','Basic','liConvert','slave','datas','LiConvertBody','liConvertBody','转换表体', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 32,'LiSystem','Basic','liConvert','slave','queryFields','LiField','liField','字段集合', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 33,'LiSystem','Basic','liFlow','master',null,'LiFlow','liFlow','流程表头', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 34,'LiSystem','Basic','liFlow','slave','nodes','LiFlowNode','liFlowNode','流程节点', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 35,'LiSystem','Basic','liFlow','slave','users','LiFlowUser','liFlowUser','流程用户', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 36,'LiSystem','Basic','liFlow','slave','connectors','LiFlowConnector','liFlowConnector','流程去向', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 37,'LiSystem','Basic','liFlow','slave','conditions','LiFlowCondition','liFlowCondition','流程条件', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 38,'LiSystem','Basic','liVersionFlow','master',null,'LiVersionFlow','liVersionFlow','流程版本表头', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 39,'LiSystem','Basic','liVersionFlow','slave','nodes','LiVersionFlowNode','liVersionFlowNode','流程版本节点', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 40,'LiSystem','Basic','liVersionFlow','slave','users','LiVersionFlowUser','liVersionFlowUser','流程版本用户', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 41,'LiSystem','Basic','liVersionFlow','slave','connectors','LiVersionFlowConnector','liVersionFlowConnector','流程版本去向', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 42,'LiSystem','Basic','liVersionFlow','slave','conditions','LiVersionFlowCondition','liVersionFlowCondition','流程版本条件', null,'JsonModel','id',null,getdate()
--update TableInfo set entityColumnName = 'queryFields' where 

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 43,'LiSystem','Basic','liVoucherFlow','master',null,'LiVoucherFlow','liVoucherFlow','单据流程', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 44,'LiSystem','Basic','liVoucherFlow','slave','datas','LiVoucherFlowStep','liVoucherFlowStep','流程步骤', null,'JsonModel','id',null,getdate()



insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 45,'LiSystem','Basic','liVersionFlowNode','master',null,'LiVersionFlowNode','liVersionFlowNode','流程版本节点', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 46,'LiSystem','Basic','liVersionFlowNode','slave','users','LiVersionFlowUser','liVersionFlowUser','流程版本用户', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 47,'LiSystem','Basic','liVersionFlowNode','slave','connectors','LiVersionFlowConnector','liVersionFlowConnector','流程版本去向', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 48,'LiSystem','Basic','liVersionFlowNode','slave','conditions','LiVersionFlowCondition','liVersionFlowCondition','流程版本条件', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 49,'LiSystem','Basic','liMessage','master',null,'LiMessage','liMessage','消息', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 50,'LiSystem','Basic','liProcedure','master',null,'ProcedureInfo','liProcedure','存储过程接口信息', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 51,'LiSystem','Basic','liProcedure','slave','datas','ParamInfo','liParamInfo','存储过程参数信息', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 52,'LiSystem','Basic','liSystemInfo','master',null,'LiSystemInfo','liSystemInfo','系统信息表', null,'JsonModel','id',null,getdate()

insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 53,'LiSystem','Basic','liU8Voucher','master',null,'LiU8Voucher','liU8Voucher','U8API单据', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 54,'LiSystem','Basic','liU8Voucher','slave','operations','LiU8Operation','liU8Operation','单据操作类型', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 55,'LiSystem','Basic','liU8Voucher','slave','paramModels','LiU8Param','liU8Param','单据参数', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 56,'LiSystem','Basic','liU8Voucher','slave','fields','LiU8Field','liU8Field','单据字段', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 57,'LiSystem','Basic','liU8Voucher','slave','contexts','LiU8EnvContext','iU8EnvContext','单据字段', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 58,'LiSystem','Basic','liPushForm','master',null,'LiPushForm','liPushForm','下推表头', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 59,'LiSystem','Basic','liPushForm','slave','pushListbuttons','LiPushListButton','liPushListButton','列表按钮', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 60,'LiSystem','Basic','liPushForm','slave','pushEvents','LiPushEvent','liPushEvent','下推事件', null,'JsonModel','id',null,getdate()


insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 62,'LiSystem','Basic','liReport','master',null,'LiReport','liPushForm','报表信息', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 63,'LiSystem','Basic','liReport','slave','datas','LiReportField','liReportField','报表字段', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 64,'LiSystem','Basic','liReport','slave','buttons','LiReportButton','liReportButton','报表按钮', null,'JsonModel','id',null,getdate()
insert into TableInfo (id,dataBaseName,entityType,entityKey,entityOrder,entityColumnName,tableName,tableAliasName,tableAbbName,tableDesc,className, keyName, childTableEntityColumnName,modifyDate) 
select 65,'LiSystem','Basic','liReport','slave','events','LiReportEvent','liReportEvent','报表按钮事件', null,'JsonModel','id',null,getdate()


SET IDENTITY_Insert TableInfo OFF

--LiForm
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 1, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 1, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'text','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 1, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 1, 'keyFieldName','主键字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'codeFieldName','编码字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'statusFieldName','状态字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'formType','类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 1, 'systemCode','系统代码', 'narchar',20,0,0,0,0,null,0,null
union all
select 1, 'panels','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 1, 'buttonGroups','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 1, 'events','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 1, 'listButtons','数据集','collection', -1,0,0,2,0,null,0,null



--LiPanel
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 2, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 2, 'formModelId','外键','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 2, 'dock','停靠区域', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'type','容器类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'text','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 2, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 2, 'tableName','表名', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'primaryKeyName','主键名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'foreigntKeyName','外键名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'entityColumnName','子表集合名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'childEntityColumnNames','所有集合名字', 'narchar',300,0,0,0,0,null,0,null
union all
select 2, 'keyType','主键类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'parentTableName','父表名', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'parentPrimaryKeyName','父主键名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 2, 'controlGroups','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 2, 'buttonGroups','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 2, 'events','数据集','collection', -1,0,0,2,0,null,0,null


--LiControlGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 3, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 3, 'panelModelId','外键','int',9,0,1,0,0,'id',1,'LiPanel'
union all
select 3, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 3, 'text','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 3, 'rowFieldName','行号字段名', 'narchar',20,0,0,0,0,null,0,null
union all
select 3, 'autoAllocation','是否自动分配', 'bit',9,0,0,0,0,null,0,null
union all
select 3, 'controls','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 3, 'buttonGroups','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 3, 'events','数据集','collection', -1,0,0,2,0,null,0,null

--LiButtonGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 4, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 4, 'formId','外键','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 4, 'panelModelId','外键','int',9,0,1,0,0,'id',1,'LiPanel'
union all
select 4, 'controlGroupId','外键','int',9,0,1,0,0,'id',1,'LiControlGroup'
union all
select 4, 'text','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 4, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 4, 'allowMinimize','allowMinimize', 'bit',9,0,0,0,0,null,0,null
union all
select 4, 'allowTextClipping','allowTextClipping', 'bit',9,0,0,0,0,null,0,null
union all
select 4, 'buttons','数据集','collection', -1,0,0,2,0,null,0,null

--select * from ColumnInfo where columnName = 'iIndex'
--LiButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 5, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 5, 'buttonGroupId','外键','int',9,0,1,0,0,'id',1,'LiButtonGroup'
union all
select 5, 'iIndex','索引', 'int',9,0,0,0,0,null,0,null
union all
select 5, 'caption','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'iconsize','图标大小', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'categoryGuid','类别ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 5, 'icon','图标名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'voucherStatus','单据状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'previousVoucherStatus','上个单据状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'statusFieldName','状态字段', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'bClearStatus','清除状态', 'bit',30,0,0,0,0,null,0,null
union all
select 5, 'entityKey','单据状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 5, 'events','数据集','collection', -1,0,0,2,0,null,0,null


--LiControl
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 6, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 6, 'controlGroupId','外键','int',9,0,1,0,0,'id',1,'LiControlGroup'
union all
select 6, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 6, 'text','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 6, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'col','列号', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'row','行号', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'colIndex','列表顺序', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'controltype','控件类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 6, 'length','长度', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'scale','精度', 'int',9,0,0,0,0,null,0,null
union all
select 6, 'bIsNull','是否为空', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'bReadOnly','是否只读', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'bRequired','是否必填', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'defaultVaule','默认值', 'nvarchar',5000,0,0,0,0,null,0,null
union all
select 6, 'controlDefaultVaule','控件默认值', 'nvarchar',5000,0,0,0,0,null,0,null
union all
select 6, 'basicInfoKey','基础档案Kdy', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoTableKey','基础档案主键字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoShowFieldName','基础档案显示字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoAssistType','基础档案辅助类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'basicInfoAssistFieldName','基础档案辅助项字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'dictInfoType','字典档案类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'bVisible','单据是否显示', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'bVisibleInList','列表是否显示', 'bit',9,0,0,0,0,null,0,null
union all
select 6, 'gridlookUpEditShowModelJson','Json字段', 'narchar',200,0,0,0,0,null,0,null
union all
select 6, 'basicInfoShowMode','基础档案显示模式', 'narchar',50,0,0,0,0,null,0,null
union all
select 6, 'controlEvents','数据集','collection', -1,0,0,2,0,null,0,null



--LiManageMeum
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 7, 'ID','主键','int',9,1,0,0,1,null,0,null
union all
select 7, 'Code','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 7, 'Name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 7, 'systemCode','系统代码', 'narchar',20,0,0,0,0,null,0,null
union all
select 7, 'isGroup','是否组', 'bit',30,0,0,0,0,null,0,null
union all
select 7, 'isSystem','是否系统', 'bit',30,0,0,0,0,null,0,null
union all
select 7, 'GroupId','组ID', 'int',30,0,0,0,0,null,0,null
union all
select 7, 'ParentID','父ID', 'int',9,0,0,0,0,null,0,null
union all
select 7, 'imageIndex','图标索引', 'int',9,0,0,0,0,null,0,null
union all
select 7, 'iOrder','排序','int',9,0,0,0,0,null,0,null


--V_SysDatabases
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName
,columnOrder,columnScale,columnIsNull,columnDefaultValue,bSearchColumns,bDisplayColumn,bVisible) 
select 8, 'dbid','主键','int',9,1,0,0,1,null,0,null
,1,0,0,null,0,0,0
union all
select 8, 'name','数据库名称','narchar',200,1,0,0,1,null,0,null
,2,0,0,null,1,1,1


--LiVoucherStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 9, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 9, 'code','编码', 'narchar',50,0,0,0,0,null,0,null
union all
select 9, 'name','名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 9, 'dataStatuss','数据集','collection', -1,0,0,2,0,null,0,null

--LiStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 10, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 10, 'fid','外键','int',9,0,1,0,0,'id',1,'LiVoucherStatus'
union all
select 10, 'code','编码', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'name','名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'bNew','新增', 'bit',9,0,0,0,0,null,0,null
union all
select 10, 'bShow','浏览', 'bit',9,0,0,0,0,null,0,null
union all
select 10, 'userFieldName','用户控件', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'dateFieldName','日期控件', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'statusFieldName','状态控件', 'narchar',50,0,0,0,0,null,0,null
union all
select 10, 'dataControlStatuss','数据集','collection', -1,0,0,2,0,null,0,null


--LiControlStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 11, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 11, 'fid','外键','int',9,0,1,0,0,'id',1,'LiStatus'
union all
select 11, 'code','编码', 'narchar',50,0,0,0,0,null,0,null
union all
select 11, 'name','名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 11, 'groupName','组名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 11, 'bReadOnly','只读', 'bit',9,0,0,0,0,null,0,null
union all
select 11, 'bVisibe','可视', 'bit',9,0,0,0,0,null,0,null
union all
select 11, 'defaultValue','组名称', 'narchar',5000,0,0,0,0,null,0,null


--LiDictGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 12, 'ID','主键','int',9,1,0,0,1,null,0,null
union all
select 12, 'Code','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 12, 'Name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 12, 'isGroup','是否组', 'bit',30,0,0,0,0,null,0,null
union all
select 12, 'isSystem','是否系统', 'bit',30,0,0,0,0,null,0,null
union all
select 12, 'GroupId','组ID', 'int',30,0,0,0,0,null,0,null
union all
select 12, 'ParentID','父ID', 'int',9,0,0,0,0,null,0,null
union all
select 12, 'systemCode','系统代码', 'narchar',9,0,0,0,0,null,0,null
union all
select 12, 'imageIndex','图标索引', 'int',9,0,0,0,0,null,0,null
union all
select 12, 'iOrder','排序','int',9,0,0,0,0,null,0,null
union all
select 12, 'dModifyDate','修改时间', 'datetime',50,0,0,0,0,null,0,null

--LiDict
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 13, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 13, 'dictParentID','外键', 'int',30,0,0,0,0,null,0,null
--select 13, 'dictParentID','外键','int',9,0,1,0,0,'id',1,'LiDictGroup'
union all
select 13, 'dictCode','编码', 'narchar',50,0,0,0,0,null,0,null
union all
select 13, 'dictName','名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 13, 'dictOrder','排序', 'int',50,0,0,0,0,null,0,null
union all
select 13, 'dictMemo','备注', 'narchar',50,0,0,0,0,null,0,null
union all
select 13, 'dModifyDate','修改时间', 'datetime',50,0,0,0,0,null,0,null


--LiVoucherStatus
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 14, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 14, 'dataBaseName','所属数据库', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityType','实体类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'systemCode','系统代码', 'narchar',10,0,0,0,0,null,0,null
union all
select 14, 'entityKey','单据Key', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityName','单据名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityOrder','是否是主表master', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'entityColumnName','对应主键上的字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableName','表名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableAliasName','表别名', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableAbbName','表中文名', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'tableDesc','表描述', 'narchar',5000,0,0,0,0,null,0,null
union all
select 14, 'className','类名', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'keyName','主键名', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'bDefaultBody','默认表体', 'bit',1,0,0,0,0,null,0,null
union all
select 14, 'childTableEntityColumnName','子表实体名', 'narchar',50,0,0,0,0,null,0,null
union all
select 14, 'modifyDate','修改时间', 'dateTime',50,0,0,0,0,null,0,null
union all
select 14, 'datas','数据集','collection', -1,0,0,2,0,null,0,null

--ColumnInfo
--select * from ColumnInfo where fid = 15
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 15, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 15, 'fid','外键','int',9,0,1,0,0,'id',1,'TableInfo'
union all
select 15, 'columnName','列名', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnAbbName','列名简称', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnType','列类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'length','长度', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKey','是否主键', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'foreignKey','是否外键', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'relationshipType','关系类型', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'databaseGeneratedType','自增类型', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKeyName','对应主表主键列名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKeyDatabaseGenerated','对应主表主键列', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'primaryKeyTableName','对应主表表名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnOrder','列顺序', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'columnScale','列小数位', 'int',50,0,0,0,0,null,0,null
union all
select 15, 'columnIsNull','列是否为空', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'columnDefaultValue','列默认值', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'bSearchColumns','该列是否可搜索', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'bDisplayColumn','该列是否显示', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'bVisible','是否显示', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'modifyDate','修改时间', 'dateTime',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoType','基础档案类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'dictInfoType','字典类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoShowFieldName','基础档案显示属性', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoRelationFieldName','基础档案关联字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'basicInfoKeyFieldName','基础档案主键字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'columnWidth','列宽度', 'int',9,0,0,0,0,null,0,null
union all
select 15, 'controlType','控件类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'gridlookUpEditShowModelJson','显示配置', 'narchar',4000,0,0,0,0,null,0,null
union all
select 15, 'bExtendField','是否扩展字段表', 'bit',50,0,0,0,0,null,0,null
union all
select 15, 'extendTableName','扩展字段表名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'extendTableKeyFieldName','扩展字段表名外键', 'narchar',50,0,0,0,0,null,0,null
union all
select 15, 'extendRelationTableKeyFieldName','扩展关联表主键', 'narchar',50,0,0,0,0,null,0,null

--LiAdminMeum
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 16, 'ID','主键','int',9,1,0,0,1,null,0,null
union all
select 16, 'Code','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 16, 'Name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 16, 'isGroup','是否组', 'bit',30,0,0,0,0,null,0,null
union all
select 16, 'isSystem','是否系统', 'bit',30,0,0,0,0,null,0,null
union all
select 16, 'GroupId','组ID', 'int',30,0,0,0,0,null,0,null
union all
select 16, 'ParentID','父ID', 'int',9,0,0,0,0,null,0,null
union all
select 16, 'imageIndex','图标索引', 'int',9,0,0,0,0,null,0,null
union all
select 16, 'iOrder','排序','int',9,0,0,0,0,null,0,null


--LiButtonGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 17, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 17, 'formId','外键','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 17, 'panelModelId','外键','int',9,0,1,0,0,'id',1,'LiPanel'
union all
select 17, 'controlGroupId','外键','int',9,0,1,0,0,'id',1,'LiControlGroup'
union all
select 17, 'buttonId','外键','int',9,0,1,0,0,'id',1,'LiButton'
union all
select 17, 'listButtonId','外键','int',9,0,1,0,0,'id',1,'LiListButton'

union all
select 17, 'fullName','全名称', 'narchar',300,0,0,0,0,null,0,null
union all
select 17, 'assemblyName','程序集', 'narchar',50,0,0,0,0,null,0,null
union all
select 17, 'eventMemo','全名称', 'narchar',300,0,0,0,0,null,0,null


--LiManageMeum
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 18, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 18, 'eventType','类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 18, 'eventName','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 18, 'eventFullName','全名称', 'narchar',300,0,0,0,0,null,0,null
union all
select 18, 'eventAssemblyName','程序集', 'narchar',50,0,0,0,0,null,0,null


--LiListButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 19, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 19, 'formId','外键','int',9,0,1,0,0,'id',1,'LiForm'
union all
select 19, 'iIndex','顺序', 'int',9,0,0,0,0,null,0,null
union all
select 19, 'caption','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'iconsize','图标大小', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'categoryGuid','类别ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 19, 'icon','图标名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'voucherStatus','单据状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 19, 'events','数据集','collection', -1,0,0,2,0,null,0,null



--LiQueryScheme
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 20, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 20, 'entityKey','单据Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 20, 'userCode','类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 20, 'querySchemeName','查询方案', 'narchar',30,0,0,0,0,null,0,null
union all
select 20, 'querys','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 20, 'entitys','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 20, 'fields','数据集','collection', -1,0,0,2,0,null,0,null



--LiQuery
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 21, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 21, 'querySchemeId','外键','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 21, 'sBracketsBefore','前括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'sFieldName','字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 21, 'sJudgmentSymbol','判断符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'oQueryValue','查询值', 'narchar',4000,0,0,0,0,null,0,null
union all
select 21, 'sJoin','连接符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'sBracketsAfter','后括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 21, 'sFieldType','控件类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 21, 'sBasicCode','基础档案', 'narchar',30,0,0,0,0,null,0,null


--LiEntity
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 22, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 22, 'querySchemeId','外键','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 22, 'sEntityType','实体类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'sEntityCode','实体编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'sEntityName','实体名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'sTableName','实体名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 22, 'iShow','是否显示', 'bit',30,0,0,0,0,null,0,null


--LiField
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 23, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 23, 'querySchemeId','外键','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 23, 'convertId','外键','int',9,0,1,0,0,'id',1,'LiConvertHead'
union all
select 23, 'code','列表字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'fieldName','实体字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'columnFieldName','实体字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'name','列表字段名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'sEntityCode','实体类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'sEntityName','实体类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 23, 'iColumnWidth','列宽', 'int',9,0,0,0,0,null,0,null
union all
select 23, 'bColumnDisplay','是否显示', 'bit',30,0,0,0,0,null,0,null
union all
select 23, 'bQuery','快速查询', 'bit',30,0,0,0,0,null,0,null
union all
select 23, 'bRange','区间', 'bit',30,0,0,0,0,null,0,null
union all
select 23, 'sColumnControlType','控件类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'sRefTypeCode','引用类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'sJudgeSymbol','判断符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 23, 'basicInfoKey','基础档案Kdy', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'dictInfoType','字典档案类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 23, 'gridlookUpEditShowModelJson','Json字段', 'narchar',200,0,0,0,0,null,0,null


--LiUsers
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 24, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 24, 'skinName','皮肤', 'narchar',50,0,0,0,0,null,0,null
union all
select 24, 'bAdmin','是否管理员', 'bit',10,0,0,0,0,null,0,null
union all
select 24, 'modifyDate','修改时间', 'dateTime',50,0,0,0,0,null,0,null

--LiRoles
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 25, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 25, 'roleCode','角色编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 25, 'roleName','角色名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 25, 'modifyDate','修改时间', 'dateTime',50,0,0,0,0,null,0,null

--LiAuthData
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 26, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 26, 'entityKey','实体Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'roleCode','角色编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'code','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 26, 'bShow','是否显示', 'bit',10,0,0,0,0,null,0,null
union all
select 26, 'bEdit','是否编辑', 'bit',10,0,0,0,0,null,0,null

--LiAuth
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 27, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 27, 'entityKey','实体Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'roleCode','角色编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'code','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 27, 'bShow','是否显示', 'bit',10,0,0,0,0,null,0,null


--LiAuth
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 28, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 28, 'userCode','用户编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 28, 'roleCode','角色编码', 'narchar',30,0,0,0,0,null,0,null

--LiVoucherCode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 29, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 29, 'entityKey','实体Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'bDefault','默认方案', 'bit',10,0,0,0,0,null,0,null
union all
select 29, 'name','方案名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'prefixName','前缀', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'fieldTextName','单据文本字段', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'fieldDateName','单据日期字段', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'dateTimeFormat','日期格式', 'narchar',30,0,0,0,0,null,0,null
union all
select 29, 'iZero','左边补0', 'int',9,0,0,0,0,null,0,null
union all
select 29, 'iStep','流水号步位', 'int',9,0,0,0,0,null,0,null
union all
select 29, 'flowNoRange','流水号范围', 'narchar',10,0,0,0,0,null,0,null


--LiConvertHead
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 30, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 30, 'convertCode','转换编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 30, 'convertName','转换名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertDestType','目标类型', 'narchar',20,0,0,0,0,null,0,null
union all
select 30, 'convertDest','目标', 'narchar',100,0,0,0,0,null,0,null
union all
select 30, 'convertSourceType','源类型', 'narchar',20,0,0,0,0,null,0,null
union all
select 30, 'convertSource','源', 'narchar',100,0,0,0,0,null,0,null
union all
select 30, 'convertRelation','转换关系', 'narchar',20,0,0,0,0,null,0,null
union all
select 30, 'convertRelationField','转换关系字段', 'narchar',100,0,0,0,0,null,0,null
union all
select 30, 'convertDestHeadName','目标表头名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertDestBodyName','目标表体名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeField','累计字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertPushField','下推数量字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertPushTableName','下推表名', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'bSourceTableFiliter','源表过滤', 'bit',10,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeTextField','累计文本字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeIDField','累计ID字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeTableName','累计表名', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'convertCumulativeDatabaseName','累计表名数据库', 'narchar',50,0,0,0,0,null,0,null
union all
select 30, 'datas','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 30, 'queryFields','数据集','collection', -1,0,0,2,0,null,0,null

--LiConvertBody

insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 31, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 31, 'fid','外键','int',9,0,1,0,0,'id',1,'LiConvertHead'
union all
select 31, 'convertDestType','目标类型', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertDCollection','目标集合名称', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertDestField','目标字段', 'narchar',100,0,0,0,0,null,0,null
union all
select 31, 'convertSourceType','源类型', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertSCollection','源集合名称', 'narchar',20,0,0,0,0,null,0,null
union all
select 31, 'convertSourceField','源字段', 'narchar',100,0,0,0,0,null,0,null
union all
select 31, 'bRef','是否引用', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'refBasicInfoType','引用基础档案', 'narchar',30,0,0,0,0,null,0,null
union all
select 31, 'refBasicInfoField','引用对照字段', 'narchar',100,0,0,0,0,null,0,null
union all
select 31, 'refBasicInfoValueField','引用对照字段', 'narchar',50,0,0,0,0,null,0,null
union all
select 31, 'bDefault','是否使用默认值', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'defaultValue','默认值字段', 'narchar',4000,0,0,0,0,null,0,null
union all
select 31, 'reverseIdFieldName','反写ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 31, 'reverseCodeFieldName','反写编码', 'narchar',50,0,0,0,0,null,0,null
union all
select 31, 'bReverseType','反写类型', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'bCumulativeRelationQty','累计关联数量字段', 'bit',10,0,0,0,0,null,0,null
union all
select 31, 'bCumulativeRelationID','累计关联ID字段', 'bit',10,0,0,0,0,null,0,null

--select * from LiField

insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 32, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 32, 'querySchemeId','外键','int',9,0,1,0,0,'id',1,'LiQueryScheme'
union all
select 32, 'code','列表字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'name','列表字段名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'convertId','外键','int',9,0,1,0,0,'id',1,'LiConvertHead'
union all
select 32, 'fieldName','实体字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'columnFieldName','实体字段名', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'sEntityCode','实体类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'sEntityName','实体类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 32, 'iColumnWidth','列宽', 'int',9,0,0,0,0,null,0,null
union all
select 32, 'bColumnDisplay','是否显示', 'bit',30,0,0,0,0,null,0,null
union all
select 32, 'bQuery','快速查询', 'bit',30,0,0,0,0,null,0,null
union all
select 32, 'bRange','区间', 'bit',30,0,0,0,0,null,0,null
union all
select 32, 'sColumnControlType','控件类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 32, 'sRefTypeCode','引用类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 32, 'sJudgeSymbol','判断符号', 'narchar',10,0,0,0,0,null,0,null


--LiFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 33, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 33, 'flowCode','流程编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 33, 'flowName','流程名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 33, 'entityKey','单据编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 33, 'entityName','单据名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 33, 'bDefault','是否默认', 'bit',1,0,0,0,0,null,0,null
union all
select 33, 'nodes','数据集','collection', -1,0,0,2,0,null,0,null


--LiFlowNode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 34, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 34, 'flowId','外键','int',9,0,1,0,0,'id',1,'LiFlow'
union all
select 34, 'flowNodeCode','流程节点编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 34, 'flowNodeName','流程节点名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 34, 'flowNodeType','流程节点类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 34, 'flowNodeInformation','流程信息', 'narchar',500,0,0,0,0,null,0,null
union all
select 34, 'X','X', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'Y','Y', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 34, 'users','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 34, 'connectors','数据集','collection', -1,0,0,2,0,null,0,null


--LiFlowUser
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 35, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 35, 'flowNodeId','外键','int',9,0,1,0,0,'id',1,'LiFlowNode'
union all
select 35, 'userCode','用户编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 35, 'userName','用户名称', 'narchar',50,0,0,0,0,null,0,null


--LiFlowConnector
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 36, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 36, 'flowNodeId','外键','int',9,0,1,0,0,'id',1,'LiFlowNode'
union all
select 36, 'flowNodeCodeTo','节点编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 36, 'flowNodeNameTo','节点名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 36, 'flowNodeFormIndex','源节点哪个锚点', 'int',9,0,0,0,0,null,0,null
union all
select 36, 'flowNodeToIndex','目标节点哪个锚点', 'int',9,0,0,0,0,null,0,null
union all
select 36, 'conditions','数据集','collection', -1,0,0,2,0,null,0,null

--LiFlowCondition
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 37, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 37, 'flowConnectorId','外键','int',9,0,1,0,0,'id',1,'LiFlowConnector'
union all
select 37, 'sBracketsBefore','前括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'sFieldName','字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 37, 'sJudgmentSymbol','判断符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'oQueryValue','查询值', 'narchar',4000,0,0,0,0,null,0,null
union all
select 37, 'sJoin','连接符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'sBracketsAfter','后括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 37, 'sFieldType','控件类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 37, 'sBasicCode','基础档案', 'narchar',30,0,0,0,0,null,0,null




--LiVersionFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 38, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 38, 'flowVersionNumber','流程版本号', 'narchar',30,0,0,0,0,null,0,null
union all
select 38, 'flowCode','流程编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 38, 'flowName','流程名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 38, 'entityKey','单据编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 38, 'entityName','单据名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 38, 'openStatus','开启状态', 'narchar',50,0,0,0,0,null,0,null
union all
select 38, 'bDefault','是否默认', 'bit',1,0,0,0,0,null,0,null
union all
select 38, 'flowVersionDate','流程日期', 'dateTime',50,0,0,0,0,null,0,null
union all
select 38, 'nodes','数据集','collection', -1,0,0,2,0,null,0,null


--LiVersionFlowNode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 39, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 39, 'flowId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlow'
union all
select 39, 'flowNodeCode','流程节点编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 39, 'flowNodeName','流程节点名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 39, 'flowNodeType','流程节点类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 39, 'flowNodeInformation','流程信息', 'narchar',500,0,0,0,0,null,0,null
union all
select 39, 'X','X', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'Y','Y', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 39, 'users','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 39, 'connectors','数据集','collection', -1,0,0,2,0,null,0,null


--LiVersionFlowUser
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 40, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 40, 'flowNodeId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 40, 'userCode','用户编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 40, 'userName','用户名称', 'narchar',50,0,0,0,0,null,0,null


--LiVersionFlowConnector
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 41, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 41, 'flowNodeId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 41, 'flowNodeCodeTo','节点编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 41, 'flowNodeNameTo','节点名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 41, 'flowNodeFormIndex','源节点哪个锚点', 'int',9,0,0,0,0,null,0,null
union all
select 41, 'flowNodeToIndex','目标节点哪个锚点', 'int',9,0,0,0,0,null,0,null
union all
select 41, 'conditions','数据集','collection', -1,0,0,2,0,null,0,null

--LiVersionFlowCondition
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 42, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 42, 'flowConnectorId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlowConnector'
union all
select 42, 'sBracketsBefore','前括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'sFieldName','字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 42, 'sJudgmentSymbol','判断符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'oQueryValue','查询值', 'narchar',4000,0,0,0,0,null,0,null
union all
select 42, 'sJoin','连接符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'sBracketsAfter','后括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 42, 'sFieldType','控件类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 42, 'sBasicCode','基础档案', 'narchar',30,0,0,0,0,null,0,null


--LiVoucherFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 43, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 43, 'flowVersionId','流程版本号ID', 'int',30,0,0,0,0,null,0,null
union all
select 43, 'flowVersionNumber','流程版本号', 'narchar',50,0,0,0,0,null,0,null
union all
select 43, 'flowCode','流程编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowName','流程名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'entityKey','单据编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'entityName','单据名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'voucherId','单据ID', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'voucherCode','单据编号', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowStatus','流程状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowTitle','流程标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 43, 'flowStartDate','流程开始时间', 'dateTime',30,0,0,0,0,null,0,null
union all
select 43, 'flowEndDate','流程结束时间', 'dateTime',30,0,0,0,0,null,0,null
union all
select 43, 'datas','数据集','collection', -1,0,0,2,0,null,0,null


--LiVoucherFlowStep
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 44, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 44, 'flowVoucherId','外键','int',9,0,1,0,0,'id',1,'LiVoucherFlow'
union all
select 44, 'flowSeq','流程顺序', 'int',10,0,0,0,0,null,0,null
union all
select 44, 'flowVersionNextStepNodeId','流程下一步', 'int',10,0,0,0,0,null,0,null
union all
select 44, 'flowVersionNodeId','流程下一步', 'int',10,0,0,0,0,null,0,null
union all
select 44, 'flowUserCode','流程用户编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowUserName','流程用户名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowApprovalType','审批类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowContent','流程内容', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowStatus','流程状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 44, 'flowDate','流程时间', 'dateTime',30,0,0,0,0,null,0,null


--LiVersionFlowNode
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 45, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 45, 'flowId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlow'
union all
select 45, 'flowNodeCode','流程节点编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 45, 'flowNodeName','流程节点名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 45, 'flowNodeType','流程节点类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 45, 'flowNodeInformation','流程信息', 'narchar',500,0,0,0,0,null,0,null
union all
select 45, 'X','X', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'Y','Y', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 45, 'users','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 45, 'connectors','数据集','collection', -1,0,0,2,0,null,0,null


--LiVersionFlowUser
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 46, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 46, 'flowNodeId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 46, 'userCode','用户编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 46, 'userName','用户名称', 'narchar',50,0,0,0,0,null,0,null


--LiVersionFlowConnector
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 47, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 47, 'flowNodeId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlowNode'
union all
select 47, 'flowNodeCodeTo','节点编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 47, 'flowNodeNameTo','节点名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 47, 'flowNodeFormIndex','源节点哪个锚点', 'int',9,0,0,0,0,null,0,null
union all
select 47, 'flowNodeToIndex','目标节点哪个锚点', 'int',9,0,0,0,0,null,0,null
union all
select 47, 'conditions','数据集','collection', -1,0,0,2,0,null,0,null

--LiVersionFlowCondition
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 48, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 48, 'flowConnectorId','外键','int',9,0,1,0,0,'id',1,'LiVersionFlowConnector'
union all
select 48, 'sBracketsBefore','前括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'sFieldName','字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 48, 'sJudgmentSymbol','判断符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'oQueryValue','查询值', 'narchar',4000,0,0,0,0,null,0,null
union all
select 48, 'sJoin','连接符号', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'sBracketsAfter','后括号', 'narchar',10,0,0,0,0,null,0,null
union all
select 48, 'sFieldType','控件类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 48, 'sBasicCode','基础档案', 'narchar',30,0,0,0,0,null,0,null

--LiFlow
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 49, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 49, 'messageType','消息类型', 'narchar',20,0,0,0,0,null,0,null
union all
select 49, 'messageDate','消息日期', 'dateTime',9,0,0,0,0,null,0,null
union all
select 49, 'messageContent','消息内容', 'bit',1,0,0,0,0,null,0,null
union all
select 49, 'messageRead','是否已阅', 'narchar',500,0,0,0,0,null,0,null
union all
select 49, 'voucherFlowId','单据流程ID', 'int',9,0,0,0,0,null,0,null
union all
select 49, 'flowVersionId','流程版本ID', 'int',9,0,0,0,0,null,0,null
union all
select 49, 'flowVersionNumber','流程版本号', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'flowCode','流程编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'flowName','流程名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 49, 'entityKey','单据编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'entityName','单据名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 49, 'voucherId','单据ID', 'int',9,0,0,0,0,null,0,null
union all
select 49, 'voucherCode','单据编号', 'narchar',30,0,0,0,0,null,0,null
union all
select 49, 'userCode','用户编码', 'narchar',30,0,0,0,0,null,0,null


--ProcedureInfo
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 50, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 50, 'dataBaseName','数据库名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 50, 'systemCode','系统代码', 'narchar',10,0,0,0,0,null,0,null
union all
select 50, 'entityKey','实体Key', 'narchar',30,0,0,0,0,null,0,null
union all
select 50, 'procedureName','存储过程名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 50, 'datas','数据集','collection', -1,0,0,2,0,null,0,null

--ParamInfo
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 51, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 51, 'fid','外键','int',9,0,1,0,0,'id',1,'ProcedureInfo'
union all
select 51, 'paramName','参数名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 51, 'paramType','参数类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 51, 'paramLength','参数长度', 'int',9,0,0,0,0,null,0,null


--LiSystemInfo
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 52, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 52, 'systemCode','系统代码', 'narchar',10,0,0,0,0,null,0,null
union all
select 52, 'systemDataBaseName','系统数据库', 'narchar',20,0,0,0,0,null,0,null
union all
select 52, 'systemName','系统名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 52, 'systemU8MenuId','系统U8菜单ID', 'narchar',10,0,0,0,0,null,0,null
union all
select 52, 'systemTitle','系统标题', 'narchar',50,0,0,0,0,null,0,null
union all
select 52, 'companyName','公司名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 52, 'companyLogo','公司Logo', 'narchar',5000,0,0,0,0,null,0,null
union all
select 52, 'bDefault','默认信息', 'bit',1,0,0,0,0,null,0,null


--LiU8Voucher
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 53, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 53, 'code','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 53, 'name','名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 53, 'voucherType','单据类型', 'narchar',10,0,0,0,0,null,0,null
union all
select 53, 'operations','数据集','collection', -1,0,0,2,0,null,0,null

--LiU8Operation
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 54, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 54, 'fid','外键','int',9,0,1,0,0,'id',1,'LiU8Voucher'
union all
select 54, 'operationCode','操作编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 54, 'operationName','操作名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 54, 'operationSymbol','操作符号', 'narchar',20,0,0,0,0,null,0,null
union all
select 54, 'paramModels','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 54, 'fields','数据集','collection', -1,0,0,2,0,null,0,null

--LiU8Param
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 55, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 55, 'fid','外键','int',9,0,1,0,0,'id',1,'LiU8Operation'
union all
select 55 'paramName','参数名', 'narchar',30,0,0,0,0,null,0,null
union all
select 55, 'paramDesc','参数描述', 'narchar',200,0,0,0,0,null,0,null
union all
select 55, 'paramType','参数类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 55, 'paramDirection','参数方向', 'narchar',20,0,0,0,0,null,0,null
union all
select 55, 'paramTransMode','传递方式', 'narchar',20,0,0,0,0,null,0,null
union all
select 55, 'paramIsRequire','是否可选', 'bit',1,0,0,0,0,null,0,null
union all
select 55, 'paramBoObject','BO对象', 'narchar',50,0,0,0,0,null,0,null
union all
select 55, 'paramBoType','是否BO表头', 'nvarchar',50,0,0,0,0,null,0,null
union all
select 55, 'paramDefaultValue','默认值', 'narchar',200,0,0,0,0,null,0,null
union all
select 55, 'parambID','是否是ID字段名', 'bit',1,0,0,0,0,null,0,null
union all
select 55, 'parambCode','是否是Code字段名', 'bit',1,0,0,0,0,null,0,null


--LiU8Field
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 56, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 56, 'fid','外键','int',9,0,1,0,0,'id',1,'LiU8Operation'
union all
select 56, 'fieldEntityType','实体类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 56, 'fieldName','字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 56, 'fieldDesc','字段描述', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldType','字段类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 56, 'fieldIsRequire','是否必输', 'bit',1,0,0,0,0,null,0,null
union all
select 56, 'fieldMaxValue','最大值', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldMinValue','最小值', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldDefaultValue','默认值', 'narchar',200,0,0,0,0,null,0,null
union all
select 56, 'fieldbDefault','是否默认', 'bit',1,0,0,0,0,null,0,null
union all
select 56, 'fieldLength','最大长度', 'int',9,0,0,0,0,null,0,null


--LiU8EnvContext
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 57, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 57, 'fid','外键','int',9,0,1,0,0,'id',1,'LiU8Operation'
union all
select 57 'contextName','上下文名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 57, 'contextDesc','描述', 'narchar',200,0,0,0,0,null,0,null
union all
select 57, 'contextType','上下文类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 57, 'contextDefaultValue','默认值', 'narchar',200,0,0,0,0,null,0,null


--LiPushForm
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 58, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 58, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 58, 'text','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 58, 'height','高度', 'int',9,0,0,0,0,null,0,null
union all
select 58, 'width','宽度', 'int',9,0,0,0,0,null,0,null
union all
select 58, 'systemCode','系统代码', 'narchar',20,0,0,0,0,null,0,null
union all
select 58, 'pushEvents','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 58, 'pushListButtons','数据集','collection', -1,0,0,2,0,null,0,null



--LiPushListButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 59, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 59, 'pushFormId','外键','int',9,0,1,0,0,'id',1,'LiPushForm'
union all
select 59, 'iIndex','顺序', 'int',9,0,0,0,0,null,0,null
union all
select 59, 'caption','标题', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'name','名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'iconsize','图标大小', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'categoryGuid','类别ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 59, 'icon','图标名称', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'voucherStatus','单据状态', 'narchar',30,0,0,0,0,null,0,null
union all
select 59, 'pushEvents','数据集','collection', -1,0,0,2,0,null,0,null

--LiPushEvent
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 60, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 60, 'pushFormId','外键','int',9,0,1,0,0,'id',1,'LiPushForm'
union all
select 60, 'pushListButtonId','外键','int',9,0,1,0,0,'id',1,'LiPushListButton'

union all
select 60, 'fullName','全名称', 'narchar',300,0,0,0,0,null,0,null
union all
select 60, 'assemblyName','程序集', 'narchar',50,0,0,0,0,null,0,null
union all
select 60, 'eventMemo','全名称', 'narchar',300,0,0,0,0,null,0,null


--LiButtonGroup
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 61, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 61, 'controlId','外键','int',9,0,1,0,0,'id',1,'LiControl'
union all
select 61, 'eventType','事件类型', 'narchar',30,0,0,0,0,null,0,null
union all
select 61, 'eventExpression','事件表达式', 'narchar',5000,0,0,0,0,null,0,null
union all
select 61, 'bEnable','是否启用', 'bit',1,0,0,0,0,null,0,null
union all
select 61, 'eventMemo','事件类型', 'narchar',300,0,0,0,0,null,0,null




--LiReport
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 62, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 62, 'reportKey','编码', 'narchar',30,0,0,0,0,null,0,null
union all
select 62, 'reportName','名称', 'narchar',50,0,0,0,0,null,0,null
union all
select 62, 'systemCode','系统帐套号', 'narchar',10,0,0,0,0,null,0,null
union all
select 62, 'reportSql','报表SQL', 'narchar',5000,0,0,0,0,null,0,null
union all
select 62, 'reportCountSql','报表总数SQL', 'narchar',5000,0,0,0,0,null,0,null
union all
select 62, 'datas','数据集','collection', -1,0,0,2,0,null,0,null
union all
select 62, 'buttons','数据集','collection', -1,0,0,2,0,null,0,null

--LiReportField
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 63, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 63, 'reportId','外键','int',9,0,1,0,0,'id',1,'LiReport'
union all
select 63, 'columnName','字段名', 'narchar',50,0,0,0,0,null,0,null
union all
select 63, 'columnCaption','列名', 'narchar',50,0,0,0,0,null,0,null
union all
select 63, 'columnType','列类型', 'narchar',100,0,0,0,0,null,0,null
union all
select 63, 'iColumnWidth','列宽', 'int',9,0,0,0,0,null,0,null
union all
select 63, 'bColumnDisplay','是否显示列', 'bit',1,0,0,0,0,null,0,null
union all
select 63, 'iColumnIndex','列索引', 'int',9,0,0,0,0,null,0,null
union all
select 63, 'bQuery','是否查询', 'bit',1,0,0,0,0,null,0,null
union all
select 63, 'iDisplayFormatType','显示格式类型', 'int',9,0,0,0,0,null,0,null
union all
select 63, 'displayFormat','显示格式', 'narchar',30,0,0,0,0,null,0,null
union all
select 63, 'bColumnGroup','是否汇总', 'bit',1,0,0,0,0,null,0,null
union all
select 63, 'columnGroupFormat','汇总格式', 'narchar',30,0,0,0,0,null,0,null


--LiReportButton
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 64, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 64, 'reportId','外键','int',9,0,1,0,0,'id',1,'LiReport'
union all
select 64, 'caption','标题', 'narchar',20,0,0,0,0,null,0,null
union all
select 64, 'name','名称', 'narchar',20,0,0,0,0,null,0,null
union all
select 64, 'iconsize','按钮类型', 'narchar',50,0,0,0,0,null,0,null
union all
select 64, 'categoryGuid','类别ID', 'narchar',50,0,0,0,0,null,0,null
union all
select 64, 'icon','图标', 'narchar',50,0,0,0,0,null,0,null
union all
select 64, 'iIndex','顺序', 'int',9,0,0,0,0,null,0,null
union all
select 64, 'events','数据集','collection', -1,0,0,2,0,null,0,null

--LiReportEvent
insert into ColumnInfo (fid,columnName,columnAbbName,columnType,length,primaryKey,foreignKey,relationshipType,databaseGeneratedType,primaryKeyName,primaryKeyDatabaseGenerated,primaryKeyTableName) 
select 65, 'id','主键','int',9,1,0,0,1,null,0,null
union all
select 65, 'reportButtonId','外键','int',9,0,1,0,0,'id',1,'LiReportButton'
union all
select 65, 'fullName','全名称', 'narchar',20,0,0,0,0,null,0,null
union all
select 65, 'assemblyName','程序集', 'narchar',20,0,0,0,0,null,0,null
union all
select 65, 'eventMemo','备注', 'narchar',20,0,0,0,0,null,0,null
GO

exec sp_CreateTable 4
exec sp_CreateTable 11

--select * from LiForm
--select * from TableInfo
--select * from TestHead