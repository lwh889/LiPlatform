using LiHttp.Enum;
using LiModel.LiConvert;
using LiU8API;
using LiU8API.LiEnum;
using LiU8API.Model;
using LiVoucherConvert.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert.Service.Impl
{
    public class U8VoucherConvert : AVoucherConvert
    {
        /// <summary>
        /// 转换数据是否有前缀
        /// </summary>
        public LiU8VoucherModel _liU8Voucher;
        public LiU8VoucherModel liU8Voucher { set { _liU8Voucher = value; } get { return _liU8Voucher; } }

        public override LiReponseModel pushVoucher()
        {

            if (liConvertHead == null) throw new Exception("转换规则为空！");
            if (convertData == null) throw new Exception("转换数据为空！");

            LiReponseModel liReponse = LiReponseModel.getInstance();

            //LiU8VoucherModel liU8VoucherModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntitySingle<LiU8VoucherModel>(liConvertHead.convertDest, "code");

            U8Voucher u8Voucher = new U8Voucher(OperationType.NEW, liU8Voucher);
            u8Voucher.initDomHead();
            u8Voucher.initDomBody(convertData.Count);

            //表头转换
            List< LiConvertBodyModel> convertHeads = liConvertHead.datas.Where(m => m.convertDestType == liConvertHead.convertDestHeadName).ToList();
            foreach(LiConvertBodyModel convertBodyModel in convertHeads)
            {
                if (convertBodyModel.bDefault == false && string.IsNullOrEmpty(convertBodyModel.convertSourceField))
                    continue;

                if (convertBodyModel.bDefault)
                {
                    u8Voucher.setDomHeadValue(convertBodyModel.convertDestField, convertBodyModel.defaultValue);
                }
                else if (!string.IsNullOrEmpty(convertBodyModel.convertSourceField))
                {
                    string fieldName = isPrefix ? string.Format("Li{0}_{1}", convertBodyModel.convertSourceType, convertBodyModel.convertSourceField) : convertBodyModel.convertSourceField;
                    u8Voucher.setDomHeadValue(convertBodyModel.convertDestField, convertData[0][fieldName]);
                }
            }

            //表体转换
            int iRow = 0;
            List<LiConvertBodyModel> convertBodys = liConvertHead.datas.Where(m => m.convertDestType == liConvertHead.convertDestHeadName).ToList();
            foreach(DataRow dr in convertData)
            {
                foreach (LiConvertBodyModel convertBodyModel in convertBodys)
                {
                    if (convertBodyModel.bDefault == false && string.IsNullOrEmpty(convertBodyModel.convertSourceField))
                        continue;

                    if (convertBodyModel.bDefault)
                    {
                        u8Voucher.setDomBodyValue(iRow, convertBodyModel.convertDestField, convertBodyModel.defaultValue);
                    }
                    else if (!string.IsNullOrEmpty(convertBodyModel.convertSourceField))
                    {
                        string fieldName = isPrefix ? string.Format("Li{0}_{1}", convertBodyModel.convertSourceType, convertBodyModel.convertSourceField) : convertBodyModel.convertSourceField;
                        u8Voucher.setDomBodyValue(iRow, convertBodyModel.convertDestField, convertData[0][fieldName]);
                    }
                }
                ++iRow;
            }


            U8APIReponse u8APIReponse = u8Voucher.commit();
            liReponse.bSuccess = u8APIReponse.bSuccess;
            liReponse.result = u8APIReponse.resultContent;
            liReponse.voucherId = u8APIReponse.voucherID;
            liReponse.voucherCode = u8APIReponse.voucherCode;

            return liReponse;
        }
    }
}
