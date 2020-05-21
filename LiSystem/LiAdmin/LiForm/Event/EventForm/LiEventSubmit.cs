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
using LiModel.Form;

namespace LiForm.Event.EventForm
{
    public class LiEventSubmit : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;
            ButtonModel buttonModel = this.Tag as ButtonModel;

            try
            {
                string resultContent = "";

                if (FlowUtil.startFlow(this.liForm.formCode, Convert.ToString(this.liForm.voucherId), Convert.ToString(this.liForm.voucherCode), this.liForm.formDataDict, out resultContent))
                {
                    bSuccess = this.liForm.saveVoucher(buttonModel);
                }

                MessageUtil.Show(resultContent, "温馨提示");
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
