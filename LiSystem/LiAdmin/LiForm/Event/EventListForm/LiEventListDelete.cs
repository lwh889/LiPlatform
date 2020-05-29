using LiCommon.Util;
using LiContexts;
using LiLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiForm.Event.EventListForm
{
    public class LiEventListDelete : LiAEvent
    {
        public override bool receiveEvent()
        {
            bool bSuccess = false;


            if (MessageUtil.ShowMsgBox("是否删除所选择的数据？", "温馨提示", MsgType.YesNo) == DialogResult.Yes)
            {
                try
                {
                    string entityKey = this.liListForm.getEntityKey();
                    string keyFieldName = this.liListForm.getVoucherKeyFieldName();
                    string primaryFieldName = this.liListForm.getPrimaryFieldName();
                    DataRow[] drs = this.liListForm.getListSelectedDataRow();
                    if (drs != null && drs.Length > 0)
                    {
                        List<object> existIds = new List<object>();

                        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                        foreach (DataRow dr in drs)
                        {
                            if (!existIds.Contains(dr[primaryFieldName]))
                            {
                                Dictionary<string, object> dict = LiContexts.LiContext.getHttpEntity(entityKey, LiContext.SystemCode).getEntityDictionarySingle(dr[primaryFieldName], keyFieldName);
                                if (dict != null)
                                {
                                    existIds.Add(dr[primaryFieldName]);
                                    dataList.Add(dict);
                                }
                            }
                        }

                        if (dataList.Count > 0)
                        {
                            bSuccess = LiContexts.LiContext.getHttpEntity(entityKey, LiContext.SystemCode).batchDeleteEntity(dataList);

                            this.liListForm.RefreshData();
                        }
                    }


                }
                catch (Exception ex)
                {
                    LogUtil.Fatal("列表删除错误：", ex);
                    MessageUtil.Show("列表删除错误：" + ex.Message, "系统提示");
                }
            }


            return bSuccess;
        }
        public override bool sendEvent()
        {
            return eventMediator.relay(this); //请中介者转发
        }
    }
}
