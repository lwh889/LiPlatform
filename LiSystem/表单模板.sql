
declare @formId int
declare @formName nvarchar(30)
declare @formText nvarchar(30)
declare @systemCode nvarchar(9)
declare @categoryGuid nvarchar(50)

--单表单据
INSERT INTO [dbo].[LiForm] ([name], [text], [height], [width], [dCreateDate], [keyName], [codeFieldName], [keyFieldName], [formType], [statusFieldName], [systemCode]) 
VALUES (@formName, @formText, 100, 800, GETDATE(), NULL, N'billCode', N'id', N'单据', N'billStatus', @systemCode)

set @formId = @@IDENTITY

set @categoryGuid = NEWID()
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'查询', N'btnQuery', N'Large', @categoryGuid, N'U8|U8_query_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'精确查询', N'btnPreciseQuery', N'Large', @categoryGuid, N'U8|U8_Query Budgetinfor_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'刷新', N'btnRefresh', N'Large', @categoryGuid, N'U8|U8_refresh_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'新增', N'btnNew', N'Large', @categoryGuid, N'U8|U8_add_32x32.png', N'NewStatus', GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'编辑', N'btnEdit', N'Large', @categoryGuid, N'U8|U8_Modify_32x32.png', N'EditStatus', GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'删除', N'btnDelete', N'Large', @categoryGuid, N'U8|U8_delete_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'提交', N'btnSubmit', N'Large', @categoryGuid, N'U8|U8_Submit_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'反提交', N'btnUnSubmit', N'Large', @categoryGuid, N'U8|U8_UnSubmit_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'审核', N'btnAudit', N'Large', @categoryGuid, N'U8|U8_Submit_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'反审核', N'btnUnAudit', N'Large', @categoryGuid, N'U8|U8_Unapprove_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'退出', N'btnExit', N'Large', @categoryGuid, N'U8|U8_Exit_32x32.png', NULL, GETDATE(), NULL, NULL)




