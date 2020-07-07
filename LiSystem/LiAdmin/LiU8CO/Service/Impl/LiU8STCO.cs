using LiU8CO.Model;
using LiU8CO.Util;
using MSXML2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Service.Impl
{
    public class LiU8STCO : ALiU8CO,ILiU8CO
    {
        private USERPCO.VoucherCO STCo;

        private MSXML2.IXMLDOMDocument2 domPos;

        public LiU8STCO()
        {
            domHead = new MSXML2.DOMDocumentClass();
            domBody = new MSXML2.DOMDocumentClass();
            domPos = new MSXML2.DOMDocumentClass();
        }
        public void InitCO()
        {
            STCo = new USERPCO.VoucherCO();

            STCo.IniLogin(u8Login, errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                throw new Exception(errMsg);
            }
        }

        public void InitDom(int row)
        {
            //表头
            rs.Open(liU8COVoucher.domHeadSql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            rs.Save(domHead, ADODB.PersistFormatEnum.adPersistXML);
            rs.Close();
            //清除原有数据
            foreach (IXMLDOMNode en in domHead.selectNodes("//z:row"))
            {
                domHead.selectSingleNode("//rs:data").removeChild(en);
            }
            //增加
            DomUtil.FormatDom(ref domHead, "A");
            eleHead = domHead.selectSingleNode("//z:row") as IXMLDOMElement;


            //表体
            rs.Open(liU8COVoucher.domBodySql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            rs.Save(domBody, ADODB.PersistFormatEnum.adPersistXML);
            rs.Close();
            //清除原有数据
            foreach (IXMLDOMNode en in domBody.selectNodes("//z:row"))
            {
                domBody.selectSingleNode("//rs:data").removeChild(en);
            }
            //增加
            DomUtil.FormatDom(ref domBody, "");
            MSXML2.DOMDocument domBodyCopy = (MSXML2.DOMDocument)domBody.cloneNode(true);
            foreach (IXMLDOMNode en in domBody.selectNodes("//z:row"))
            {
                domBody.selectSingleNode("//rs:data").removeChild(en);
            }
            IXMLDOMNode ele = domBodyCopy.selectSingleNode("//z:row");
            for (int i = 0; i < row; i++){
                domBody.selectSingleNode("//rs:data").appendChild(ele.cloneNode(true));
            }
        }

        public void setDefaultValue()
        {
            List<LiU8COFieldModel> liU8COHeadFields = liU8COVoucher.liU8COFields.Where(m => m.fieldEntityType == "Head" && m.fieldbDefault == true).ToList();
            foreach(LiU8COFieldModel liU8COField in liU8COHeadFields)
            {
                setDomHeadValue(liU8COField.fieldName, liU8COField.fieldDefaultValue);
            }


            List<LiU8COFieldModel> liU8COBodyFields = liU8COVoucher.liU8COFields.Where(m => m.fieldEntityType == "Body" && m.fieldbDefault == true).ToList();

            IXMLDOMNodeList ndbodylist = domBody.selectNodes("//rs:data/z:row");
            foreach (IXMLDOMElement body in ndbodylist)
            {
                setDomBodyRow(body);
                foreach (LiU8COFieldModel liU8COField in liU8COBodyFields)
                {
                    setDomBodyValue(liU8COField.fieldName, liU8COField.fieldDefaultValue);
                }
            }
        }

        public LiU8COReponseModel Insert()
        {
            setDefaultValue();
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.bSuccess = STCo.Insert("01", domHead, domBody, domPos, ref errMsg, null, ref vouchId);
            liU8COReponse.resultContent = errMsg;
            liU8COReponse.voucherID = vouchId;

            return liU8COReponse;
        }
        public LiU8COReponseModel Audit()
        {
            return null;
        }
        public LiU8COReponseModel UnAudit()
        {
            return null;
        }
        public LiU8COReponseModel Delete()
        {
            return null;
        }
    }
}
