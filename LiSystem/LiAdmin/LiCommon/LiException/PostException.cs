using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiException
{
    public class PostException : ApplicationException
    {
        public PostException(string msg)
            : base(msg)
        {

        }
        public PostException(string msg, Exception ex)
            : base(msg,ex)
        {

        }
    }
}
