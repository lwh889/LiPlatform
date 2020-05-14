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

namespace LiForm.Event.EventListForm
{
    public class LiEventListPush : LiAEvent
    {
        public override void receiveEvent()
        {

            List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liListForm.entityKey, "convertSource");
            LiRefTypeForm form = new LiRefTypeForm(list);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;
                List<DataRow> drs = this.liListForm.getSelectedDataRows();
                DataRow drHead = drs[0];
                //获取新窗体
                LiForm.Dev.LiForm liFormDest = FormUtil.getVoucher(liConvertHeadModel.convertDest) as LiForm.Dev.LiForm;

                FormUtil.pushVoucher(liConvertHeadModel, drHead, drs, liFormDest);

                LiContext.AddPageMdi(PageFormModel.getInstance(0, liFormDest, liConvertHeadModel.convertDest), this.liListForm.getParentForm());
                liFormDest.setVoucherNewStatus();
            }
        }

        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
