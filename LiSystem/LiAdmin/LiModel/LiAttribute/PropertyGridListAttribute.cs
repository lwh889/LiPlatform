using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiAttribute
{
    /// <summary>
    /// 属性表格下拉框值
    /// </summary>
    public class PropertyGridListAttribute : Attribute
    {
        public string[] _lst;
        public PropertyGridListAttribute(string[] lst)  
        { 
            //初始化列表值 
            _lst =lst; 
        }
    }
}
