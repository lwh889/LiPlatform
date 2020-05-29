using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LiHttp.GetEntity;
using LiModel.Form;
using LiForm.Dev.Util;
using LiContexts.Model;
using LiCommon.LiPostSharp.LiExceptionAspect;
using LiCommon.Util;
using LiLog;
using LiContexts;

namespace LiForm.Event.EventListForm
{
    public class LiEventListEdit : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                DataRow dr = this.liListForm.getListFocusedDataRow();
                if (dr != null)
                {
                    string entityKey = this.liListForm.getEntityKey();
                    string keyFieldName = this.liListForm.getVoucherKeyFieldName();
                    string primaryFieldName = this.liListForm.getPrimaryFieldName();
                    Dictionary<string, object> dict = this.liListForm.getFormDataDict(dr[primaryFieldName]);

                    LiForm.Dev.LiForm liForm = FormUtil.getVoucher(entityKey, dict) as LiForm.Dev.LiForm;

                    LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(Convert.ToInt32(dict[keyFieldName]), liForm, entityKey), this.liListForm.getParentForm());

                    ListButtonModel listButton = this.Tag as ListButtonModel;
                    liForm.setVoucherStatus(listButton.voucherStatus);

                    bSuccess = true;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Fatal("列表编辑错误：", ex);
                MessageUtil.Show("列表编辑错误：" + ex.Message, "系统提示");
            }

            return bSuccess;
        }
        public override bool sendEvent()
        {
            return eventMediator.relay(this); //请中介者转发
        }
    }
}
