
drop table LiSystemInfo
create table LiSystemInfo(
	ID int identity(1,1) primary key,
	systemCode nvarchar(10) ,--ϵͳ����
	systemDataBaseName nvarchar(20) ,--ϵͳ���ݿ�
	systemName nvarchar(30) ,--ϵͳ����
	systemTitle nvarchar(50)  ,--ϵͳ����
	companyName nvarchar(50)  ,
	companyLogo nvarchar(max)  ,
	bDefault bit,
	modifyDate datetime,	--�޸�ʱ��
	createDate datetime default getdate()
	)

	--alter table LiSystemInfo add systemTitle nvarchar(20)
	--alter table LiSystemInfo add systemCode nvarchar(10) 
	--alter table LiSystemInfo add systemDataBaseName nvarchar(20)
	--alter table LiSystemInfo add systemName nvarchar(30)