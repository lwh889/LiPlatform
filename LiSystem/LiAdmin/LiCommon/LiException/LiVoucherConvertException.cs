using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiException
{
    public class LiVoucherConvertException : ApplicationException
    {
        public LiVoucherConvertException(string msg)
            : base(string.Format("LiVoucherConvert模块出现异常【{0}】", msg))
        {

        }

        public LiVoucherConvertException(string msg, Exception ex)
            : base(string.Format("LiVoucherConvert模块出现异常【{0}】", msg), ex)
        {

        }
    }
}
