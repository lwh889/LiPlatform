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

namespace LiForm.Event.EventListForm
{
    public class LiEventListEdit : LiAEvent
    {
        public override void receiveEvent()
        {
            DataRow dr = this.liListForm.getListFocusedDataRow();
            if (dr != null)
            {
                string entityKey = this.liListForm.getEntityKey();
                string keyFieldName = this.liListForm.getVoucherKeyFieldName();
                string primaryFieldName = this.liListForm.getPrimaryFieldName();
                ListButtonModel listButton = this.Tag as ListButtonModel;

                Dictionary<string, object> dict = LiContexts.LiContext.getHttpEntity(entityKey).getEntityDictionarySingle(dr[primaryFieldName], keyFieldName);

                LiForm.Dev.LiForm liForm = FormUtil.getVoucher(entityKey, dict) as LiForm.Dev.LiForm;

                LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(Convert.ToInt32(dict[keyFieldName]), liForm, entityKey), this.liListForm.getParentForm());

                liForm.setVoucherStatus(listButton.voucherStatus);
            }
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
