using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.Form;

namespace LiForm.Event.EventListForm
{
    public class LiEventListQuery : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                this.liListForm.getQuickQueryWhere();
                this.liListForm.Query();
                this.liListForm.FillGridListCtrlQuery(this.liListForm.setFirstPage());
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
