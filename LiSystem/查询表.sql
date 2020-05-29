drop table LiField
drop table LiEntity
drop table LiQuery
drop table LiQueryScheme

create table LiQueryScheme(
	id int identity(1,1) primary key,
	entityKey nvarchar(30) not null,
	userCode nvarchar(20) not null,
	querySchemeName nvarchar(30) not null,
	createDate datetime default getdate()
)

--alter table LiQueryScheme add  entityKey nvarchar(30)  null

create table LiQuery(
	id int identity(1,1) primary key,
	querySchemeId int REFERENCES LiQueryScheme(id),
	sBracketsBefore nvarchar(10) ,
	sFieldName nvarchar(50) ,
	sJudgmentSymbol nvarchar(10) ,
	oQueryValue nvarchar(4000) ,
	sJoin nvarchar(10) ,
	sBracketsAfter nvarchar(10) ,
	sFieldType nvarchar(30) ,
	sBasicCode nvarchar(30) ,

	createDate datetime default getdate()
)


create table LiEntity(
	id int identity(1,1) primary key,
	querySchemeId int REFERENCES LiQueryScheme(id),
	sEntityType nvarchar(30) ,
	sEntityCode nvarchar(30) ,
	sEntityName nvarchar(50) ,
	sTableName nvarchar(50) ,
	iShow bit ,
	
	createDate datetime default getdate()
)

--alter table LiEntity add sTableName nvarchar(50)
create table LiField(
	id int identity(1,1) primary key,
	querySchemeId int REFERENCES LiQueryScheme(id),
	convertId int REFERENCES LiConvertHead(id),
	code nvarchar(30) ,
	sEntityCode nvarchar(30) ,
	[name] nvarchar(50) ,
	fieldName nvarchar(50) ,
	columnFieldName nvarchar(100) ,
	
	sEntityName nvarchar(50) ,
	iColumnWidth int ,
	bColumnDisplay bit ,
	bQuery bit ,
	bRange bit ,
	sColumnControlType nvarchar(50) ,
	sRefTypeCode nvarchar(50) ,
	sJudgeSymbol nvarchar(10) ,
	dictInfoType nvarchar(50),
	basicInfoKey nvarchar(50),
	gridlookUpEditShowModelJson nvarchar(2000),
	
	createDate datetime default getdate()
)

--alter table LiField add dictInfoType nvarchar(50)
--alter table LiField add basicInfoKey nvarchar(50)
--alter table LiField add gridlookUpEditShowModelJson nvarchar(2000)
--alter table LiField add columnFieldName nvarchar(100)
--alter table LiField add convertId int REFERENCES LiConvertHead(id)