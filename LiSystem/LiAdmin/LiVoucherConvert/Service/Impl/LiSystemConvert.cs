using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.LiConvert;
using LiVoucherConvert.Model;

namespace LiVoucherConvert.Service.Impl
{
    public class LiSystemConvert : AVoucherConvert
    {
        public Dictionary<string, object> formDataDict { set; get; }

        public override LiReponseModel pushVoucher()
        {
            LiReponseModel liReponse = LiReponseModel.getInstance();
            try
            {

                DataRow drHead = convertData[0];

                //获取转换关系
                List<LiConvertBodyModel> convertHeadList = liConvertHead.datas.Where(m => m.convertDestType == liConvertHead.convertDestHeadName).ToList();
                List<LiConvertBodyModel> convertBodyList = liConvertHead.datas.Where(m => m.convertDestType == liConvertHead.convertDestBodyName).ToList();

                //获取集合数据源
                var groupList = convertBodyList.GroupBy(m => m.convertDCollection);
                string collectionName = "";
                foreach (var group in groupList)
                {
                    collectionName = group.Key;
                }
                //转换表头
                foreach (LiConvertBodyModel convertHead in convertHeadList)
                {
                    if (convertHead.bDefault)
                    {
                        formDataDict[convertHead.convertDestField] = convertHead.defaultValue;
                        continue;
                    }
                    if (string.IsNullOrEmpty(convertHead.convertSourceField)) continue;

                    string fieldName = isPrefix ? string.Format("Li{0}_{1}", convertHead.convertSourceType, convertHead.convertSourceField) : convertHead.convertSourceField;
                    formDataDict[convertHead.convertDestField] = drHead[fieldName];
                }

                //转换表体
                if (!string.IsNullOrEmpty(collectionName))
                {
                    List<Dictionary<string, object>> dtDest = formDataDict[collectionName] as List<Dictionary<string, object>>;
                    dtDest.Clear();
                    foreach (DataRow dr in convertData)
                    {
                        Dictionary<string, object> drDest = new Dictionary<string, object>();

                        foreach (LiConvertBodyModel convertBody in convertBodyList)
                        {
                            if (convertBody.bDefault)
                            {
                                if (drDest.ContainsKey(convertBody.convertDestField))
                                {
                                    drDest[convertBody.convertDestField] = convertBody.defaultValue;
                                }
                                else
                                {
                                    drDest.Add(convertBody.convertDestField, convertBody.defaultValue);
                                }
                                continue;
                            }

                            if (string.IsNullOrEmpty(convertBody.convertSourceField)) continue;

                            string fieldName = isPrefix ? string.Format("Li{0}_{1}", convertBody.convertSourceType, convertBody.convertSourceField) : convertBody.convertSourceField;
                            if (drDest.ContainsKey(convertBody.convertDestField))
                            {
                                drDest[convertBody.convertDestField] = dr[fieldName];
                            }
                            else
                            {
                                drDest.Add(convertBody.convertDestField, dr[fieldName]);
                            }
                        }

                        dtDest.Add(drDest);

                    }
                    liReponse.bSuccess = true;
                }
            }
            catch(Exception ex)
            {
                liReponse.bSuccess = false;
                liReponse.result = ex.Message;
                liReponse.ex = ex;
            }

            return liReponse;
        }
    }
}
