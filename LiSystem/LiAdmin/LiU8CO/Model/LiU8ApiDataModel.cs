using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Model
{
    public class LiU8ApiDataModel
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
        /// 新增是否审核
        /// </summary>
        public bool bAudit { set; get; }

        /// <summary>
        /// 外ID字段名
        /// </summary>
        public string sOutHeadIdFieldName { set; get; }

        /// <summary>
        /// 外ID字段名
        /// </summary>
        public string sOutBodyIdFieldName { set; get; }
        /// <summary>
        /// 单据数据
        /// </summary>
        public List<Dictionary<string, object>> vouchDatas { set; get; }
    }
}
