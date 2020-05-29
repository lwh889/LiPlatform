using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8ParamModel
    {
        public int id { set; get; }
        public int fid { set; get; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string paramName { set; get; }
        /// <summary>
        /// 参数描述
        /// </summary>
        public string paramDesc { set; get; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string paramType { set; get; }
        /// <summary>
        /// 参数方向
        /// </summary>
        public string paramDirection { set; get; }
        /// <summary>
        /// 传递方式
        /// </summary>
        public string paramTransMode { set; get; }
        /// <summary>
        /// 是否可选
        /// </summary>
        public string paramIsRequire { set; get; }
        /// <summary>
        /// BO对象
        /// </summary>
        public string paramBoObject { set; get; }
        /// <summary>
        /// 是否BO表头
        /// </summary>
        public string paramBoType { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string paramDefaultValue { set; get; }
        /// <summary>
        /// 是否是ID字段名
        /// </summary>
        public bool parambID { set; get; }
        /// <summary>
        /// 是否是Code字段名
        /// </summary>
        public bool parambCode { set; get; }
    }
}
