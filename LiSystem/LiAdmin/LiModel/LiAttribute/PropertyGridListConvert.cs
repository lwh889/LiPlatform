using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LiModel.LiAttribute
{
    /// <summary>
    /// 属性表格下拉框转换类
    /// </summary>
    public class PropertyGridListConvert : ExpandableObjectConverter
    {
        //清楚多余的基类属性
        //public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        // {
        // return new PropertyDescriptorCollection(null);
        // }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(value, attributes);
            PropertyDescriptor[] properties = new PropertyDescriptor[props.Count];
            props.CopyTo(properties, 0);
            PropertyDescriptorCollection newprops = new PropertyDescriptorCollection(properties, false);

            try
            {
                foreach (PropertyDescriptor prop in newprops)
                {
                    if (prop.Name == "Length")
                    {

                        newprops.Remove(prop);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return newprops;
        }

        /// <summary>  
        /// 根据返回值确定是否支持下拉框的形式  
        /// </summary>  
        /// <returns>  
        /// true: 下来框的形式  
        /// false: 普通文本编辑的形式  
        /// </returns>  
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>  
        /// 根据返回值确定是否是不可编辑的文本框  
        /// </summary>  
        /// <returns>  
        /// true: 文本框不可以编辑  
        /// flase: 文本框可以编辑  
        /// </returns>  
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        { 
            PropertyGridListAttribute listAttribute = (PropertyGridListAttribute)context.PropertyDescriptor.Attributes[typeof(PropertyGridListAttribute)];
            StandardValuesCollection vals = new TypeConverter.StandardValuesCollection(listAttribute._lst);
            return vals;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value;
        }
    
    }
}
