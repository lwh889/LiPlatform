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
        /// 单据数据
        /// </summary>
        public List<Dictionary<string, object>> vouchDatas { set; get; }

        /// <summary>
        /// 采购模块参数，红蓝标识：True,蓝字。生成采购订单时，默认为True
        /// </summary>
        public bool bPositive { set; get; }

        /// <summary>
        /// 采购模块参数，0到货，1退货，2拒收。生成采购订单时，默认为空
        /// </summary>
        public string sBillType { set; get; }

        /// <summary>
        /// 采购模块参数，普通采购,直运采购,受托代销。默认为普通采购
        /// </summary>
        public string sBusType { set; get; }
    }
}
