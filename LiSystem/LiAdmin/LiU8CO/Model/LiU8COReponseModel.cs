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
            return new LiU8COReponseModel() { bSuccess = false, resultContent = string.Empty };
        }
        public bool bSuccess;
        public string voucherID;
        public string voucherCode;
        public string resultContent;
        public Exception apiEx;
    }
}
