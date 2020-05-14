using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiException
{
    public class GetException : ApplicationException
    {
        public GetException(string msg)
            : base(msg)
        {

        }

        public GetException(string msg, Exception ex)
            : base(msg,ex)
        {

        }
    }
}
