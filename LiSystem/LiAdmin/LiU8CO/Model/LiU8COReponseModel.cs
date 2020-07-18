using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Model
{
    public class LiU8COReponseModel
    {
        public static LiU8COReponseModel getInstance()
        {
            return new LiU8COReponseModel() { bSuccess = false, resultContent = string.Empty};
        }
        public bool bSuccess;
        public bool bAuditSuccess;
        public bool bDeleteSuccess;
        public string vouchID;
        public string vouchCode;
        public string resultContent;
        /// <summary>
        /// 单据数据
        /// </summary>
        public List<Dictionary<string, object>> vouchDatas { set; get; }
        public int vouchDataRowCount { set; get; }
        public Exception apiEx;
    }
}
