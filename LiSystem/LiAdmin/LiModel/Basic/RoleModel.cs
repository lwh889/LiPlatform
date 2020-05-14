using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LiModel.Basic
{
    public class RoleModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string roleCode { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string roleName { set; get; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime modifyDate { set; get; }

    }
}
