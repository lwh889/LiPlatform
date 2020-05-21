using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiForm.Dev.Util;
using LiFlow;
using LiFlow.Enums;
using LiFlow.Util;
using LiCommon.Util;
using LiFlow.Model;
using LiModel.Form;

namespace LiForm.Event.EventForm
{
    public class LiEventAudit : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;
            ButtonModel buttonModel = this.Tag as ButtonModel;

            LiVoucherFlowModel liVoucherFlowModel = FlowUtil.getCurrentFlow(this.liForm.formCode, Convert.ToString(this.liForm.voucherId));
            if (liVoucherFlowModel != null)
            {
                LiFlowApprovalForm liFlowApprovalForm = new LiFlowApprovalForm(this.liForm.formCode, Convert.ToString(this.liForm.voucherId), Convert.ToString(this.liForm.voucherCode), this.liForm.mainTableName, this.liForm.formDataDict, this.liForm.formModel);

                if (liFlowApprovalForm.ShowDialog() == DialogResult.Yes)
                {
                    switch(liFlowApprovalForm.approvalType){
                        case ApprovalType.Agree:
                            if(liFlowApprovalForm.bSuccess)
                            {
                                bSuccess = this.liForm.saveVoucher(buttonModel);
                            }
                            else{
                                MessageUtil.Show(liFlowApprovalForm.resultContent,"温馨提示");
                            }
                            break;
                        case ApprovalType.Disagree:
                            if (liFlowApprovalForm.bSuccess)
                            {
                                bSuccess = this.liForm.saveVoucher(buttonModel);
                            }
                            else
                            {
                                MessageUtil.Show(liFlowApprovalForm.resultContent, "温馨提示");
                            }
                            break;
                    }

                }
            }
            else
            {
                bSuccess = this.liForm.saveVoucher(buttonModel);
            }

            return bSuccess;
        }
        public override bool sendEvent()
        {
            return eventMediator.relay(this); //请中介者转发
        }
    }
}
