﻿using System;
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

namespace LiForm.Event.EventListForm
{
    public class LiEventListPush : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {

                List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liListForm.entityKey, "convertSource");
                LiRefTypeForm form = new LiRefTypeForm(list);
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;
                    List<DataRow> drs = this.liListForm.getSelectedDataRows();

                    switch (liConvertHeadModel.convertDestType)
                    {
                        case ConvertDestTypeModel.System:
                            break;
                        case ConvertDestTypeModel.U8:
                            U8VoucherConvert u8VoucherConvert = LiContext.getVoucherConvert(liConvertHeadModel.convertDestType, liConvertHeadModel.convertDest) as U8VoucherConvert;
                            u8VoucherConvert.liU8Voucher = LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntitySingle<LiU8VoucherModel>(liConvertHeadModel.convertDest, "code");
                            u8VoucherConvert.isPrefix = true;
                            u8VoucherConvert.convertData = drs;
                            u8VoucherConvert.liConvertHead = liConvertHeadModel;
                            LiReponseModel liReponse = LiContext.pushVoucher(liConvertHeadModel.convertDestType, liConvertHeadModel.convertDest);
                            break;
                    }
                    //DataRow drHead = drs[0];
                    ////获取新窗体
                    //LiForm.Dev.LiForm liFormDest = FormUtil.getVoucher(liConvertHeadModel.convertDest) as LiForm.Dev.LiForm;

                    //FormUtil.pushVoucher(liConvertHeadModel, drHead, drs, liFormDest);

                    //LiContext.AddPageMdi(PageFormModel.getInstance(0, liFormDest, liConvertHeadModel.convertDest), this.liListForm.getParentForm());
                    //liFormDest.setVoucherNewStatus();
                    //bSuccess = true;
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
