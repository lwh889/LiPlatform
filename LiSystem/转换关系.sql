create table LiConvertHead(
	id int identity(1,1) primary key not null,
	convertName nvarchar(50),	--转换名称
	convertDestType nvarchar(20),	--目标类型
	convertDCollection nvarchar(20),	--目标集合名称
	convertDest nvarchar(100),	--目标
	convertSourceType nvarchar(20),	--源类型
	convertSCollection nvarchar(20),	--源集合名称
	convertSource nvarchar(100),	--源
	convertRelation nvarchar(20),	--转换关系
	convertRelationField nvarchar(100),	--转换关系字段
	convertDestHeadName nvarchar(50),	--目标表头名称
	convertDestBodyName nvarchar(50),	--目标表体名称
	convertPushField nvarchar(50),	--下推数量字段
	convertCumulativeField nvarchar(50),	--累计字段
	convertCumulativeIDField nvarchar(50),	--累计ID字段
	convertCumulativeTableName nvarchar(50),	--累计表名
	convertCumulativeTextField nvarchar(50),	--累计文本字段
	convertCumulativeDatabaseName nvarchar(50),	--累计表名数据库
	bSourceTableFiliter bit default 0,	--源表过滤
	convertPushTableName nvarchar(50),	--下推表名
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()


	)
	--alter table LiConvertHead add convertCode nvarchar(50)
	--alter table LiConvertHead add convertDestHeadName nvarchar(50)
	--alter table LiConvertHead add convertDestBodyName nvarchar(50)
	--alter table LiConvertHead add convertPushField nvarchar(50)
	--alter table LiConvertHead add convertCumulativeField nvarchar(50)
	--alter table LiConvertHead add convertPushTableName nvarchar(50)
	--alter table LiConvertHead add convertCumulativeIDField nvarchar(50)
	--alter table LiConvertHead add convertCumulativeTableName nvarchar(50)
	--alter table LiConvertHead add bSourceTableFiliter bit default 0
	--alter table LiConvertHead add convertCumulativeTextField nvarchar(50)
	--alter table LiConvertHead add convertCumulativeDatabaseName nvarchar(50)
	
	--select * from LiConvertHead
create table LiConvertBody(
	
	id int identity(1,1) primary key,
	fid int REFERENCES LiConvertHead(id),
	convertDestType nvarchar(20),	--目标类型
	convertDestField nvarchar(100),	--目标字段
	convertSourceType nvarchar(20),	--源类型
	convertSourceField nvarchar(100),	--源字段
	bRef bit default 0,	--是否引用
	refBasicInfoType nvarchar(30),	--引用基础档案
	refBasicInfoField nvarchar(100),	--引用对照字段
	refBasicInfoValueField nvarchar(50),	--引用值字段
	defaultValue nvarchar(4000),	--默认值字段
	bDefault bit,	--是否使用默认值
	reverseIdFieldName nvarchar(50),	--反写ID
	reverseCodeFieldName nvarchar(50),	--反写编码
	bReverseType bit,	--反写类型
	bCumulativeRelationQty bit default 0,	--累计关联数量字段
	bCumulativeRelationID bit default 0,	--累计关联ID字段
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

	--alter table LiConvertBody add convertDCollection nvarchar(20)
	--alter table LiConvertBody add convertSCollection nvarchar(20)
	--alter table LiConvertBody add refBasicInfoValueField nvarchar(50)
	--alter table LiConvertBody add defaultValue nvarchar(4000)
	--alter table LiConvertBody add bDefault bit
	--alter table LiConvertBody add reverseCodeFieldName nvarchar(50)
	--alter table LiConvertBody add reverseIdFieldName nvarchar(50)
	--alter table LiConvertBody add bReverseType bit
	--alter table LiConvertBody add bCumulativeRelationQty bit default 0
	--alter table LiConvertBody add bCumulativeRelationID bit default 0