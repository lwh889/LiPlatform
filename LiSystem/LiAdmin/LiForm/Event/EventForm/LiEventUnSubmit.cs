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

namespace LiForm.Event.EventForm
{
    public class LiEventUnSubmit : LiAEvent
    {
        public override void receiveEvent()
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

            currentFlowNode = FlowUtil.getNextStepFlow(this.liForm.formCode, Convert.ToString(this.liForm.voucherId), out liVoucherFlowStepModel, out liVoucherFlowTemp, out liVersionFlowModel);

            if (FlowUtil.revokeFlow(this.liForm.formCode, RevokeType.UnSubmit, this.liForm.formCode, Convert.ToString(this.liForm.voucherId), liVoucherFlowStepModel, out resultContent))
            {
                this.liForm.saveVoucher();
            }

            MessageUtil.Show(resultContent, "温馨提示");
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
