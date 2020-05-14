using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using LiModel.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using LiModel.Form;

namespace LiModel.Users
{
    public class UserModel:AGridlookUpEditModelBase
    {
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }

        public override string getValueMember()
        {
            return "userCode";
        }

        public override string getDisplayMember()
        {
            return "userName";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("userName"))
            {
                SearchColumns.Add("userName");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("userCode"))
            {
                DisplayColumns.Add("userCode");
            }
            if (!DisplayColumns.Contains("userName"))
            {
                DisplayColumns.Add("userName");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("userCode"))
            {
                DictModelDesc.Add("userCode", "用户编码");
            }
            if (!DictModelDesc.ContainsKey("userName"))
            {
                DictModelDesc.Add("userName", "用户名称");
            }
            return DictModelDesc;
        }

        public int id { set; get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Edit]
        [Display(Name = "用户编码", Order = 1)]
        public string userCode { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Edit]
        [Display(Name = "用户名称", Order = 2)]
        public string userName { set; get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Edit]
        [Display(Name = "用户密码", Order = 3)]
        [JsonConverter(typeof(PasswordConverter))]
        public string userPassword { set; get; }

        /// <summary>
        /// 皮肤
        /// </summary>
        [Display(Name = "皮肤", Order = 3)]
        public string skinName { set; get; }

        /// <summary>
        /// 管理员
        /// </summary>
        [Edit]
        [ControlTypeAttribute("CheckEdit")]
        [Display(Name = "管理员", Order = 4)]
        public bool bAdmin { set; get; }

        private DateTime _modifyDate;

        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime modifyDate { get { _modifyDate = DateTime.Now; return _modifyDate; } set { _modifyDate = value; } }
    }
}
