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
        public override void receiveEvent()
        {
            this.liListForm.getQuickQueryWhere();
            this.liListForm.Query();
            this.liListForm.FillGridListCtrlQuery(this.liListForm.setFirstPage());
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
