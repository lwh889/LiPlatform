using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert.Model
{
    public class LiReponseModel
    {
        public static LiReponseModel getInstance()
        {
            return new LiReponseModel() { bSuccess = false };
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool bSuccess;

        /// <summary>
        /// 返回单据编码
        /// </summary>
        public string voucherCode;

        /// <summary>
        /// 返回单据ID
        /// </summary>
        public string voucherId;

        /// <summary>
        /// 返回结果内容
        /// </summary>
        public string result;
    }
}
