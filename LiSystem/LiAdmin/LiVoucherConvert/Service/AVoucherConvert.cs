using LiVoucherConvert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert.Service
{
    public abstract class AVoucherConvert
    {
        public abstract LiReponseModel pushVoucher();
    }
}
