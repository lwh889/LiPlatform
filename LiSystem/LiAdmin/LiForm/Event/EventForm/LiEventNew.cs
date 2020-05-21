using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiCommon.Util;
using LiModel.Form;
using LiModel.LiEnum;

using LiHttp.GetEntity;

namespace LiForm.Event.EventForm
{
    public class LiEventNew : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;
            ButtonModel buttonModel = this.Tag as ButtonModel;

            try
            {
                liForm.getNewDate();
                liForm.loadData();

                switch (liForm.getVoucherType())
                {
                    case VoucherType.Voucher:
                        ButtonModel listButton = this.Tag as ButtonModel;
                        break;
                    case VoucherType.TreeBasicInfo:
                        liForm.setShowDockPanel("dockPanel_Edit", true);

                        break;
                    case VoucherType.BasicInfo:
                        break;
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
