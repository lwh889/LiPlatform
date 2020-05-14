using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Interface;

namespace LiModel.Form
{
    /// <summary>
    /// 状态
    /// </summary>
    public class StatusModel : GridlookUpEditModel
    {
        public int id { set; get; }
        public int fid { set; get; }

        public bool bShow { set; get; }
        public bool bNew { set; get; }

        /// <summary>
        /// 用户控件
        /// </summary>
        public string userFieldName { set; get; }

        /// <summary>
        /// 日期控件
        /// </summary>
        public string dateFieldName { set; get; }

        /// <summary>
        /// 状态控件
        /// </summary>
        public string statusFieldName { set; get; }

        /// <summary>
        /// 控件值
        /// </summary>
        public List<ControlStatusModel> dataControlStatuss { set; get; }


    }
}
