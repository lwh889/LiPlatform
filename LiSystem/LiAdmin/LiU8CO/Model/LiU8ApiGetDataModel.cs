using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Model
{
    public class LiU8ApiGetDataModel
    {
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
        public string sCardNumber { set; get; }

        /// <summary>
        /// 查询类型
        /// </summary>
        public int bQueryType { set; get; }
        /// <summary>
        /// 查询起始
        /// </summary>
        public int iStart { set; get; }

        /// <summary>
        /// 查询结束
        /// </summary>
        public int iEnd { set; get; }

        /// <summary>
        /// 显示字段名
        /// </summary>
        public string sSelectFields { set; get; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string sWhereString { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public string sOrderByString { set; get; }
    }
}
