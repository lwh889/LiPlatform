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
using LiContexts;
using LiContexts.Model;
using LiVoucherConvert.Model;
using LiCommon.Util;
using LiVoucherConvert.Service.Impl;
using LiVoucherConvert;
using LiU8API.Model;

namespace LiForm.Event.EventListForm
{
    public class LiEventListRef : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {

                List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liListForm.entityKey, "convertDest");
                LiRefTypeForm form = new LiRefTypeForm(list);
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;
                    bSuccess = ListFormUtil.refVoucher(this.liListForm.entityKey, liConvertHeadModel, this.liListForm.tableModel, this.liListForm.tableModelList, this.liListForm.getParentForm(),null);

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
