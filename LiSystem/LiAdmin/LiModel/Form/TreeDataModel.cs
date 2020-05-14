using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    /// <summary>
    /// 树模型
    /// </summary>
    public class TreeDataModel
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public int ID { get; set; }	//数据ID，主键

        /// <summary>
        /// 节点编码
        /// </summary>
        public string Code { get; set; }	//数据名称

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }	//数据名称

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { get; set; }	//系统代码

        /// <summary>
        /// 是否是组
        /// </summary>
        public bool isGroup { get; set; }

        /// <summary>
        /// 是否是系统
        /// </summary>
        public bool isSystem { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public int GroupId { get; set; }	//分组ID，当前位于树形菜单第几级的意思

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentID { get; set; }	//父标签ID，父标签的数据ID

        /// <summary>
        /// 图标索引
        /// </summary>
        public int imageIndex { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int iOrder { get; set; }	//
    }
}
