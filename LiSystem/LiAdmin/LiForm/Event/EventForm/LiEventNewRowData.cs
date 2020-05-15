using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Event.EventForm
{
    public class LiEventNewRowData : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                this.liForm.addNewRow(this.focusEntityKey);
                bSuccess = true;
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
