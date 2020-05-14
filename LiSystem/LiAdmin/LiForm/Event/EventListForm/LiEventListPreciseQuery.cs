using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiForm.Dev;
using System.Windows.Forms;
using LiModel.Form;
using LiControl.Util;

namespace LiForm.Event.EventListForm
{
    public class LiEventListPreciseQuery : LiAEvent
    {
        public override void receiveEvent()
        {
            LiQueryForm queryForm = new LiQueryForm(this.liListForm.entityKey);
            if (queryForm.ShowDialog() == DialogResult.Yes)
            {
                string whereStr = DevFormUtil.getPreciseWhereStr(queryForm.returnQuerys, true);
                this.liListForm.setQueryWhere(whereStr);

                this.liListForm.Query();
                this.liListForm.FillGridListCtrlQuery(this.liListForm.setFirstPage());
            }
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
