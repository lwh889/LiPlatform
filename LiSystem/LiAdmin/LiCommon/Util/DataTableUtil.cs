using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LiCommon.Util
{
    /// <summary>
    /// 数据表转换类
    /// </summary>
    public class DataTableUtil
    {
        /// <summary>
        /// 根据字典获取空表
        /// </summary>
        /// <param name="tableInfoDict">列名-列类型</param>
        /// <returns></returns>
        public static DataTable getEmptyDataTable(Dictionary<string,object> tableInfoDict){
            DataTable dt = new DataTable();
            foreach (KeyValuePair<string, object> kvp in tableInfoDict)
            {
                DataColumn dc = null;
                switch (Convert.ToString(kvp.Value).ToLower())
                {
                    case "binary":
                    case "varbinary":
                    case "byte":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Byte"));
                        break;
                    case "tinyint":
                    case "smallint":
                    case "short":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Int16"));
                        break;
                    case "int":
                    case "integer":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Int32"));
                        break;
                    case "long":
                    case "bigint":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Int64"));
                        break;
                    case "bit":
                    case "boolean":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Boolean"));
                        break;
                    case "numeric":
                    case "decimal":
                    case "double":
                    case "float":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Double"));
                        break;
                    case "character":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.Char"));
                        break;
                    case "time":
                    case "date":
                    case "timestamp":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.DateTime"));
                        break;
                    case "nchar":
                    case "char":
                    case "text":
                    case "ntext":
                    case "varchar":
                    case "nvarchar":
                    case "string":
                        dc = new DataColumn(kvp.Key, Type.GetType("System.String"));
                        break;
                    default:
                        dc = new DataColumn(kvp.Key, Type.GetType("System.String"));
                        break;
                }
                dt.Columns.Add(dc);
            }
            return dt;
        }
    }
}
