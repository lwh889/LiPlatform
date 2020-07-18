
drop table LiManageMeum
create table LiManageMeum(
	ID int identity(1,1) primary key,
	Code nvarchar(30) not null,
	Name nvarchar(30) not null,
	systemCode nvarchar(20) not null,
	isGroup bit not null,
	isSystem bit default 0,
	GroupId int not null,
	ParentID int not null,
	imageIndex int not null,
	iOrder int not null,
	dCreateDate datetime default getdate()
)

--alter table LiManageMeum add systemCode nvarchar(20)

insert into LiManageMeum (code, name, isGroup,isSystem, groupId, parentId, imageIndex, iOrder)
select 'LiVoucherDesign','�����',1,1,1,0,1,1
union all
select 'LiBasicInfoDesign','�����������',1,1,2,0,2,2
union all
select 'LiU8BasicInfoDesign','U8�����������',1,1,3,0,3,3
union all
select 'LiSystemManage','ϵͳ����',1,1,4,4,4,4
union all
select 'LiU8BasicInfoDesign','ת�����',1,1,5,0,5,5
union all
select 'LiDictDesign','�����ֵ����',0,1,4,4,1,1
union all
select 'LiUsers','�û�����',0,1,4,4,2,2
union all
select 'LiRoles','��ɫ����',0,1,4,4,3,3
union all
select 'LiConvert','����ת��',0,1,4,4,4,4
union all
select 'LiFlow','������',0,1,4,4,5,5
union all
select 'LiVersionFlow','�������汾',0,1,4,4,6,6
union all
select 'LiVoucherFlowManage','����������',0,1,4,4,7,7
union all
select 'LiTableConfigure','����Ϣ����',0,1,4,4,8,8
union all
select 'LiProcedureConfigure','����Ϣ����',0,1,4,4,9,9
union all
select 'LiSystemInfoForm','ϵͳ��Ϣ����',0,1,4,4,10,10
union all
select 'LiReportDesignForm','�������',0,1,4,4,11,11
union all
select 'LiDictDesign','��浵����',1,0,2,2,1,1
union all
select 'LiDictDesign','���Ϸ���',0,0,2,2021,1,1
union all
select 'LiDictDesign','����',0,0,2,2021,1,1


