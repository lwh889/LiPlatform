using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Users
{
    public class UserRoleModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }	//数据ID，主键

        /// <summary>
        /// 用户编码
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string roleCode { get; set; }
    }
}
