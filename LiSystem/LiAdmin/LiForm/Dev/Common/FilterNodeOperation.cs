using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

namespace LiForm.Dev.Common
{

    /// <summary>
    /// 过滤节点操作类
    /// </summary>
    public class FilterNodeOperation : TreeListOperation
    {
        string pattern;
        public FilterNodeOperation(string _pattern) { pattern = _pattern; }

        public override void Execute(TreeListNode node)
        {
            if (NodeContainsPattern(node, pattern))
            {
                node.Visible = true;
                if (node.ParentNode != null)
                {
                    node.ParentNode.Visible = true;
                }
            }
            else
            {
                node.Visible = false;
            }
        }

        /// <summary>
        /// 模糊包含模式
        /// </summary>
        bool NodeContainsPattern(TreeListNode node, string pattern)
        {
            foreach (TreeListColumn col in node.TreeList.Columns)
            {
                if (node.GetValue(col).ToString().ToUpper().Contains(pattern.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
