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
        public override bool receiveEvent()
        {
            bool bSuccess = false;
            ButtonModel buttonModel = this.Tag as ButtonModel;

            try
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
