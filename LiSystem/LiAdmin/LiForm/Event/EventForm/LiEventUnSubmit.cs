using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiForm.Dev.Util;
using LiFlow.Enums;
using LiFlow.Util;
using LiFlow.Model;
using LiCommon.Util;
using LiModel.Form;

namespace LiForm.Event.EventForm
{
    public class LiEventUnSubmit : LiAEvent
    {
        public override bool receiveEvent()
        {
            string resultContent;

            /// <summary>
            /// 当前审批节点
            /// </summary>
            LiVersionFlowNodeModel currentFlowNode = null;

            /// <summary>
            /// 最新的已审批节点
            /// </summary>
            LiVoucherFlowStepModel liVoucherFlowStepModel = null;

            /// <summary>
            /// 已运行的单据流程
            /// </summary>
            LiVoucherFlowModel liVoucherFlowTemp = null;

            /// <summary>
            /// 版本流程
            /// </summary>
            LiVersionFlowModel liVersionFlowModel = null;

            bool bSuccess = false;
            ButtonModel buttonModel = this.Tag as ButtonModel;

            try
            {
                currentFlowNode = FlowUtil.getNextStepFlow(this.liForm.formCode, Convert.ToString(this.liForm.voucherId), out liVoucherFlowStepModel, out liVoucherFlowTemp, out liVersionFlowModel);

                if(currentFlowNode != null)
                {
                    if (FlowUtil.revokeFlow(this.liForm.formCode, RevokeType.UnSubmit, this.liForm.formCode, Convert.ToString(this.liForm.voucherId), liVoucherFlowStepModel, out resultContent))
                    {
                        bSuccess = this.liForm.saveVoucher(buttonModel);
                    }
                    MessageUtil.Show(resultContent, "温馨提示");
                }
                else
                {
                    bSuccess = this.liForm.saveVoucher(buttonModel);
                }


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
