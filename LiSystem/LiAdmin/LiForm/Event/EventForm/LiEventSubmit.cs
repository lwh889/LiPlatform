using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiForm.Dev.Util;
using LiFlow.Util;
using LiCommon.Util;

namespace LiForm.Event.EventForm
{
    public class LiEventSubmit : LiAEvent
    {
        public override void receiveEvent()
        {
            string resultContent = "";

            if (FlowUtil.startFlow(this.liForm.formCode, Convert.ToString(this.liForm.voucherId), Convert.ToString(this.liForm.voucherCode), this.liForm.formDataDict, out resultContent))
            {
                this.liForm.saveVoucher();
            }

            MessageUtil.Show(resultContent, "温馨提示");
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
