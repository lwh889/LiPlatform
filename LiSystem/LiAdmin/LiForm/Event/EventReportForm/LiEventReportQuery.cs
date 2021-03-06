﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Event.EventReportForm
{
    public class LiEventReportQuery : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                this.liReportForm.getQuickQueryWhere();
                this.liReportForm.Query();
                this.liReportForm.FillGridListCtrlQuery(this.liReportForm.setFirstPage());
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
