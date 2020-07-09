using LiCommon.Util;
using LiU8CO.Model;
using MSXML2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Service.Impl
{
    public class LiU8SACO : ALiU8CO, ILiU8CO
    {
        public IXMLDOMDocument2 _domBodyForLog;
        public IXMLDOMDocument2 domBodyForLog { set { _domBodyForLog = value; }get { return _domBodyForLog; } }

        private VoucherCO_Sa.ClsVoucherCO_SA SACo;
        private object clssys;
        private string strUserMode = string.Empty;

        public LiU8SACO()
        {
            domBodyForLog = new MSXML2.DOMDocumentClass();
        }
        public void InitCO()
        {
            //VB
            //Set SACo = New VoucherCO_Sa.ClsVoucherCO_SA
            //Dim clssys As Object
            //Set clssys = CreateObject("USSAServer.clsSystem")
            //clssys.Init dlogin
            //Call SACo.Init(SODetails, dlogin, connDest, , clssys)

            SACo = new VoucherCO_Sa.ClsVoucherCO_SAClass();

            System.Type oType = System.Type.GetTypeFromProgID("USSAServer.clsSystem");
            clssys = System.Activator.CreateInstance(oType);
            oType.InvokeMember("Init", System.Reflection.BindingFlags.InvokeMethod, null, clssys, new object[] { this.u8Login });

            SACo.Init( VoucherCO_Sa.VoucherTypeSA.SODetails, this.u8Login, null, ref strUserMode,ref clssys);

            //if (!string.IsNullOrEmpty(errMsg))
            //{
            //    throw new Exception(errMsg);
            //}
        }

        public void SetApiContext(string paramName, object paramValue)
        {
            AttributeUtil.setValue<LiU8STCO>(paramName, paramValue, this);
        }

        public void InitDom(int row)
        {
            domHead = new MSXML2.DOMDocumentClass();
            domBody = new MSXML2.DOMDocumentClass();

            SACo.GetVoucherData(ref domHead, ref domBody);

            setDomHead(domHead);
            setDomBody(domBody, row);
            SetDefaultValue();
        }

        public LiU8COReponseModel Insert()
        {
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.resultContent = SACo.Save(domHead, domBody, 0, ref vouchIdObject);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            liU8COReponse.voucherID = Convert.ToString(vouchIdObject);
            return liU8COReponse;
        }

        public LiU8COReponseModel Audit()
        {
            IXMLDOMDocument2 domHead = new MSXML2.DOMDocumentClass();
            IXMLDOMDocument2 domBody = new MSXML2.DOMDocumentClass();

            liU8COReponse = LiU8COReponseModel.getInstance();
            SACo.GetVoucherData(ref domHead, ref domBody, vouchId);
            liU8COReponse.resultContent = SACo.VerifyVouch(domHead, true);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            return liU8COReponse;
        }

        public LiU8COReponseModel UnAudit()
        {
            IXMLDOMDocument2 domHead = new MSXML2.DOMDocumentClass();
            IXMLDOMDocument2 domBody = new MSXML2.DOMDocumentClass();

            liU8COReponse = LiU8COReponseModel.getInstance();
            SACo.GetVoucherData(ref domHead, ref domBody, vouchId);
            liU8COReponse.resultContent = SACo.VerifyVouch(domHead, false);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            return liU8COReponse;
        }

        public LiU8COReponseModel Delete()
        {
            IXMLDOMDocument2 domHead = new MSXML2.DOMDocumentClass();
            IXMLDOMDocument2 domBody = new MSXML2.DOMDocumentClass();

            liU8COReponse = LiU8COReponseModel.getInstance();
            SACo.GetVoucherData(ref domHead, ref domBody, vouchId);
            liU8COReponse.resultContent = SACo.Delete(domHead, ref _domBodyForLog);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            return liU8COReponse;
        }
    }
}
