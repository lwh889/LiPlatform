drop table LiU8COField
drop table LiU8COVoucher
create table LiU8COVoucher(
	id int identity(1,1) primary key not null,
	[code]  nvarchar(30),	--编码
	[name]  nvarchar(50),	--名称
	voucherType  nvarchar(10),	--单据类型
	voucherClassify  nvarchar(10),	--单据分类
	headKeyFieldName  nvarchar(20) ,
	bodyKeyFieldName  nvarchar(20) ,
	cardNumber  nvarchar(20) ,
	domHeadSql  nvarchar(4000),	--单据分类
	domBodySql  nvarchar(4000),	--单据分类
	timeStampSql  nvarchar(4000),	--单据分类
	dCreateDate datetime default getdate()
)

--alter table LiU8COVoucher add timeStampSql  nvarchar(4000) 
--alter table LiU8COVoucher add headKeyFieldName  nvarchar(20) 
--alter table LiU8COVoucher add bodyKeyFieldName  nvarchar(20) 
--alter table LiU8COVoucher add cardNumber  nvarchar(20) 

create table LiU8COField(
	id int identity(1,1) primary key not null,
	fid int REFERENCES LiU8COVoucher(id),
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