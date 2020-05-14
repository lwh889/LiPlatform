using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Event.EventListForm
{
    public class LiEventListRefresh : LiAEvent
    {
        public override void receiveEvent()
        {
            this.liListForm.Query();
            this.liListForm.FillGridListCtrlQuery(this.liListForm.setFirstPage());
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
