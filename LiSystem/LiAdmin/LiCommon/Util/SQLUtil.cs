using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.Util
{
    public class SQLUtil
    {
        public static string getFieldNameFormat(string tableName, string columnName)
        {
            return string.Format("Li{0}_{1}", tableName, columnName);
        }
    }
}
