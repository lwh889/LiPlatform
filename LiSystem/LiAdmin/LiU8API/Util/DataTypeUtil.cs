using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSXML2;

namespace LiU8API.Util
{
    public class DataTypeUtil
    {
        public static object convertType(string dataType, object value) 
        {
            switch (dataType)
            {
                case "int":
                    return Convert.ToInt32(value);
                    break;
                case "bool":
                    return Convert.ToBoolean(value);
                    break;
                default:
                    return Convert.ToString(value);
                    break;
            }
        }

        public static object getDefaultValue(string dataType)
        {
            switch (dataType)
            {
                case "MSXML2.IXMLDOMDocument2":
                    return  new DOMDocumentClass();
                case "int":
                    int i = 0;
                    return i;
                    break;
                case "bool":
                    bool b = false;
                    return b;
                    break;
                default:
                    string s = "";
                    return s;
                    break;
            }
        }
    }
}
