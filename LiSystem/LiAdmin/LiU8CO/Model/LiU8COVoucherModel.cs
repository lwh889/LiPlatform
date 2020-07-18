using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Model
{
    public class LiU8COVoucherModel
    {

        public int id { set; get; }

        /// <summary>
        /// 单据编码
        /// </summary>
        public string code { set; get; }

        /// <summary>
        /// 单据名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string voucherType { set; get; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string headKeyFieldName { set; get; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string bodyKeyFieldName { set; get; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string cardNumber { set; get; }

        /// <summary>
        /// 单据分类
        /// </summary>
        public string voucherClassify { set; get; }

        /// <summary>
        /// 表头DOMSQL,只用于库存单据
        /// </summary>
        public string domHeadSql { set; get; }
        /// <summary>
        /// 时间SQL
        /// </summary>
        public string timeStampSql { set; get; }

        /// <summary>
        /// 表体DOMSQL,可能暂时不用
        /// </summary>
        public string domBodySql { set; get; }

        public List<LiU8COFieldModel> liU8COFields { set; get; }
    }
}
