using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;

namespace LiHttp.Server
{
    public class LiHttpDelete : ALiHttp, ILiHttp
    {
        public LiHttpDelete(string entityKey, string systemCode) : base(systemCode, entityKey)
        {
        }
        public DeleteParamModel getDeleteParamModel(Dictionary<string, object> datas)
        {
            DeleteParamModel deleteParamModel = new DeleteParamModel();
            deleteParamModel.type = "delete";
            deleteParamModel.systemCode = systemCode;
            deleteParamModel.entityKey = entityKey;
            deleteParamModel.datas = datas;

            return deleteParamModel;
        }

        public DeleteBatchParamModel getDeleteBatchParamModel(List<Dictionary<string, object>> dataList)
        {

            DeleteBatchParamModel deleteBatchParamModel = new DeleteBatchParamModel();
            deleteBatchParamModel.type = "delete";
            deleteBatchParamModel.systemCode = systemCode;
            deleteBatchParamModel.entityKey = entityKey;
            deleteBatchParamModel.datas = dataList;

            return deleteBatchParamModel;
        }



        public DeleteParamModel getDeleteParamModel( object entitys)
        {
            DeleteParamModel deleteParamModel = new DeleteParamModel();
            deleteParamModel.type = "delete";
            deleteParamModel.systemCode = systemCode;
            deleteParamModel.entityKey = entityKey;
            deleteParamModel.datas = JsonUtil.GetDictionaryByModel(entitys);

            return deleteParamModel;
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
