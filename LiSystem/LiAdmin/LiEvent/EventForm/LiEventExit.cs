using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiEvent.EventForm
{
    public class LiEventExit : LiAEvent
    {
        public LiEventExit(string id)
            : base(id)
        {
        }
        public override void receiveEvent()
        {
            Console.WriteLine("退出收到请求。");
        }
        public override void sendEvent()
        {
            Console.WriteLine("退出发出请求。");
            eventMediator.relay(this); //请中介者转发
        }
    }
}
