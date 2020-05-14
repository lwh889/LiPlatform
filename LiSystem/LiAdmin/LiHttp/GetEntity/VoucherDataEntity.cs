using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiHttp.Server;
using LiHttp.RequestParam;
using LiCommon.Util;

namespace LiHttp.GetEntity
{
    public class VoucherDataEntity : AHttpEntity
    {
        public VoucherDataEntity(string entityKey, string systemCode) : base(entityKey, systemCode)
        {
        }
        public Dictionary<string, object> getEntityDictionarySingle1(object keyValue, string keyFieldName, string entityKey)
        {
            this.entityKey = entityKey;

            return getEntityDictionarySingle(keyValue, keyFieldName);
        }

    }
}
