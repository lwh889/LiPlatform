using LiCommon.Util;
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
    public class LiU8STCO : ALiU8CO, ILiU8CO
    {
        private USERPCO.VoucherCO STCo;

        private MSXML2.IXMLDOMDocument2 domPos;

        private IXMLDOMDocument2 _DomMsg;

        /// <summary>
        /// 
        /// </summary>
        public IXMLDOMDocument2 DomMsg { set { _DomMsg = value; } get { return _DomMsg; } }

        public LiU8STCO()
        {
            DomMsg = new MSXML2.DOMDocumentClass();
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
            domHead = new MSXML2.DOMDocumentClass();
            domBody = new MSXML2.DOMDocumentClass();
            domPos = new MSXML2.DOMDocumentClass();

            //正式
            setDomHeadByDataBase(domHead);
            setDomBodyByDataBase(domBody, row);
            SetDefaultValue();

            ////测试
            //setDomHeadTest(domHead);
            //setDomBodyTest(domBody, row);
            //domHead.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";
            //domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";
            //domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("autoid").nodeValue = "";
        }
        public void SetApiContext(string paramName, object paramValue)
        {
            ModelUtil.setValue<LiU8STCO>(paramName, paramValue, this);
        }
        public LiU8COReponseModel Insert()
        {
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.bSuccess = STCo.Insert(vouchType, domHead, domBody, domPos, ref errMsg, null, ref vouchId, ref _DomMsg, false, false);
            liU8COReponse.resultContent = errMsg;
            liU8COReponse.vouchID = vouchId;

            if (liU8COReponse.bSuccess)
            {
                GetVouchDataByID(liU8COReponse);
            }

            return liU8COReponse;
        }
        public LiU8COReponseModel Audit()
        {
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.bSuccess = STCo.Verify(vouchType, vouchId, ref errMsg, null, null, null, false, false);
            liU8COReponse.bAuditSuccess = liU8COReponse.bSuccess;
            liU8COReponse.resultContent = errMsg;
            liU8COReponse.vouchID = vouchId;
            return liU8COReponse;
        }
        public LiU8COReponseModel UnAudit()
        {
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.bSuccess = STCo.UnVerify(vouchType, vouchId, ref errMsg, null, getTimestamp(this.vouchId), null, false, false);
            liU8COReponse.resultContent = errMsg;
            liU8COReponse.bDeleteSuccess = liU8COReponse.bSuccess;
            liU8COReponse.vouchID = vouchId;
            return liU8COReponse;
        }
        public LiU8COReponseModel Delete()
        {
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.bSuccess = STCo.Delete(vouchType, vouchId, ref errMsg, null, getTimestamp(this.vouchId), ref _DomMsg, false, false, true);
            liU8COReponse.resultContent = errMsg;
            liU8COReponse.vouchID = vouchId;
            return liU8COReponse;
        }
    }
}
