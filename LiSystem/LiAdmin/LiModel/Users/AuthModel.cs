using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Users
{
    public class AuthModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }	//数据ID，主键

        /// <summary>
        /// 单据Key
        /// </summary>
        public string entityKey { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string roleCode { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool bShow { get; set; }	
    }
}
