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
                    LiRefForm liRefForm = new LiRefForm(liConvertHeadModel);

                    if (liRefForm.ShowDialog() == DialogResult.Yes)
                    {
                        List<DataRow> drs = liRefForm.SelectDataRows;
                        DataRow drHead = drs[0];
                        //获取新窗体
                        LiForm.Dev.LiForm liForm = FormUtil.getVoucher(this.liListForm.entityKey) as LiForm.Dev.LiForm;

                        FormUtil.pushVoucher(liConvertHeadModel, drHead, drs, liForm);

                        LiContext.AddPageMdi(PageFormModel.getInstance(0, liForm, this.liListForm.entityKey), this.liListForm.ParentForm);
                        liForm.setVoucherNewStatus();
                        bSuccess = true;
                    }
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
