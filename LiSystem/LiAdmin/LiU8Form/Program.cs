using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
using LiCommon.Util;
using LiContexts;
using LiContexts.LiEnum;
using LiForm.Dev.Util;
using LiForm.LiStatus;
using LiHttp.Enum;
using LiModel.Basic;
using LiModel.U8;
using LiModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiU8Form
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if(args != null && args.Length > 0)
            {

                LiLog.LogUtil.Info("U8方式登录系统：" + args[0]);
                //U8LoginInfo u8LoginInfo = JsonUtil.GetEntity<U8LoginInfo>("{\"cAcc_Id\":\"004\",\"CurDate\":\"2020-06-05\",\"cUserId\":\"demo\",\"cUserName\":\"demo\",\"cUserPassWord\":\"DEMO\",\"dbServerName\":\"127.0.0.1\",\"SysPassword\":\"111222\",\"cMenuId\":\"LWHU80101\",\"cMenuName\":\"产品线BG对照表1\",\"cAuthId\":\"LWHU80101\"} ");
                U8LoginInfo u8LoginInfo = JsonUtil.GetEntity<U8LoginInfo>(args[0]);

                //ImageUtil.imageFolderPath = "C:\\LiProject\\平台\\LiSystem\\LiAdmin\\Image";
                ImageUtil.loadAllImage();

                LiHttp.Server.LiHttpSetting.URL = string.Format("http://{0}:8002", u8LoginInfo.dbServerName);
                SystemInfoModel systemInfoModel = new SystemInfoModel();
                systemInfoModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.SystemInfo).getEntityList<SystemInfoModel>());

                Dictionary<string, object> whereDict = new Dictionary<string, object>();
                whereDict.Add("userCode", u8LoginInfo.cUserId);
                UserModel userModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.User).getEntitySingle<UserModel>(whereDict);

                LiContexts.LiContext.SystemLoginType = LoginType.U8;
                LiContexts.LiContext.userInfo = userModel;
                LiContexts.LiContext.HostIP = NetUtil.getIPAddress();
                LiContexts.LiContext.HostName = NetUtil.getHostName();
                LiContexts.LiContext.LoginData = u8LoginInfo.CurDate;
                LiContexts.LiContext.SeverIP = u8LoginInfo.dbServerName;
                LiContexts.LiContext.SystemInfo = systemInfoModel.getSingleByDataSource("999") as SystemInfoModel;

                if (string.IsNullOrEmpty(userModel.skinName))
                {
                    UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                }
                else
                {
                    UserLookAndFeel.Default.SetSkinStyle(userModel.skinName);
                }

                Application.EnableVisualStyles();
                if (u8LoginInfo.cMenuId.Substring(u8LoginInfo.cMenuId.Length - 4, 4) == "List")
                {
                    RibbonForm liListForm = FormUtil.getVoucherList(u8LoginInfo.cMenuId.Substring(0, u8LoginInfo.cMenuId.Length - 4), LiContext.SystemCode);
                    liListForm.Text = u8LoginInfo.cMenuName;
                    Application.Run(liListForm);
                }
                else
                {
                    LiForm.Dev.LiForm liForm = FormUtil.getVoucher(u8LoginInfo.cMenuId) as LiForm.Dev.LiForm;
                    liForm.Text = u8LoginInfo.cMenuName;
                    LiStatusReadOnlyDev liStatusReadOnlyDev = liForm.getVoucherNewStatus();

                    liForm.Show();
                    liForm.setVoucherStatus(liStatusReadOnlyDev.statusName);
                    Application.Run(liForm);
                }

            }
            else
            {
                MessageUtil.Show("传入参数为空", "系统提示");
            }
        }
    }
}
