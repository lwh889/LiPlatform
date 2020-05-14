using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiAttribute
{
    /// <summary>
    /// 自定义属性
    /// </summary>
    public class SingleAttribute : BaseAttribute
    {

        private object _value;
        public SingleAttribute(object value)
        {
            _value = value;
        }

        public virtual object Value
        {
            get { return _value; }
        }

        /// <summary>
        /// 获取类的字段名
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="A">属性类型</typeparam>
        /// <returns></returns>
        public static string getProperyName<T, A>() where T : new()
        {
            var obj = new T();
            var t = obj.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (!property.IsDefined(typeof(A), true)) continue;

                var attributes = property.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    return attribute.GetType().GetProperty("Value").Name;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取类属性值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="A">属性类型</typeparam>
        /// <returns></returns>
        public static object getClassValue<T, A>() where T : new()
        {
            var obj = new T();
            var t = obj.GetType();

            if (!t.IsDefined(typeof(A), true)) return null;

            var attributes = t.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                if (typeof(A).FullName.Equals(attribute.GetType().FullName))
                {
                    return attribute.GetType().GetProperty("Value").GetValue(attribute);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取类属性值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="A">属性类型</typeparam>
        /// <returns></returns>
        public static object getClassName<T, A>() where T : new()
        {
            var obj = new T();
            var t = obj.GetType();

            if (!t.IsDefined(typeof(A), true)) return null;

            var attributes = t.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                if (typeof(A).FullName.Equals(attribute.GetType().FullName))
                {
                    return attribute.GetType().GetProperty("Name").GetValue(attribute);
                }
            }
            return null;
        }



        /// <summary>
        /// 获取实体字段描述字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> getModelDescDictionary<T, A>(string attributeName) where T : new()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            var obj = new T();
            var t = obj.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (!property.IsDefined(typeof(A), true)) continue;

                var attributes = property.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (typeof(A).FullName.Equals(attribute.GetType().FullName))
                    {
                        dict.Add(property.Name, Convert.ToString(attribute.GetType().GetProperty(attributeName).GetValue(attribute)));
                    }
                }


                //获取属性的值
                //var propertyValue = property.GetValue(obj) as string;
                //if (propertyValue == null)
                //    throw new Exception("exception info");//这里可以自定义，也可以用具体系统异常类
            }

            return dict;
        }

        /// <summary>
        /// 获取类的字段属性值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="A">属性类型</typeparam>
        /// <returns></returns>
        public static object getValue<T, A>(string sFieldName) where T : new()
        {
            var obj = new T();
            var t = obj.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (!property.IsDefined(typeof(A), true)) continue;

                var attributes = property.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    if (sFieldName.Equals(attribute.GetType().Name))
                        return attribute.GetType().GetProperty("Value").GetValue(attribute);
                }


                //获取属性的值
                //var propertyValue = property.GetValue(obj) as string;
                //if (propertyValue == null)
                //    throw new Exception("exception info");//这里可以自定义，也可以用具体系统异常类
            }

            return false;
        }
    }
}
