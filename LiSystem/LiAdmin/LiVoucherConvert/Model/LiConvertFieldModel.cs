using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiVoucherConvert.Model
{
    public class LiConvertFieldModel
    {

        public int id { get; set; }

        /// <summary>
        /// 转换关系
        /// </summary>
        public int fid { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityCode { set; get; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityName { set; get; }


        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName { set; get; }

        /// <summary>
        /// 列名名称
        /// </summary>
        public string columnFieldName { set; get; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int iColumnWidth { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool bColumnDisplay { set; get; }

        /// <summary>
        /// 快速查询
        /// </summary>
        public bool bQuery { set; get; }

        /// <summary>
        /// 区间查询
        /// </summary>
        public bool bRange { set; get; }

        /// <summary>
        /// 数值类型
        /// </summary>
        public string sColumnControlType { set; get; }

        /// <summary>
        /// 引用类型
        /// </summary>
        public string sRefTypeCode { set; get; }

        /// <summary>
        /// 判断符号
        /// </summary>
        public string sJudgeSymbol { set; get; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string dictInfoType { set; get; }

        /// <summary>
        /// 基础档案类型
        /// </summary>
        public string basicInfoKey { set; get; }

        /// <summary>
        /// 控件格式
        /// </summary>
        public string gridlookUpEditShowModelJson { set; get; }

    }
}
