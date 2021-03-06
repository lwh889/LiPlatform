﻿using System;
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
using LiCommon.Util;

namespace LiForm.Event.EventForm
{
    public class LiEventPush : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;

            try
            {

                List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liForm.formCode, "convertSource");
                LiRefTypeForm form = new LiRefTypeForm(list);
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;


                    //获取表头
                    List<Dictionary<string, object>> formDataDictSourceList = new List<Dictionary<string, object>>();
                    formDataDictSourceList.Add(this.liForm.formDataDict);
                    DataTable dt = DataUtil.DictionaryToTable(formDataDictSourceList);


                    //获取表体
                    List<LiConvertBodyModel> convertBodySourceList = liConvertHeadModel.datas.Where(m => m.convertSCollection != null).ToList();
                    string collectionNameSource = "";
                    var groupList = convertBodySourceList.GroupBy(m => m.convertSCollection);
                    foreach (var group in groupList)
                    {
                        collectionNameSource = group.Key;
                    }
                    DataTable dtSource = this.liForm.getEntityData(collectionNameSource);
                    List<DataRow> drs = new List<DataRow>();
                    drs.AddRange(dtSource.Select("1=1"));


                    bSuccess = ListFormUtil.pushVoucher(this.liForm.formCode, liConvertHeadModel, this.liForm.tableModel, this.liForm.tableModelList, dt.Rows[0], drs, this.liListForm.getParentForm());
                }


                //List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>(this.liForm.formCode, "convertSource");
                //LiRefTypeForm form = new LiRefTypeForm(list);
                //if (form.ShowDialog() == DialogResult.Yes)
                //{
                //    LiConvertHeadModel liConvertHeadModel = form.liConvertHeadModel;
                //    //获取新窗体
                //    LiForm.Dev.LiForm liFormDest = FormUtil.getVoucher(liConvertHeadModel.convertDest) as LiForm.Dev.LiForm;

                //    List<Dictionary<string, object>> formDataDictSourceList = new List<Dictionary<string, object>>();
                //    formDataDictSourceList.Add(this.liForm.formDataDict);
                //    DataTable dt = DataUtil.DictionaryToTable(formDataDictSourceList);

                //    //获取表体
                //    List<LiConvertBodyModel> convertBodySourceList = liConvertHeadModel.datas.Where(m => m.convertSCollection != null).ToList();
                //    string collectionNameSource = "";
                //    var groupList = convertBodySourceList.GroupBy(m => m.convertSCollection);
                //    foreach (var group in groupList)
                //    {
                //        collectionNameSource = group.Key;
                //    }
                //    DataTable dtSource = this.liForm.getEntityData(collectionNameSource);
                //    List<DataRow> drs = new List<DataRow>();
                //    drs.AddRange(dtSource.Select("1=1"));

                //    FormUtil.pushVoucher(liConvertHeadModel, dt.Rows[0], drs, liFormDest, false);

                //    LiContext.AddPageMdi(PageFormModel.getInstance(0, liFormDest, liConvertHeadModel.convertDest), this.liForm.ParentForm);
                //    liFormDest.setVoucherNewStatus();

                //    bSuccess = true;
                //}
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
