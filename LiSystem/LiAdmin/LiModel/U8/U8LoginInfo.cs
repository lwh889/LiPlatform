using LiModel.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.U8
{
    public class U8LoginInfo
    {

        /// <summary>
        /// 帐套ID
        /// </summary>
        public string cAcc_Id { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime CurDate { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string cUserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string cUserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string cUserPassWord { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string dbServerName { get; set; }

        /// <summary>
        /// 系统密码
        /// </summary>
        public string SysPassword { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string cMenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string cMenuName { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string cAuthId { get; set; }
    }
}
