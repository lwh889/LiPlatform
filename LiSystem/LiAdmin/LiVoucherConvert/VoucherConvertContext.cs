using LiVoucherConvert.Model;
using LiVoucherConvert.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert
{
    public class VoucherConvertContext
    {
        Dictionary<string, AVoucherConvert> voucherConverts = new Dictionary<string, AVoucherConvert>();

        public void put(string key, AVoucherConvert aVoucherConvert)
        {
            if (!voucherConverts.ContainsKey(key))
            {
                voucherConverts.Add(key, aVoucherConvert);
            }
        }

        public AVoucherConvert get(string key)
        {
            if (voucherConverts.ContainsKey(key))
            {
                return voucherConverts[key];
            }

            return null;
        }

        public LiReponseModel pushVoucher(string key)
        {
            if (voucherConverts.ContainsKey(key))
            {
                return voucherConverts[key].pushVoucher();
            }

            return null;
        }
    }
}
