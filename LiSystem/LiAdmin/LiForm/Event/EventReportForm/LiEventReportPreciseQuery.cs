using LiControl.Util;
using LiForm.Dev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiForm.Event.EventReportForm
{
    public class LiEventReportPreciseQuery : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {
                LiQueryForm queryForm = new LiQueryForm(liReportForm.entityKey, liReportForm.currentQuerySchemeModel, liReportForm.querySchemeModels);
                if (queryForm.ShowDialog() == DialogResult.Yes)
                {
                    string whereStr = DevFormUtil.getPreciseWhereStr(queryForm.returnQuerys, true);
                    this.liReportForm.setQueryWhere(whereStr);
                    this.liReportForm.currentQuerySchemeModel = queryForm.currentQuerySchemeModel;
                    this.liReportForm.Query();
                    this.liReportForm.FillGridListCtrlQuery(this.liReportForm.setFirstPage());
                    bSuccess = true;
                }
                this.liReportForm.reloadQuerySchemes(queryForm.querySchemeModels);
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
