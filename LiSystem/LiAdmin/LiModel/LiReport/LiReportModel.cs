using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiReport
{
    public class LiReportModel
    {
        public static LiReportModel getInstance()
        {
            return new LiReportModel() { datas = new List<LiReportFieldModel>(), buttons = new List<LiReportButtonModel>() };
        }
        /// <summary>
        /// ID
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 报表编码
        /// </summary>
        public string reportKey { set; get; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string reportName { set; get; }
        /// <summary>
        /// 系统帐套号
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 系统菜单编码
        /// </summary>
        public int menuCode { set; get; }
        /// <summary>
        /// SQL
        /// </summary>
        public string reportSql { set; get; }
        /// <summary>
        /// SQL总数
        /// </summary>
        public string reportCountSql { set; get; }

        /// <summary>
        /// 列表按钮
        /// </summary>
        public List<LiReportFieldModel> datas { set; get; }

        /// <summary>
        /// 列表按钮
        /// </summary>
        public List<LiReportButtonModel> buttons { set; get; }
    }
}
