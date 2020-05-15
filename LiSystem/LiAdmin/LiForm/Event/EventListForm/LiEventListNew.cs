using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiContexts;
using LiContexts.Model;
using LiForm.Dev.Util;
using LiModel.Form;

namespace LiForm.Event.EventListForm
{
    public class LiEventListNew : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                ListButtonModel listButton = this.Tag as ListButtonModel;

                LiForm.Dev.LiForm liForm = FormUtil.getVoucher(this.liListForm.entityKey) as LiForm.Dev.LiForm;

                LiContext.AddPageMdi(PageFormModel.getInstance(0, liForm, this.liListForm.entityKey), this.liListForm.ParentForm);
                liForm.setVoucherStatus(listButton.voucherStatus);
                bSuccess = true;
            }
            catch (Exception ex)
            {
            }

            return bSuccess;
        }
        public override bool sendEvent()
        {
            return eventMediator.relay(this); //请中介者转发
        }
    }
}
