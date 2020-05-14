using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;

namespace LiHttp.Server
{
    public class LiHttpUpdate : ALiHttp, ILiHttp
    {
        public LiHttpUpdate(string entityKey, string systemCode) : base(systemCode, entityKey)
        {
        }
        public UpdateParamModel getUpdateParamModel( Dictionary<string, object> datas)
        {

            UpdateParamModel updateParamModel = new UpdateParamModel();
            updateParamModel.type = "insert";
            updateParamModel.systemCode = systemCode;
            updateParamModel.entityKey = entityKey;
            updateParamModel.datas = datas;

            return updateParamModel;
        }


        public UpdateParamModel getUpdateParamModel(object entitys)
        {
            UpdateParamModel updateParamModel = new UpdateParamModel();
            updateParamModel.type = "insert";
            updateParamModel.systemCode = systemCode;
            updateParamModel.entityKey = entityKey;
            updateParamModel.datas = JsonUtil.GetDictionaryByModel(entitys);

            return updateParamModel;
        }

        public UpdateBatchParamModel getUpdateBatchParamModel<TEntity>( List<TEntity> entitys)
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
            foreach (TEntity entity in entitys)
            {
                dataList.Add(JsonUtil.GetDictionaryByModel(entity));
            }

            UpdateBatchParamModel updateBatchParamModel = new UpdateBatchParamModel();
            updateBatchParamModel.type = "insert";
            updateBatchParamModel.systemCode = systemCode;
            updateBatchParamModel.entityKey = entityKey;
            updateBatchParamModel.datas = dataList;

            return updateBatchParamModel;
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
