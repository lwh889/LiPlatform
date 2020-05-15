using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;

namespace LiForm.Event.EventForm
{
    public class LiEventItemClick : LiAEvent
    {
        public LiEventItemClick(string id):base(id)
        {
        }

        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
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
