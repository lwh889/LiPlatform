
/*------------------------------------------------------------------二次开发主菜单节点---------------------------------------*/
IF NOT EXISTS(SELECT * FROM UFSystem.dbo.UA_Menu WHERE cMenu_Id = 'LWHU8')
	INSERT INTO UFSystem.dbo.UA_Menu (cMenu_Id, cMenu_Name, cMenu_Eng, cSub_Id, IGrade, cSupMenu_Id, bEndGrade, cAuth_Id, iOrder, iImgIndex, Paramters, Depends, Flag)
	VALUES ('LWHU8', '快速开发系统', Null, null , 0, '#1', 0, NULL, -9999, 0, null, null, null)
GO

IF NOT EXISTS(SELECT * FROM UFSystem.dbo.uA_auth WHERE cAuth_Id = 'LWHU8')
	INSERT INTO UFSystem.dbo.uA_auth(cAuth_Id,cAuth_Name,cSub_Id,iGrade,cSupAuth_Id,bEndGrade,iOrder,cAcc_Id,cAuthType,cAllSupAuths)
	VALUES('LWHU8','二次开发','DP',2,'LWHU8',0,9999,NULL,NULL,NULL)
GO


/*---------------------------------------------------------------------------基础档案----------------------------------------------------------*/

IF NOT EXISTS(SELECT * FROM UFSystem.dbo.UA_Menu WHERE cMenu_Id = 'LWHU801')
	INSERT INTO UFSystem.dbo.UA_Menu (cMenu_Id, cMenu_Name, cMenu_Eng, cSub_Id, IGrade, cSupMenu_Id, bEndGrade, cAuth_Id, iOrder, iImgIndex, Paramters, Depends, Flag)
	VALUES ('LWHU801', '销售组', Null, null , 0, 'LWHU8', 0, NULL, 1000, 1,null	, null, null)
ELSE
UPDATE UFSystem.dbo.UA_Menu SET cAuth_Id=NULL  WHERE cMenu_Id = 'LWHU801'
GO

IF NOT EXISTS(SELECT * FROM UFSystem.dbo.uA_auth WHERE cAuth_Id = 'LWHU801')
	INSERT INTO UFSystem.dbo.uA_auth(cAuth_Id,cAuth_Name,cSub_Id,iGrade,cSupAuth_Id,bEndGrade,iOrder,cAcc_Id,cAuthType,cAllSupAuths)
	VALUES('LWHU801','销售组','DP',3,'LWHU8',0,1000,NULL,NULL,NULL)
GO


-------------------------------------------------------------产品线BG对照表
IF NOT EXISTS(SELECT * FROM UFSystem..ua_idt WHERE id='LWHU80101')
BEGIN
	INSERT INTO UFSystem..ua_idt(id,[assembly],catalogtype,type) 
	VALUES('LWHU80101','LiSystemPlugin3.ClsIntface',0,0)
END
GO


--delete FROM UFSystem.dbo.ua_idt WHERE id = 'LWHU80101'
--select * FROM UFSystem.dbo.UA_Menu WHERE CMenu_Id = 'LWHU80101'
IF NOT EXISTS(SELECT * FROM UFSystem.dbo.UA_Menu WHERE CMenu_Id = 'LWHU80101')
INSERT INTO UFSystem.dbo.UA_Menu (CMenu_Id, cMenu_Name, cMenu_Eng, cSub_Id, IGrade, cSupMenu_Id, bEndGrade, cAuth_Id, iOrder, iImgIndex, Paramters, Depends, Flag)
				VALUES ('LWHU80101', '产品线BG对照表1', NULL,NULL , 2, 'LWHU801', 1, 'LWHU80101', 1200, 4, null, null, null)
GO


--delete UFSystem.dbo.uA_auth WHERE cAuth_Id = 'CATO0201'
IF NOT EXISTS(SELECT * FROM UFSystem.dbo.uA_auth WHERE cAuth_Id = 'LWHU80101')
INSERT INTO UFSystem.dbo.uA_auth (cAuth_Id, cAuth_Name, cSub_Id, iGrade, cSupAuth_Id, bEndGrade, iOrder, cAcc_Id, cAuthType)
				  VALUES ('LWHU80101', '产品线BG对照表','DP', 4,  'LWHU801',0,1200,NULL,null)
   update UFSystem.dbo.uA_auth set cAllSupauths= 'LWHU801' WHERE cAuth_Id = 'LWHU80101'
GO



