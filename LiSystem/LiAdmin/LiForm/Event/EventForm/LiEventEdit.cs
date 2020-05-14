using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.Form;
using System.Data;
using LiCommon.Util;
using LiModel.LiEnum;

namespace LiForm.Event.EventForm
{
    public class LiEventEdit : LiAEvent
    {
        public override void receiveEvent()
        {
            ButtonModel listButton = this.Tag as ButtonModel;
            if (liForm.getVoucherType() == VoucherType.TreeBasicInfo)
            {
                DataRow dr = liForm.getDataRowInTreeList(liForm.formCode);
                Dictionary<string, object> dict = DataUtil.DataRowToDictionary(dr);
                liForm.formDataDict = dict;
                liForm.loadData();
                liForm.setShowDockPanel("dockPanel_Edit", true);
            }

        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
