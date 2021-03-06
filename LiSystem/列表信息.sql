
create table LiColumn(
	id int identity(1,1) primary key not null,
	formId int not null,	--
	columnFieldName nvarchar(50),	--名称
	columnCaption nvarchar(100),	--标题
	columnWidth int default 100,	--宽度
	columnVisible bit default 1,	--是否显示
	columnIndex bit default 1,	--排序
	entityType nvarchar(100),	--实体名称
	bQuery bit default 0,	--快速查询
	bQueryRange bit default 0,	--区间查询
	columnControlType nvarchar(30),	--控件类型
	basicInfoKey nvarchar(30),	--基础档案类型
	judgeSymbol nvarchar(30),	--判断符号

	dCreateDate datetime default getdate()
)