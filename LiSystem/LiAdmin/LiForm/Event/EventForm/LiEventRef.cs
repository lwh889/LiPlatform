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

namespace LiForm.Event.EventForm
{
    public class LiEventRef : LiAEvent
    {
        public override void receiveEvent()
        {
            List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liForm.formCode, "convertDest");
            LiRefTypeForm form = new LiRefTypeForm(list);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;
                LiRefForm liRefForm = new LiRefForm(liConvertHeadModel);
                if (liRefForm.ShowDialog() == DialogResult.Yes)
                {
                    List<DataRow> drs = liRefForm.SelectDataRows;
                    DataRow drHead = drs[0];
                    FormUtil.pushVoucher(liConvertHeadModel,drHead, drs, this.liForm);
                    this.liForm.loadData();
                }
            }
        }
        public override void sendEvent()
        {
            eventMediator.relay(this); //请中介者转发
        }
    }
}
