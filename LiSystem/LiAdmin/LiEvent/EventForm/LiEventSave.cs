using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LiEvent.EventForm
{
    public class LiEventSave : LiAEvent
    {
        public LiEventSave(string id)
            : base(id)
        {
        }
        public override void receiveEvent()
        {
            Console.WriteLine("保存收到请求。");
        }
        public override void sendEvent()
        {
            Console.WriteLine("保存发出请求。");
            eventMediator.relay(this); //请中介者转发
        }
    }
}
