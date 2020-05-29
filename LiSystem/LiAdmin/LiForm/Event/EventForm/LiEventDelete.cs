using LiCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiForm.Event.EventForm
{
    public class LiEventDelete : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;
            string resultContent = "";
            try
            {
                if (MessageUtil.ShowMsgBox("是否删除？", "温馨提示", MsgType.YesNo) == DialogResult.Yes)
                {
                    bSuccess = this.liForm.deleteVoucher(out resultContent) ;
                    if (!bSuccess)
                    {
                        MessageUtil.Show("删除失败！" + resultContent, "温馨提示");
                        return bSuccess;
                    }
                    else
                    {
                        if (!this.liForm.getJumpTurnVoucher("Previous", this.liForm.voucherId))
                        {
                            this.liForm.Close();
                        }
                    }
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
