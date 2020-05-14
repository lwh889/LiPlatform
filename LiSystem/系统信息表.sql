
drop table LiSystemInfo
create table LiSystemInfo(
	ID int identity(1,1) primary key,
	systemCode nvarchar(10) ,--系统代码
	systemDataBaseName nvarchar(20) ,--系统数据库
	systemName nvarchar(30) ,--系统名称
	systemTitle nvarchar(50)  ,--系统标题
	companyName nvarchar(50)  ,
	companyLogo nvarchar(max)  ,
	bDefault bit,
	modifyDate datetime,	--修改时间
	createDate datetime default getdate()
	)

	--alter table LiSystemInfo add systemTitle nvarchar(20)
	--alter table LiSystemInfo add systemCode nvarchar(10) 
	--alter table LiSystemInfo add systemDataBaseName nvarchar(20)
	--alter table LiSystemInfo add systemName nvarchar(30)