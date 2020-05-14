using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiAttribute
{

    public class BaseAttribute : Attribute
    {
        public BaseAttribute() { }


        /// <summary>
        /// 获取类属性值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="A">属性类型</typeparam>
        /// <returns></returns>
        public static bool IsExistAttributeInClass<T, A>() where T : new()
        {
            var obj = new T();
            var t = obj.GetType();



            if (t.IsDefined(typeof(A), true)) return true;
            else return false;

        }

        public static List<string> GetFieldNameListContainAttribute<T, A>() where T : new()
        {
            List<string> fieldNames = new List<string>();

            var obj = new T();
            var t = obj.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {

                if (property.IsDefined(typeof(A), true)) fieldNames.Add(property.Name);
            }

            return fieldNames;
        }

        /// <summary>
        /// 获取实体字段类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> getModelTypeDictionary<T, A>() where T : new()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            var obj = new T();
            var t = obj.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                dict.Add(property.Name, property.PropertyType.FullName);
            }

            return dict;
        }
    }

}
