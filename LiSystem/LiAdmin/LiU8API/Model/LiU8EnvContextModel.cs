using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8EnvContextModel
    {
        public int id { set; get; }
        public int fid { set; get; }
        /// <summary>
        /// 上下文名称
        /// </summary>
        public string contextName { set; get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string contextDesc { set; get; }
        /// <summary>
        /// 上下文类型
        /// </summary>
        public string contextType { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string contextDefaultValue { set; get; }
    }
}
