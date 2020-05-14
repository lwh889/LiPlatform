using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiException
{
    public class LiHttpException : ApplicationException
    {
        public LiHttpException(string msg)
            : base(string.Format("LiHttp模块出现异常【{0}】", msg))
        {

        }

        public LiHttpException(string msg, Exception ex)
            : base(string.Format("LiHttp模块出现异常【{0}】", msg), ex)
        {

        }
    }
}
