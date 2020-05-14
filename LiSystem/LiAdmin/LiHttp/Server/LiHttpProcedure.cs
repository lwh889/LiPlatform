using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;

namespace LiHttp.Server
{
    public class LiHttpProcedure : ALiHttp, ILiHttp
    {
        public LiHttpProcedure(string entityKey, string systemCode) : base(systemCode, entityKey)
        {
        }

        public ProcedureParamModel getProcedureParamModel(Dictionary<string, object> datas)
        {
            ProcedureParamModel procedureParamModel = new ProcedureParamModel();
            procedureParamModel.systemCode = systemCode;
            procedureParamModel.type = "Procedure";
            procedureParamModel.entityKey = entityKey;
            procedureParamModel.datas = datas;

            return procedureParamModel;
        }

        public ProcedureParamModel getProcedureParamModel(object entitys)
        {
            ProcedureParamModel procedureParamModel = new ProcedureParamModel();
            procedureParamModel.type = "insert";
            procedureParamModel.systemCode = systemCode;
            procedureParamModel.entityKey = entityKey;
            procedureParamModel.datas = JsonUtil.GetDictionaryByModel(entitys);

            return procedureParamModel;
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
