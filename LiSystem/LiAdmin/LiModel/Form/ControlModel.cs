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
        public static List<ControlModel> getInstancesByModel(object entity)
        {
            List<ControlModel> controls = new List<ControlModel>();

            var t = entity.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(EditAttribute), true))
                {
                    ControlModel customerControlModel = new ControlModel();
                    customerControlModel.name = property.Name;

                    controls.Add(customerControlModel);

                    var attributes = property.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (typeof(ControlTypeAttribute).FullName.Equals(attribute.GetType().FullName))
                        {
                            customerControlModel.controltype = Convert.ToString(attribute.GetType().GetProperty("Value").GetValue(attribute));
                        }

                        if (typeof(DisplayAttribute).FullName.Equals(attribute.GetType().FullName))
                        {
                            customerControlModel.text = Convert.ToString(attribute.GetType().GetProperty("Name").GetValue(attribute));
                            customerControlModel.row = Convert.ToInt16(attribute.GetType().GetProperty("Order").GetValue(attribute));
                        }
                    }

                    if (string.IsNullOrEmpty(customerControlModel.controltype))
                    {

                        switch (property.GetType().FullName)
                        {
                            case "System.Boolean":
                                customerControlModel.controltype = "CheckEdit";
                                break;
                            case "System.String":
                                customerControlModel.controltype = "TextEdit";
                                break;
                            case "System.DateTime":
                                customerControlModel.controltype = "CalcEdit";
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Double":
                            case "System.Float":
                                customerControlModel.controltype = "CalcEdit";
                                break;
                            default:
                                customerControlModel.controltype = "TextEdit";
                                break;
                        }
                    }
                }

            }

            return controls;
        }

        public static ControlModel getInstance(int controlGroupId)
        {
            return new ControlModel() { id = 0, controlGroupId = controlGroupId, name = "control1", text = "控件名称", length = 300, height = 24, col = 1, row = 1, controltype = "TextEdit",bVisibleInList=true, bVisible = true };
        }

        public static ControlModel getVoucherCode()
        {
            return new ControlModel() { id = 0, name = "billCode", text = "单据编号", width = 300, length = 20, height = 24, col = 1, row = 1, controltype = "VoucherCodeEdit", bVisibleInList = true, bVisible = true };
        }

        /// <summary>
        /// 用户控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControl()
        {
            return new ControlModel() { id = 0, width = 300, length = 20, height = 24, col = 1, row = 1, controltype = "UserEdit", bVisibleInList = true, bVisible = true, basicInfoKey = "liUsers", basicInfoTableKey = "userCode", basicInfoShowFieldName = "userName", basicInfoShowMode = "NAME" };
        }
        /// <summary>
        /// 日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControl()
        {
            return new ControlModel() { id = 0, width = 300, length = 20, height = 24, col = 1, row = 1, controltype = "DateEdit", bVisibleInList = true, bVisible = true };
        }

        /// <summary>
        /// 制单人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlMaker()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cMaker";
            controlModel.text = "制单人";
            return controlModel;
        }
        /// <summary>
        /// 制单日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlMakerDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dMakeDate";
            controlModel.text = "制单日期";
            return controlModel;
        }

        /// <summary>
        /// 修改人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlModifer()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cModifer";
            controlModel.text = "修改人";
            return controlModel;
        }
        /// <summary>
        /// 修改日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlModifyDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dModifiedDate";
            controlModel.text = "修改日期";
            return controlModel;
        }

        /// <summary>
        /// 提交人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlSumbit()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cSumbit";
            controlModel.text = "提交人";
            return controlModel;
        }
        /// <summary>
        /// 提交日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlSumbitDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dSumbitDate";
            controlModel.text = "提交日期";
            return controlModel;
        }

        /// <summary>
        /// 审核人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlAuditor()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cAuditor";
            controlModel.text = "审核人";
            return controlModel;
        }
        /// <summary>
        /// 审核日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlAuditDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dAuditDate";
            controlModel.text = "审核日期";
            return controlModel;
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
        /// 控件类型,在存储过程也要修改sp_CreateTable
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
