using LiModel.LiEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiReport
{
    public class LiReportEventModel
    {
        public static LiReportEventModel getInstance( int reportButtonId)
        {
            return new LiReportEventModel() { reportButtonId = reportButtonId };
        }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int id { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int reportButtonId { set; get; }

        /// <summary>
        /// 全名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("全名称")]
        public string fullName { set; get; }

        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("程序集")]
        public string assemblyName { set; get; }

        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("备注")]
        public string eventMemo { set; get; }
    }
}
