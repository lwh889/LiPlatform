using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using LiModel.Form;
using LiCommon.Util;
using LiHttp.Enum;
using LiContexts;
using LiControl.Util;
using LiModel.Util;
using LiModel.Users;

namespace LiForm
{
    public partial class LiModifyPwdForm : DevExpress.XtraEditors.XtraForm
    {
        UserModel userModel;

        List<GridlookUpEditModel> fieldTextDatasoucre = new List<GridlookUpEditModel>();

        List<GridlookUpEditModel> fieldDateDatasoucre = new List<GridlookUpEditModel>();

        #region 引用数据源
        private GridlookUpEditModel gridlookUpEditModel = new GridlookUpEditModel();
        private FlowNoRangeModel flowNoRangeModel = new FlowNoRangeModel();
        private DateTimeFormatModel dateTimeFormatModel = new DateTimeFormatModel();
        #endregion

        public LiModifyPwdForm(UserModel userModel)
        {
            InitializeComponent();

            this.userModel = userModel;

            Init();
        }

        public void Init()
        {
            InitData();

            InitControl();
        }

        public void InitData()
        {
        }

        public void InitControl()
        {
        }

        private void loadData()
        {
            txt_UserCode.Text = userModel.userCode;
        }

        private void getDate()
        {

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_PwdOri.Text))
            {
                MessageUtil.Warning("旧密码不能为空", "温馨提示");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_PwdNew.Text))
            {
                MessageUtil.Warning("新密码不能为空", "温馨提示");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_PwdConfirm.Text))
            {
                MessageUtil.Warning("新密码不能为空", "温馨提示");
                return;
            }

            if (!txt_PwdNew.Text.Equals(txt_PwdConfirm.Text))
            {
                MessageUtil.Warning("新密码与确认密码不相等！", "温馨提示");
                return;
            }

            if (!userModel.userPassword.Equals(txt_PwdOri.Text))
            {
                MessageUtil.Warning("旧密码错误！", "温馨提示");
                return;
            }

            userModel.userPassword = txt_PwdNew.Text;
            LiContext.getHttpEntity(LiEntityKey.User).updateEntity(userModel);
            if (LiContext.getHttpEntity(LiEntityKey.User).bSuccess)
            {
                MessageUtil.Show("成功修改！", "温馨提示");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                userModel.userPassword = txt_PwdOri.Text;
                MessageUtil.Show("修改失败！"+ LiContext.getHttpEntity(LiEntityKey.User).resultContent , "温馨提示");
                return;
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void LiVoucherCodeForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}