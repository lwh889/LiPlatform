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
        public override void receiveEvent()
        {
            ListButtonModel listButton = this.Tag as ListButtonModel;

            LiForm.Dev.LiForm liForm = FormUtil.getVoucher(this.liListForm.entityKey) as LiForm.Dev.LiForm;

            LiContext.AddPageMdi(PageFormModel.getInstance(0, liForm, this.liListForm.entityKey), this.liListForm.ParentForm);
            liForm.setVoucherStatus(listButton.voucherStatus);
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
