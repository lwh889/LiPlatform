using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace LiCommon.Util
{
    /// <summary>
    /// 数据工具类
    /// </summary>
    public class DataUtil
    {

        /// <summary>
        /// 删除表格行
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="deleteEntity">删除行</param>
        /// <param name="list"></param>
        public static void deleteInList<TEntity>(TEntity deleteEntity, List<TEntity> list)
        {
            if (list != null)
            {
                list.Remove(deleteEntity);
            }
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="deleteLists">删除行</param>
        /// <param name="list"></param>
        public static void deleteInList<TEntity>(List<TEntity> deleteLists, List<TEntity> list)
        {
            if (list != null && deleteLists != null)
            {
                foreach (TEntity deleteEntity in deleteLists)
                {
                    list.Remove(deleteEntity);
                }
            }
        }

        /// <summary>
        /// DataRow转字典
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DataRowToDictionary(DataRow dr)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (dr == null) return dict;

            foreach (DataColumn dc in dr.Table.Columns)
            {
                dict.Add(dc.ColumnName, dr[dc.ColumnName]);
            }

            return dict;
        }

        /// <summary>
        /// Table转字典
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> TableToDictionary(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            if (dt == null) return list;

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    dict.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dict);
            }

            return list;
        }

        /// <summary>
        /// 字典转Table
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static DataTable DictionaryToTable(List<Dictionary<string, object>> lists)
        {
            DataTable dt = new DataTable();

            if (lists != null && lists.Count > 0)
            {
                foreach(Dictionary<string, object> dict in lists)
                {
                    foreach (KeyValuePair<string, object> item in dict)
                    {
                        if (dt.Columns.Contains(item.Key) && item.Value != null && item.Value.GetType().Name != "DBNull")
                        {
                            DataColumn dc = dt.Columns[item.Key];

                            if (dc.DataType.Name != item.Value.GetType().Name)
                            {
                                dt.Columns.Remove(dc.ColumnName);
                                dt.Columns.Add(item.Key, item.Value.GetType());
                            }

                        }

                        if (!dt.Columns.Contains(item.Key))
                        {
                            if (item.Value != null && item.Value.GetType().Name != "DBNull")
                                dt.Columns.Add(item.Key, item.Value.GetType());
                            else
                                dt.Columns.Add(item.Key, typeof(string));
                        }

                    }

                }
                foreach (Dictionary<string, object> dictionary in lists)
                {
                    DataRow dr = dt.NewRow();
                    foreach (KeyValuePair<string, object> item in dictionary)
                    {
                        if (item.Value == null)
                        {
                            dr[item.Key] = DBNull.Value;
                        }
                        else
                        {
                            dr[item.Key] = item.Value;
                        }
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, object> getEmptyDictionary(Dictionary<string, object> tableInfoDict)
       {
           Dictionary<string, object> dict = new Dictionary<string, object>();
           foreach (KeyValuePair<string, object> kvp in tableInfoDict)
           {
               if (kvp.Value.GetType().Name == "List" || kvp.Value.GetType().Name == "List`1")
               {
                   List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                   List<Dictionary<string, object>> childTableInfoDict = (List<Dictionary<string, object>>)kvp.Value;
                   if (childTableInfoDict.Count > 0)
                   {
                       list.Add(getEmptyDictionary(childTableInfoDict[0]));
                       dict.Add(kvp.Key, list);
                   }
                   else
                   {
                       dict.Add(kvp.Key, null);
                   }
               }
               else if (kvp.Value.GetType().Name == "JObject")
               {
                   dict.Add(kvp.Key, getEmptyDictionary((Dictionary<string, object>)kvp.Value));
               }
               else
               {

                   switch (Convert.ToString(kvp.Value).ToLower())
                   {
                       case "binary":
                       case "varbinary":
                       case "byte":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Byte")));
                           break;
                       case "tinyint":
                       case "smallint":
                       case "short":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Int16")));
                           break;
                       case "int":
                       case "integer":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Int32")));
                           break;
                       case "long":
                       case "bigint":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Int64")));
                           break;
                       case "bit":
                       case "boolean":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Boolean")));
                           break;
                       case "numeric":
                       case "decimal":
                       case "double":
                       case "float":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Double")));
                           break;
                       case "character":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.Char")));
                           break;
                       case "time":
                       case "date":
                       case "datetime":
                       case "timestamp":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.DateTime")));
                           break;
                       case "nchar":
                       case "char":
                       case "text":
                       case "ntext":
                       case "varchar":
                       case "nvarchar":
                       case "string":
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.String")));
                           break;
                       default:
                           dict.Add(kvp.Key, GetDefaultValue(Type.GetType("System.String")));
                           break;
                   }
               }
               //dt.Columns.Add(dc);
           }
           return dict;
       }

        /// <summary>
        /// 获取变量的默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;

        }


        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }


        /// <summary>
        /// 新建一个对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TEntity newEntity<TEntity>(TEntity entity) where TEntity : new()
        {

            var newEntity = new TEntity();


            return newEntity;
        }
    }
}
