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
            return new LiU8COReponseModel() { bSuccess = false, resultContent = string.Empty, outBodyVouchID = new List<Dictionary<string, string>>() };
        }
        public bool bSuccess;
        public string vouchID;
        public string vouchCode;
        public string resultContent;
        public string outHeadVouchID;
        public List<Dictionary<string,string>> outBodyVouchID;
        public Exception apiEx;
    }
}
