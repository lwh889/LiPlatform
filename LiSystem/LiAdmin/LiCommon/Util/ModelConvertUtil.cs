using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.Util
{
    public class ModelConvertUtil
    {
        /// <summary>
        /// List泛型转换DataTable.
        /// </summary>
        public DataTable ListToDataTable<TEntity>(List<TEntity> items)
        {
            var tb = new DataTable(typeof(TEntity).Name);

            PropertyInfo[] props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (TEntity item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// model转换DataTable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public DataTable ModelToDataTable<TEntity>(TEntity items)
        {
            var tb = new DataTable(typeof(TEntity).Name);

            PropertyInfo[] props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }


            var values = new object[props.Length];

            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(items, null);
            }

            tb.Rows.Add(values);


            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// DataTable转换泛型List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<TEntity> DataTableToList<TEntity>(DataTable dt) where TEntity : new()
        {
            // 定义集合
            List<TEntity> ts = new List<TEntity>();

            // 获得此模型的类型
            Type type = typeof(TEntity);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                TEntity t = new TEntity();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }


        public static TEntity DataTableToModel<TEntity>(DataTable dt) where TEntity : new()
        {
            // 定义实体
            TEntity t = new TEntity();

            // 获得此模型的类型
            Type type = typeof(TEntity);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {

                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                break;
            }
            return t;
        }
    }
}
