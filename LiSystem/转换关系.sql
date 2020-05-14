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

	modifyDate datetime,	--修改时间
	createDate datetime default getdate()


	)
	--alter table LiConvertHead add convertCode nvarchar(50)

	select * from LiConvertHead
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
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
)

	--alter table LiConvertBody add convertDCollection nvarchar(20)
	--alter table LiConvertBody add convertSCollection nvarchar(20)