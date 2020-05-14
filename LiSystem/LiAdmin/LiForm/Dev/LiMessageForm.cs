using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

using LiHttp.GetEntity;
using LiModel.Basic;
using LiCommon.Util;
using LiModel.LiEnum;
using LiForm.Dev.Util;
using LiContexts.Model;
using LiModel.Form;
using LiHttp.Enum;
using LiContexts;

namespace LiForm.Dev
{
    public partial class LiMessageForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public LiMessageForm()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
        }

        private void InitData()
        {

        }

        private void InitControl()
        {

        }

        private void Query()
        {
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).getEntityList<MessageModel>(LiContexts.LiContext.userInfo.userCode, "userCode");
            gridView1.BestFitColumns();
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Query();
        }

        private void btnRead_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageModel messageModel = gridView1.GetFocusedRow() as MessageModel;
            messageModel.messageRead = true;
            LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).updateEntity(messageModel);

            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).bSuccess)
            {
                MessageUtil.Show("已阅", "温馨提示");
            }
            else
            {
                MessageUtil.Show("已阅失败", "温馨提示");
            }
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            openMessage();
        }

        private void openMessage()
        {
            MessageModel messageModel = gridView1.GetFocusedRow() as MessageModel;

            switch (messageModel.messageType)
            {
                case MessageType.Flow:
                    FormModel formModel = LiContext.getFormModel(messageModel.entityKey, LiContext.SystemCode);
                    //FormModel formModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getEntitySingle<FormModel>(messageModel.entityKey,"entityKey");
                    Dictionary<string, object> dict = LiContexts.LiContext.getHttpEntity(messageModel.entityKey, LiContext.SystemCode).getEntityDictionarySingle(messageModel.voucherId, formModel.keyFieldName);

                    LiForm liForm = FormUtil.getVoucher(messageModel.entityKey, dict) as LiForm;
                    LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(Convert.ToInt32(messageModel.voucherId),  liForm, messageModel.entityKey), this.ParentForm);
                    liForm.setVoucherStatus();

                    messageModel.messageRead = true;
                    LiContexts.LiContext.getHttpEntity(LiEntityKey.Message, LiContext.SystemCode).updateEntity(messageModel);
                    break;
            }
        }

        private void LiMessageForm_Load(object sender, EventArgs e)
        {
            Query();
        }
    }
}