using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LiModel.Users;
using LiCommon.Util;
using LiCommon.LiPostSharp.LiExceptionAspect;
using System.Linq;
using DevExpress.LookAndFeel;
using LiModel.Basic;
using LiHttp.Enum;
using LiContexts;
using LiControl.Util;
using LiModel.LiEnum;

namespace LiForm
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class LiLoginForm : DevExpress.XtraEditors.XtraForm
    {
        #region 引用数据源
        private SystemInfoModel systemInfoModel = new SystemInfoModel();
        #endregion
        public LiLoginForm()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
        }

        private void InitControl()
        {
            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.VALUE, systemInfoModel.getValueMember(), systemInfoModel.getDisplayMember(), systemInfoModel.getSearchColumns(), systemInfoModel.getDisplayColumns(), systemInfoModel.getDictModelDesc(), gridLookUpEdit_systemInfo, this, systemInfoModel.getDataSource<List<SystemInfoModel>>());
        }

        private void InitData()
        {
            loadData();
        }

        private void loadData()
        {
            systemInfoModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.SystemInfo).getEntityList<SystemInfoModel>());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_MouseMove(object sender, MouseEventArgs e)
        {
            btnExit.BackColor = Color.Red;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.Transparent;
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMinimized_MouseMove(object sender, MouseEventArgs e)
        {
            btnMinimized.ForeColor = Color.White;
            btnMinimized.BackColor = Color.FromArgb(30, 0, 0, 0);
        }

        private void btnMinimized_MouseLeave(object sender, EventArgs e)
        {
            btnMinimized.ForeColor = Color.DimGray;
            btnMinimized.BackColor = Color.Transparent;
        }

        [ExceptionHandle]
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserCode.Text))
            {
                MessageUtil.Show("用户名不能为空！", "温馨提示");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageUtil.Show("密码不能为空！", "温馨提示");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSeverIP.Text))
            {
                MessageUtil.Show("服务器地址不能为空！", "温馨提示");
                return;
            }

            if (gridLookUpEdit_systemInfo.EditValue == null)
            {
                MessageUtil.Show("帐套号不能为空！", "温馨提示");
                return;
            }

            Dictionary<string, object> whereDict = new Dictionary<string, object>();
            whereDict.Add("userCode", txtUserCode.Text);
            List<UserModel> userModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.User).getEntityList<UserModel>(whereDict);
            if (userModels.Count<=0)
            {
                MessageUtil.Show(string.Format("用户【{0}】不存在！", txtUserCode.Text), "温馨提示");
                return;
            }

            whereDict.Clear();
            whereDict.Add("userCode", txtUserCode.Text);
            whereDict.Add("userPassword", EncryptUtil.Base64Encrypt( txtPassword.Text));
            UserModel userModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.User).getEntitySingle<UserModel>(whereDict);

            if (userModel == null)
            {
                MessageUtil.Show("密码错误！", "温馨提示");
                return;
            }

            LiContexts.LiContext.userInfo = userModel;
            LiContexts.LiContext.HostIP = NetUtil.getIPAddress();
            LiContexts.LiContext.HostName = NetUtil.getHostName();
            LiContexts.LiContext.LoginData = dateLogin.DateTime;
            LiContexts.LiContext.SeverIP = txtSeverIP.Text;
            LiLog.LogUtil.Info("登录系统");
            LiContexts.LiContext.SystemInfo = systemInfoModel.getSingleByDataSource(Convert.ToString(gridLookUpEdit_systemInfo.EditValue)) as SystemInfoModel;
            

            if (string.IsNullOrEmpty(userModel.skinName))
            {
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            }
            else
            {
                UserLookAndFeel.Default.SetSkinStyle(userModel.skinName);
            }
            this.DialogResult = DialogResult.Yes;

        }

        private void LiLoginForm_Load(object sender, EventArgs e)
        {
            this.dateLogin.EditValue = DateTime.Now;
        }

        private void TxtSeverIP_EditValueChanged(object sender, EventArgs e)
        {
            LiContexts.LiContext.SeverIP = txtSeverIP.Text;
            loadData();
        }
    }


}