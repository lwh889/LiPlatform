using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Model
{
    public class LiU8ApiIdModel
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string sOperationType { set; get; }

        /// <summary>
        /// U8模块
        /// </summary>
        public string sSubId { set; get; }

        /// <summary>
        /// 帐套号
        /// </summary>
        public string sAccID { set; get; }

        /// <summary>
        /// 年份
        /// </summary>
        public string sYear { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string sUserID { set; get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string sPassword { set; get; }

        /// <summary>
        /// 登陆日期
        /// </summary>
        public string sDate { set; get; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string sVouchType { set; get; }

        /// <summary>
        /// 弃审是否删除
        /// </summary>
        public bool bDelete { set; get; }

        /// <summary>
        /// 单据ID
        /// </summary>
        public List<object> vouchIds { set; get; }
    }
}
