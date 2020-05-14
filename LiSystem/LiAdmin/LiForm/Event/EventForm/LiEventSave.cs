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
    public class LiEventSave : LiAEvent
    {
        public override void receiveEvent()
        {
            this.liForm.saveVoucher();
        }
        public override void sendEvent()
        {
            Console.WriteLine("保存发出请求。");
            eventMediator.relay(this); //请中介者转发
        }
    }
}
