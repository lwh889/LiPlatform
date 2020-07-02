using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using LiForm.Dev;
using LiHttp.Enum;
using LiContexts;
using LiModel.LiConvert;
using LiForm.Dev.Util;
using LiCommon.LiPostSharp.LiExceptionAspect;

namespace LiForm.Event.EventForm
{
    public class LiEventRef : LiAEvent
    {
        [ExceptionHandle]
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liForm.formCode, "convertDest");
            LiRefTypeForm form = new LiRefTypeForm(list);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;

                bSuccess = ListFormUtil.refVoucher(this.liForm.formCode, liConvertHeadModel, this.liForm.tableModel, this.liForm.tableModelList, this.liForm.ParentForm, this.liForm);
            }

            return bSuccess;
        }
        public override bool sendEvent()
        {
            return eventMediator.relay(this); //请中介者转发
        }
    }
}
