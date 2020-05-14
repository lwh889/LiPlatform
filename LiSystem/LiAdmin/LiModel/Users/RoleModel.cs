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


namespace LiModel.Users
{
    /// <summary>
    /// 角色模型
    /// </summary>
    public class RoleModel
    {
        public int id { set; get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Edit]
        [Display(Name = "角色编码", Order = 1)]
        public string roleCode { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Edit]
        [Display(Name = "角色名称", Order = 2)]
        public string roleName { set; get; }

        private DateTime _modifyDate;

        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime modifyDate { get { _modifyDate = DateTime.Now; return _modifyDate; } set { _modifyDate = value; } }
    }
}
