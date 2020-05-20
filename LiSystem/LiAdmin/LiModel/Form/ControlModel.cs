using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LiModel.Form
{
    public class ControlModel
    {

        public static ControlModel getInstance(int controlGroupId)
        {
<<<<<<< HEAD
            return new ControlModel() { id = 0, controlGroupId = controlGroupId, name = "control1", text = "控件名称", length = 400, height = 24, col = 1, row = 1, controltype = "TextEdit", bVisibleInList = true, bVisible = true, bIsNull = true };
=======
            return new ControlModel() { id = 0, controlGroupId = controlGroupId, name = "control1", text = "控件名称", length = 300, height = 24, col = 1, row = 1, controltype = "TextEdit", bVisibleInList = true, bVisible = true, bIsNull = true };
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int id { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int controlGroupId { set; get; }

        /// <summary>
        /// 控件名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        public string name { set; get; }

        /// <summary>
        /// 控件标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string text { set; get; }


        /// <summary>
        /// 控件宽度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("宽度")]
        public int width { set; get; }

        /// <summary>
        /// 控件高度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("高度")]
        public int height { set; get; }

        /// <summary>
        /// 第几列
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("列位置")]
        public int col { set; get; }

        /// <summary>
        /// 第几行
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("行位置")]
        public int row { set; get; }

        /// <summary>
        /// 控件类型,在存储过程也要修改sp_CreateTable,Controltype
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("控件类型"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "VoucherCodeEdit", "TextEdit", "CheckEdit", "MemoEdit", "DecimalEdit", "IntEdit", "DateTimeEdit", "TimeEdit", "GridLookUpEditComboBox", "GridLookUpEditRef", "GridLookUpEditRefAssist", "TreeListLookUpEdit", "UserEdit", "DateEdit", "StatusEdit" })]
        public string controltype { set; get; }

        /// <summary>
        /// 数据长度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("长度")]
        public int length { set; get; }

        /// <summary>
        /// 数据精度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("小数精度")]
        public int scale { set; get; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("是否允许为空")]
        public bool bIsNull { set; get; }

        private bool _bReadOnly;
        /// <summary>
        /// 是否只读
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("只读")]
        public bool bReadOnly { 
            set { _bReadOnly = value; } 
            get { 
                if (_bReadOnly) 
                { return _bReadOnly; } 
                else {
                    switch (controltype)
                    {
                        case "VoucherCodeEdit":
                        case "GridLookUpEditRefAssist":
                            return true;
                            break;
                    }
                    return _bReadOnly;
                }
            } 
        }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("必填")]
        public bool bRequired { set; get; }

        /// <summary>
        /// 默认值
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("默认值")]
        public string defaultVaule { set; get; }
        /// <summary>
        ///  是否显示
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据是否显示")]
        public bool bVisible { set; get; }

        /// <summary>
        ///  是否显示
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("列表是否显示")]
        public bool bVisibleInList { set; get; }

        /// <summary>
        ///  基础档案
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("基础档案")]
        public string basicInfoKey { set; get; }


        /// <summary>
        ///  字典档案类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("字典档案类型")]
        public string dictInfoType { set; get; }

        /// <summary>
        ///  基础档案主键
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("基础档案主键")]
        public string basicInfoTableKey { set; get; }

        /// <summary>
        ///  基础档案显示字段
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("基础档案显示字段")]
        public string basicInfoShowFieldName { set; get; }

        /// <summary>
        ///  基础档案辅助类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("基础档案辅助类型")]
        public string basicInfoAssistType { set; get; }

        /// <summary>
        ///  基础档案辅助显示字段名
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("基础档案辅助显示字段名")]
        public string basicInfoAssistFieldName { set; get; }

        /// <summary>
        /// 控件显示模式Json
        /// </summary>
        [Browsable(false)]  
        public string gridlookUpEditShowModelJson { set; get; }

        /// <summary>
        /// 基础档案显示模式
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("基础档案显示模式"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "VALUE", "NAME", "NAME_VALUE", "VALUE_NAME" })]
        public string basicInfoShowMode { set; get; }
    }
}
