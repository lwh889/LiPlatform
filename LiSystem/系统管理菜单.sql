
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
select 'LiVoucherDesign','表单设计',1,1,1,0,1,1
union all
select 'LiBasicInfoDesign','基础档案设计',1,1,2,0,2,2
union all
select 'LiU8BasicInfoDesign','U8基础档案设计',1,1,3,0,3,3
union all
select 'LiSystemManage','系统管理',1,1,4,4,4,4
union all
select 'LiU8BasicInfoDesign','转换设计',1,1,5,0,5,5
union all
select 'LiDictDesign','数据字典设计',0,1,4,4,1,1
union all
select 'LiUsers','用户档案',0,1,4,4,2,2
union all
select 'LiRoles','角色档案',0,1,4,4,3,3
union all
select 'LiConvert','单据转换',0,1,4,4,4,4
union all
select 'LiFlow','审批流',0,1,4,4,5,5
union all
select 'LiVersionFlow','审批流版本',0,1,4,4,6,6
union all
select 'LiVoucherFlowManage','审批流管理',0,1,4,4,7,7
union all
select 'LiTableConfigure','表信息配置',0,1,4,4,8,8
union all
select 'LiProcedureConfigure','表信息配置',0,1,4,4,9,9
union all
select 'LiSystemInfoForm','系统信息配置',0,1,4,4,10,10
union all
select 'LiReportDesignForm','报表设计',0,1,4,4,11,11
union all
select 'LiDictDesign','库存档案组',1,0,2,2,1,1
union all
select 'LiDictDesign','物料分组',0,0,2,2021,1,1
union all
select 'LiDictDesign','物料',0,0,2,2021,1,1


