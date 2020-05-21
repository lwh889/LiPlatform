using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiForm.Dev.Util;
using LiModel.Form;

namespace LiForm.Event.EventForm
{
    public class LiEventUnAudit : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;
            ButtonModel buttonModel = this.Tag as ButtonModel;

            try
            {
                bSuccess = this.liForm.saveVoucher(buttonModel);
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
