using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Event.EventForm
{
    public class LiEventNewRowData : LiAEvent
    {
        public override void receiveEvent()
        {
            this.liForm.addNewRow(this.focusEntityKey);
        }

        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
