using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LiModel.Converter;
using Newtonsoft.Json;
using LiModel.Form;

namespace LiModel.Basic
{
    public class SystemInfoModel : AGridlookUpEditModelBase
    {
        public override object getSingleByDataSource(string keyValue)
        {
            List<SystemInfoModel> sList = getDataSource<List<SystemInfoModel>>();
            return sList.Where(m => m.systemCode == keyValue).FirstOrDefault() ;

        }
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }

        public static SystemInfoModel getInstance()
        {
            SystemInfoModel systemInfoModel = new SystemInfoModel();
            systemInfoModel.bDefault = true;
            return systemInfoModel;
        }

        public override string getValueMember()
        {
            return "systemCode";
        }

        public override string getDisplayMember()
        {
            return "systemName";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("systemName"))
            {
                SearchColumns.Add("systemName");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("systemCode"))
            {
                DisplayColumns.Add("systemCode");
            }
            if (!DisplayColumns.Contains("systemName"))
            {
                DisplayColumns.Add("systemName");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("systemCode"))
            {
                DictModelDesc.Add("systemCode", "帐套代码");
            }
            if (!DictModelDesc.ContainsKey("systemName"))
            {
                DictModelDesc.Add("systemName", "帐套名称");
            }
            return DictModelDesc;
        }


        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 系统数据库
        /// </summary>
        public string systemDataBaseName { set; get; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string systemName { set; get; }
        /// <summary>
        /// 系统标题
        /// </summary>
        public string systemTitle { set; get; }
        /// <summary>
        /// 所属数据库
        /// </summary>
        public string companyName { set; get; }
        /// <summary>
        /// 所属数据库
        /// </summary>
        [JsonConverter(typeof(LiModel.Converter.ImageConverter))]
        public Image companyLogo { set; get; }
        /// <summary>
        /// 默认信息
        /// </summary>
        public bool bDefault { set; get; }
    }
}
