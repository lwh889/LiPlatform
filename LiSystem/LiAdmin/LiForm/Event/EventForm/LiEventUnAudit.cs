using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiForm.Dev.Util;

namespace LiForm.Event.EventForm
{
    public class LiEventUnAudit : LiAEvent
    {
        public override void receiveEvent()
        {
            this.liForm.saveVoucher();
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
