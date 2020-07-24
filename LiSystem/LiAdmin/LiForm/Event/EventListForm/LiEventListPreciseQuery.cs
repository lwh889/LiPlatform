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
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                LiQueryForm queryForm = new LiQueryForm(liListForm.entityKey, liListForm.currentQuerySchemeModel, liListForm.querySchemeModels);
                if (queryForm.ShowDialog() == DialogResult.Yes)
                {
                    string whereStr = DevFormUtil.getPreciseWhereStr(queryForm.returnQuerys, true);
                    this.liListForm.setQueryWhere(whereStr);
                    this.liListForm.currentQuerySchemeModel = queryForm.currentQuerySchemeModel;
                    this.liListForm.Query();
                    this.liListForm.FillGridListCtrlQuery(this.liListForm.setFirstPage());
                    bSuccess = true;
                }
                this.liListForm.setQuerySchemes(queryForm.querySchemeModels);
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
