using MSXML2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Util
{
    public class DomUtil
    {
        public static void FormatDom(ref MSXML2.DOMDocument SourceDom, string editprop)
        {
            IXMLDOMElement ele_body;

            IXMLDOMNodeList ndheadlist;
            IXMLDOMNodeList ndbodylist;

            String Filedname;
            //'格式部分
            ndheadlist = SourceDom.selectNodes("//s:Schema/s:ElementType/s:AttributeType");
            ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
            if (ndbodylist.length == 0)
            {
                ele_body = SourceDom.createElement("z:row");
                SourceDom.selectSingleNode("//rs:data").appendChild(ele_body);
            }
            ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
            foreach (IXMLDOMElement body in ndbodylist)
            {
                foreach (IXMLDOMElement head in ndheadlist)
                {
                    Filedname = head.attributes.getNamedItem("name").nodeValue + "";
                    if (body.attributes.getNamedItem(Filedname) == null)
                        //  '若没有当前元素，就增加当前元素
                        body.setAttribute(Filedname, "");
                    switch (head.lastChild.attributes.getNamedItem("dt:type").nodeValue.ToString())
                    {
                        case "number":
                        case "float":
                        case "boolean":
                            if (body.attributes.getNamedItem(Filedname).nodeValue.ToString().ToUpper() == "false".ToUpper())
                                body.setAttribute(Filedname, 0);
                            break;
                        default:


                            if (body.attributes.getNamedItem(Filedname).nodeValue.ToString().ToUpper() == "否".ToUpper())
                                body.setAttribute(Filedname, 0);
                            break;
                    }

                }
                if (editprop != "")
                    body.setAttribute("editprop", editprop);
            }

        }
    }
}
