using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.LiAttribute;
using LiModel.LiEnum;

namespace LiModel.Form
{
    public class FormModel
    {
        public static FormModel getInstance(string formTemplateType = FormTemplateType.SINGLEVOUCHER)
        {
            string formType = "单据";
            switch (formTemplateType)
            {
                case FormTemplateType.BASEINFO:
                    formType = "基础档案";
                    break;
                case FormTemplateType.TREEBASEINFO:
                    formType = "树形基础档案";
                    break;
            }
            return new FormModel() { id = 0, name = "LiForm1", text="表单标题1", codeFieldName="billCode", keyFieldName = "id", statusFieldName = "billStatus", formType= formType, height = 100, width = 800, panels = new List<PanelModel>(), buttonGroups = new List<ButtonGroupModel>(), events = new List<EventModel>(), listButtons = new List<ListButtonModel>()};
        }

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Browsable(false)]  
        public int id { set; get; }

        /// <summary>
        /// 容器名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        [Description("")]
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
        /// 主键名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("主键名称")]
        public string keyFieldName { set; get; }

        /// <summary>
        /// 单据编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据编码")]
        public string codeFieldName { set; get; }

        /// <summary>
        /// 单据状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据状态")]
        public string statusFieldName { set; get; }

        /// <summary>
        /// 控件类型,在存储过程也要修改sp_CreateTable,VoucherType
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("类型"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "单据", "树形基础档案", "基础档案" })]
        public string formType { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("系统代码")]
        public string systemCode { set; get; }

        [Browsable(false)]
        public List<PanelModel> panels { set; get; }

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

        /// <summary>
        /// 列表按钮
        /// </summary>
        [Browsable(false)]
        public List<ListButtonModel> listButtons;
    }
}
