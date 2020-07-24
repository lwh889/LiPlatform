﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Event.EventReportForm
{
    public class LiEventReportExit : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                this.liReportForm.Close();
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
