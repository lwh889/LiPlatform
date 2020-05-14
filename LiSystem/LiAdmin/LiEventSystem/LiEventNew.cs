using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiForm.Event;

namespace LiEventSystem
{
    public class LiEventNew : LiAEvent
    {

        public override void receiveEvent()
        {
            Console.WriteLine("自定义收到请求。");
        }
        public override void sendEvent()
        {
            Console.WriteLine("自定义发出请求。");
            eventMediator.relay(this); //请中介者转发
        }
    }
}
