using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using LiForm.Dev;
using LiHttp.GetEntity;
using LiModel.LiConvert;
using LiForm.Dev.Util;
using LiHttp.Enum;
using LiContexts;
using LiContexts.Model;
using LiVoucherConvert.Service.Impl;
using LiU8API.Model;
using LiVoucherConvert.Model;
using LiCommon.Util;
using LiVoucherConvert;
using LiCommon.LiPostSharp.LiExceptionAspect;

namespace LiForm.Event.EventListForm
{
    public class LiEventListPush : LiAEvent
    {
        [ExceptionHandle]
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liListForm.entityKey, "convertSource");
            LiRefTypeForm form = new LiRefTypeForm(list);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;

                List<DataRow> drs = this.liListForm.getSelectedDataRows();
                bSuccess = ListFormUtil.pushVoucher(this.liListForm.entityKey, liConvertHeadModel, this.liListForm.tableModel, this.liListForm.tableModelList, drs[0], drs, this.liListForm.getParentForm());

            }
            return bSuccess;

        }

        public override bool sendEvent()
        {
            return eventMediator.relay(this); //请中介者转发
        }
    }
}
