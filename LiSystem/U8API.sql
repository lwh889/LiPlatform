
create table LiU8Voucher(
	id int identity(1,1) primary key not null,
	[code]  nvarchar(30),	--编码
	[name]  nvarchar(50),	--名称
	voucherType  nvarchar(10),	--单据类型
	dCreateDate datetime default getdate()
)


create table LiU8Operation(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Voucher(id),
	operationCode  nvarchar(30),	--操作编码
	operationName  nvarchar(50),	--操作名称
	operationSymbol  nvarchar(20),	--操作符号
	dCreateDate datetime default getdate()
)


create table LiU8Field(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Operation(id),
	fieldEntityType  nvarchar(30),	--实体类型
	fieldName  nvarchar(50),	--字段名
	fieldDesc  nvarchar(200),	--字段描述
	fieldType  nvarchar(50),	--字段类型
	fieldIsRequire  bit,	--是否必输
	fieldMaxValue  nvarchar(200),	--最大值
	fieldMinValue  nvarchar(200),	--最小值
	fieldDefaultValue  nvarchar(200),	--默认值
	fieldbDefault  bit,	--是否默认
	fieldLength  int,	--最大长度
	dCreateDate datetime default getdate()
)

--alter table LiU8Field add fieldDefaultValue  nvarchar(200)
--alter table LiU8Field add fieldbDefault  bit


create table LiU8Param(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Operation(id),
	paramName  nvarchar(30),	--参数名
	paramDesc  nvarchar(200),	--参数描述
	paramType  nvarchar(50),	--参数类型
	paramDirection  nvarchar(20),	--参数方向
	paramTransMode  nvarchar(20),	--传递方式
	paramIsRequire  bit,	--是否可选
	paramBoObject  nvarchar(50),	--BO对象
	paramBoType  nvarchar(50),	--是否BO表头
	paramDefaultValue  nvarchar(200),
	parambID  bit,	--是否是ID字段名
	parambCode  bit,	--是否是Code字段名
	dCreateDate datetime default getdate()
)
--alter table LiU8Param add paramDefaultValue  nvarchar(200)
--alter table LiU8Param add parambID  bit
--alter table LiU8Param add parambCode  bit


--API上下文
create table LiU8EnvContext(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8Operation(id),
	contextName  nvarchar(30),	--上下文名称
	contextDesc  nvarchar(200),	--	描述
	contextType  nvarchar(50),	--	上下文类型
	contextDefaultValue  nvarchar(200),
	dCreateDate datetime default getdate()
)
--alter table LiU8EnvContext add contextDefaultValue  nvarchar(200)


