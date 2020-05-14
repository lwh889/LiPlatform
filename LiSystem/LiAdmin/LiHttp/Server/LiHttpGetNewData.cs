using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;

namespace LiHttp.Server
{
    public class LiHttpGetNewData : ALiHttp, ILiHttp
    {
        public LiHttpGetNewData(string entityKey, string systemCode) : base(systemCode, entityKey)
        {
        }
        public GetNewDataParamModel getGetNewDataParamModel()
        {
            GetNewDataParamModel getNewDataParamModel = new GetNewDataParamModel();
            getNewDataParamModel.type = "insert";
            getNewDataParamModel.systemCode = systemCode;
            getNewDataParamModel.entityKey = entityKey;

            return getNewDataParamModel;
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
