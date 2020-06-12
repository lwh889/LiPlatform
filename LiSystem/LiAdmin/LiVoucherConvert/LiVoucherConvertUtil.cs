using LiModel.LiConvert;
using LiVoucherConvert.Model;
using LiVoucherConvert.Service;
using LiVoucherConvert.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert
{
    public class LiVoucherConvertUtil
    {
        /// <summary>
        /// 单据转换
        /// </summary>
        public static VoucherConvertContext voucherConvertContext = new VoucherConvertContext();

        /// <summary>
        /// 获取单据转换
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static AVoucherConvert getVoucherConvert(string convertType, string convertCode)
        {
            string keyName = string.Format("{0}_{1}", convertType, convertCode);
            AVoucherConvert voucherConvert = voucherConvertContext.get(keyName);
            if (voucherConvert == null)
            {
                switch (convertType)
                {
                    case ConvertDestTypeModel.System:
                        voucherConvert = new LiSystemConvert();
                        voucherConvertContext.put(keyName, voucherConvert);
                        break;
                    case ConvertDestTypeModel.U8:
                        voucherConvert = new U8VoucherConvert();
                        voucherConvertContext.put(keyName, voucherConvert);
                        break;
                }
            }

            return voucherConvert;
        }

        /// <summary>
        /// 下推单据
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static LiReponseModel pushVoucher(string convertType, string convertCode)
        {
            AVoucherConvert voucherConvert = getVoucherConvert(convertType, convertCode);
            if (voucherConvert == null) return LiReponseModel.getInstance();
            return voucherConvert.pushVoucher();
        }
    }
}
