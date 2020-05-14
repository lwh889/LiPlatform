using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;

namespace LiHttp.Server
{
    public class LiHttpInsert : ALiHttp,ILiHttp
    {
        public LiHttpInsert(string entityKey, string systemCode):base(systemCode, entityKey)
        {
        }
        public InsertParamModel getInsertParamModel(Dictionary<string, object> datas)
        {
            InsertParamModel insertParamModel = new InsertParamModel();
            insertParamModel.type = "insert";
            insertParamModel.systemCode = systemCode;
            insertParamModel.entityKey = entityKey;
            insertParamModel.datas = datas;

            return insertParamModel;
        }


        public InsertParamModel getInsertParamModel(object entitys)
        {
            InsertParamModel insertParamModel = new InsertParamModel();
            insertParamModel.type = "insert";
            insertParamModel.systemCode = systemCode;
            insertParamModel.entityKey = entityKey;
            insertParamModel.datas = JsonUtil.GetDictionaryByModel(entitys);

            return insertParamModel;
        }

        public InsertBatchParamModel getInsertBatchParamModel<TEntity>( List<TEntity> entitys)
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            foreach (TEntity entity in entitys)
            {
                dataList.Add(JsonUtil.GetDictionaryByModel(entity));
            }

            InsertBatchParamModel insertBatchParamModel = new InsertBatchParamModel();
            insertBatchParamModel.type = "insert";
            insertBatchParamModel.systemCode = systemCode;
            insertBatchParamModel.entityKey = entityKey;
            insertBatchParamModel.datas = dataList;

            return insertBatchParamModel;
        }

        public override bool httpGet(LiHttpSetting liHttpSetting, out string retultContent)
        {
            return base.httpGet(liHttpSetting, out retultContent);
        }

        public override bool httpPost(LiHttpSetting liHttpSetting, IParamModel paramModel, out string retultContent)
        {
            return base.httpPost(liHttpSetting, paramModel, out retultContent);
        }
    }
}
