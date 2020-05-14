using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;
namespace LiModel.Form
{
    /// <summary>
    /// 容器模型
    /// </summary>
    public class PanelModel
    {

        public static PanelModel getInstance(int formModelIdTemp)
        {
            return new PanelModel() { id = 0, formModelId = formModelIdTemp, dock = "top", type = "Basic", name = "panel1", text = "容器1", height = 100, width = 800, controlGroups = new List<ControlGroupModel>(), buttonGroups = new List<ButtonGroupModel>(),events = new List<EventModel>() };
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int id { set; get; }

        /// <summary>
        /// 单据ID
        /// </summary>
        [Browsable(false)]  
        public int formModelId { set; get; }
        /// <summary>
        /// 容器布局
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("布局"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "top", "bottom", "left", "right", "fill" })]
        [Description("")]
        public string dock { set; get; }

        /// <summary>
        /// Basic,Grid，容器类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("类型"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "Basic", "Grid" })]
        [Description("")]
        public string type { set; get; }

        /// <summary>
        /// 容器名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        public string name { set; get; }

        /// <summary>
        /// 容器标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string text { set; get; }

        /// <summary>
        /// 容器高度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("高度")]
        public int height { set; get; }

        /// <summary>
        /// 容器宽度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("宽度")]
        public int width { set; get; }

        /// <summary>
        /// 表名
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("表名")]
        public string tableName { set; get; }

        /// <summary>
        /// 主键名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("主键名称")]
        public string primaryKeyName { set; get; }

        /// <summary>
        /// 外键名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("外键名称")]
        public string foreigntKeyName { set; get; }

        /// <summary>
        /// 父表上的集合名字
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("在父表集合名称")]
        public string entityColumnName { set; get; }

        /// <summary>
        /// 所有集合名字
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("所有集合名字")]
        public string childEntityColumnNames { set; get; }

        /// <summary>
        /// 主键类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("主键类型"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "Identity", "Uniqueidentifier" })]
        public string keyType { set; get; }

        /// <summary>
        /// 父表名
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("父表名")]
        public string parentTableName { set; get; }

        /// <summary>
        /// 父主键名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("父主键名称")]
        public string parentPrimaryKeyName { set; get; }

        /// <summary>
        /// 控件组的组
        /// </summary>
        [Browsable(false)]  
        public List<ControlGroupModel> controlGroups;

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        [Browsable(false)]
        public List<ButtonGroupModel> buttonGroups;

        /// <summary>
        /// 事件组
        /// </summary>
        [Browsable(false)]
        public List<EventModel> events;
    }
}
