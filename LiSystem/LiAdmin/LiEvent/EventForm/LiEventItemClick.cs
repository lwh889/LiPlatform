using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;

namespace LiEvent.EventForm
{
    public class LiEventItemClick : LiAEvent
    {
        public LiEventItemClick(string id):base(id)
        {
        }

        public override void receiveEvent()
        {
            Console.WriteLine("具体同事类1收到请求。");
        }
        public override void sendEvent()
        {
            Console.WriteLine("具体同事类1发出请求。");
            eventMediator.relay(this); //请中介者转发
        }
    }
}
