using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Model
{
    public class LiU8ApiGetDataModel
    {
        public static LiU8ApiGetDataModel convertLiU8ApiGetDataModel(object liU8ApiInfo)
        {
            if (liU8ApiInfo == null) return null;

            LiU8ApiGetDataModel liU8ApiGetData = new LiU8ApiGetDataModel();
            liU8ApiGetData.iQueryType = 0;
            liU8ApiGetData.iEnd = 1000;
            liU8ApiGetData.iStart = 1;
            liU8ApiGetData.sOrderByString = "";
            liU8ApiGetData.sSelectFields = "*";
            switch (liU8ApiInfo.GetType().Name)
            {
                case "LiU8ApiIdModel":
                    LiU8ApiIdModel liU8ApiId = liU8ApiInfo as LiU8ApiIdModel;
                    liU8ApiGetData.sAccID = liU8ApiId.sAccID;
                    liU8ApiGetData.sDate = liU8ApiId.sDate;
                    liU8ApiGetData.sPassword = liU8ApiId.sPassword;
                    liU8ApiGetData.sSubId = liU8ApiId.sSubId;
                    liU8ApiGetData.sUserID = liU8ApiId.sUserID;
                    liU8ApiGetData.sYear = liU8ApiId.sYear;

                    break;
                case "LiU8ApiDataModel":
                    LiU8ApiDataModel liU8ApiData = liU8ApiInfo as LiU8ApiDataModel;
                    liU8ApiGetData.sAccID = liU8ApiData.sAccID;
                    liU8ApiGetData.sDate = liU8ApiData.sDate;
                    liU8ApiGetData.sPassword = liU8ApiData.sPassword;
                    liU8ApiGetData.sSubId = liU8ApiData.sSubId;
                    liU8ApiGetData.sUserID = liU8ApiData.sUserID;
                    liU8ApiGetData.sYear = liU8ApiData.sYear;
                    break;
                default:
                    return null;
            }

            return liU8ApiGetData;
        }
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
        public int iQueryType { set; get; }
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
