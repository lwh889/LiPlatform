using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert.Model
{
    /// <summary>
    /// 转换类型表头
    /// </summary>
    public class LiConvertHeadModel
    {
        public static LiConvertHeadModel getInstance()
        {

            return new LiConvertHeadModel() { datas = new List<LiConvertBodyModel>(), queryFields = new List<LiConvertFieldModel>() };
        }

        /// <summary>
        /// ID
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 转换编码
        /// </summary>
        public string convertCode { set; get; }

        /// <summary>
        /// 转换名称
        /// </summary>
        public string convertName { set; get; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public string convertDestType { set; get; }

        /// <summary>
        /// 目标
        /// </summary>
        public string convertDest { set; get; }

        /// <summary>
        /// 源类型
        /// </summary>
        public string convertSourceType { set; get; }

        /// <summary>
        /// 源
        /// </summary>
        public string convertSource { set; get; }

        /// <summary>
        /// 转换关系
        /// </summary>
        public string convertRelation { set; get; }

        /// <summary>
        /// 转换关系字段
        /// </summary>
        public string convertRelationField { set; get; }


        /// <summary>
        /// 列表按钮
        /// </summary>
        public List<LiConvertBodyModel> datas;

        /// <summary>
        /// 查询内容
        /// </summary>
        public List<LiConvertFieldModel> queryFields;
    }
}
