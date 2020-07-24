using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiReport
{
    public class LiReportFieldModel
    {
        public static LiReportFieldModel getInstance()
        {
            return new LiReportFieldModel() { bColumnDisplay = true, bColumnGroup = false, bQuery = true, displayFormat = "", iColumnWidth = 100, iDisplayFormatType = 0 };
        }
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        public int reportId { set; get; }

        /// <summary>
        /// 字段名
        /// </summary>
        public string columnName { set; get; }

        /// <summary>
        /// 列名称
        /// </summary>
        public string columnCaption { set; get; }

        /// <summary>
        /// 列类型
        /// </summary>
        public string columnType { set; get; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int iColumnWidth { set; get; }

        /// <summary>
        /// 是否显示列
        /// </summary>
        public bool bColumnDisplay { set; get; }

        /// <summary>
        /// 列索引
        /// </summary>
        public int iColumnIndex { set; get; }

        /// <summary>
        /// 是否查询
        /// </summary>
        public bool bQuery { set; get; }

        /// <summary>
        /// 是否排序
        /// </summary>
        public bool bOrderBy { set; get; }

        /// <summary>
        /// 排序升降
        /// </summary>
        public string orderBy { set; get; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int iOrderByIndex { set; get; }

        /// <summary>
        /// 显示格式，时间，数字，自定义？
        /// </summary>
        public int iDisplayFormatType { set; get; }

        /// <summary>
        /// 显示格式
        /// </summary>
        public string displayFormat { set; get; }

        /// <summary>
        /// 是否汇总
        /// </summary>
        public bool bColumnGroup { set; get; }

        /// <summary>
        /// 汇总格式
        /// </summary>
        public string columnGroupFormat { set; get; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public string controlType { set; get; }

        /// <summary>
        /// 引用档案Key
        /// </summary>
        public string basicInfoKey { set; get; }

        /// <summary>
        /// 引用档案显示名称
        /// </summary>
        public string basicInfoShowFieldName { set; get; }

        /// <summary>
        /// 引用档案显示模式
        /// </summary>
        public string basicInfoShowMode { set; get; }

        /// <summary>
        /// 引用档案主键
        /// </summary>
        public string basicInfoTableKey { set; get; }
        
        /// <summary>
        /// 字典档案Key
        /// </summary>
        public string dictInfoType { set; get; }

        /// <summary>
        /// 引用档案配置信息
        /// </summary>
        public string gridlookUpEditShowModelJson { set; get; }
    }
}
