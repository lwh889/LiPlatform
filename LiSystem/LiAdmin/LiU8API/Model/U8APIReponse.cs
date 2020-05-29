using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class U8APIReponse
    {
        public bool bSuccess;
        public string voucherID;
        public string voucherCode;
        public string resultContent;
        public Exception apiEx;
    }
}
