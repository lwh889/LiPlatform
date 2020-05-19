
declare @formId int
declare @formName nvarchar(30)
declare @formText nvarchar(30)
declare @systemCode nvarchar(9)
declare @categoryGuid nvarchar(50)

--������
INSERT INTO [dbo].[LiForm] ([name], [text], [height], [width], [dCreateDate], [keyName], [codeFieldName], [keyFieldName], [formType], [statusFieldName], [systemCode]) 
VALUES (@formName, @formText, 100, 800, GETDATE(), NULL, N'billCode', N'id', N'����', N'billStatus', @systemCode)

set @formId = @@IDENTITY

set @categoryGuid = NEWID()
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'��ѯ', N'btnQuery', N'Large', @categoryGuid, N'U8|U8_query_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'��ȷ��ѯ', N'btnPreciseQuery', N'Large', @categoryGuid, N'U8|U8_Query Budgetinfor_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'ˢ��', N'btnRefresh', N'Large', @categoryGuid, N'U8|U8_refresh_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'����', N'btnNew', N'Large', @categoryGuid, N'U8|U8_add_32x32.png', N'NewStatus', GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'�༭', N'btnEdit', N'Large', @categoryGuid, N'U8|U8_Modify_32x32.png', N'EditStatus', GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'ɾ��', N'btnDelete', N'Large', @categoryGuid, N'U8|U8_delete_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'�ύ', N'btnSubmit', N'Large', @categoryGuid, N'U8|U8_Submit_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'���ύ', N'btnUnSubmit', N'Large', @categoryGuid, N'U8|U8_UnSubmit_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'���', N'btnAudit', N'Large', @categoryGuid, N'U8|U8_Submit_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'�����', N'btnUnAudit', N'Large', @categoryGuid, N'U8|U8_Unapprove_32x32.png', NULL, GETDATE(), NULL, NULL)
INSERT INTO [dbo].[LiListButton] ([formId], [caption], [name], [iconsize], [categoryGuid], [icon], [voucherStatus], [dCreateDate], [userFieldName], [dateFieldName]) 
VALUES (@formId, N'�˳�', N'btnExit', N'Large', @categoryGuid, N'U8|U8_Exit_32x32.png', NULL, GETDATE(), NULL, NULL)




