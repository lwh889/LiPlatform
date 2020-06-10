VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "ClsIntface"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Implements UFPortalProxyInterface.ILoginable
Public Property Get Business() As Object
  Set Business = gBus
End Property
Public Property Set Business(ByRef oBus As Object)
   Set gBus = oBus  '获得business对象。
End Property

Private Function ILoginable_CallFunction(ByVal cMenuId As String, ByVal cMenuName As String, ByVal cAuthId As String, ByVal cCmdLine As String) As Object
   '创建新的连接，用于查询、数据更新，不使用调用者的连接对象，但该连接对象可能会传回给调用者。
   'If gConn Is Nothing Then
   '  Set gConn = New ADODB.Connection
   '  gConn.CursorLocation = adUseClient
   '  gConn.Open gLogin.UfDbName
   'End If
   
   Dim oJson As Object
   
   Dim sTransJson As String
   sTransJson = "{ cAcc_Id: '', CurDate: '',cUserId: '',cUserName: '',cUserPassWord: '',dbServerName: '',SysPassword: '',cMenuId: '',cMenuName: '',cAuthId: ''}"
   
   Set oJson = JSON.parse(sTransJson)
   oJson.Item("cAcc_Id") = gLogin.cAcc_Id
   oJson.Item("CurDate") = Format(gLogin.CurDate, "yyyy-MM-dd")
   oJson.Item("cUserId") = gLogin.cUserId
   oJson.Item("cUserName") = gLogin.cUserName
   oJson.Item("cUserPassWord") = gLogin.cUserPassWord
   oJson.Item("dbServerName") = gLogin.dbServerName
   oJson.Item("SysPassword") = gLogin.SysPassword
   oJson.Item("cMenuId") = cMenuId
   oJson.Item("cMenuName") = cMenuName
   oJson.Item("cAuthId") = cAuthId
   
   'MsgBox JSON.toString(oJson)
   sTransJson = Replace(JSON.toString(oJson), """", "\""")
   sTransJson = Replace(sTransJson, " ", "^^")
   
   Shell ("C:\LiProject\平台\LiSystem\LiAdmin\LiU8Form\bin\Debug\LiU8Form.exe " & sTransJson)
End Function
Private Function ILoginable_Login() As Boolean
   If ((Not gBus Is Nothing) And gbLogin = False) Then
       Set gLogin = gBus.GetVBclsLogin() '获得login对象
       gbLogin = True
       ILoginable_Login = True
   Else
      ILoginable_Login = Not gBus Is Nothing
   End If
End Function
Private Function ILoginable_LogOff() As Boolean
   ILoginable_LogOff = True '本行代码必须。
   gbLogin = False '本行代码必须。
End Function

