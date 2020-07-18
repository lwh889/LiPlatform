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
    public class LiU8PUCO : ALiU8CO, ILiU8CO
    {
        private VoucherCO_PU.clsVoucherCO_PUClass PUCo;
        private Info_PU.ClsS_Infor clsInfo;

        private bool _bPositive;
        private string _sBillType;
        private string _sBusType;
        private short _VoucherState;
        private string _sBufType;
        private string _sPtCode;
        private string _sOverDetailsXml;
        private VoucherVerify.UseMode _usMode;
        private VoucherCO_PU.vouchertype vouchertype;

        private IXMLDOMDocument2 _CurDom;
        private IXMLDOMDocument2 _DomMsg;

        /// <summary>
        /// 
        /// </summary>
        public IXMLDOMDocument2 DomMsg { set { _DomMsg = value; } get { return _DomMsg; } }
        /// <summary>
        /// 错误DOM
        /// </summary>
        public IXMLDOMDocument2 CurDom { set { _CurDom = value; } get { return _CurDom; } }

        /// <summary>
        /// 红蓝标识：True,蓝字
        /// </summary>
        public bool bPositive { set { _bPositive = value; }get{ return _bPositive; } }
        /// <summary>
        /// 0到货，1退货，2拒收
        /// </summary>
        public string sBillType { set { _sBillType = value; } get { return _sBillType; } }
        /// <summary>
        /// 2新增，1修改，0非编辑
        /// </summary>
        public short VoucherState { set { _VoucherState = value; } get { return _VoucherState; } }
        /// <summary>
        /// 普通采购,直运采购,受托代销
        /// </summary>
        public string sBusType { set { _sBufType = value; } get { return _sBufType; } }
        /// <summary>
        /// 留空
        /// </summary>
        public string sBufType { set { _sBufType = value; } get { return _sBufType; } }
        /// <summary>
        /// 留空
        /// </summary>
        public string sPtCode { set { _sPtCode = value; } get { return _sPtCode; } }
        /// <summary>
        /// 
        /// </summary>
        public string sOverDetailsXml { set { _sOverDetailsXml = value; } get { return _sOverDetailsXml; } }
        /// <summary>
        /// 模式，0：CS;1:BS
        /// </summary>
        public VoucherVerify.UseMode usMode { set { _usMode = value; } get { return _usMode; } }

        public LiU8PUCO()
        {
            usMode = VoucherVerify.UseMode.CS;
            sPtCode = "";
            sBufType = "";
            sBusType = "";
            sOverDetailsXml = "";
            DomMsg = new MSXML2.DOMDocumentClass();
            CurDom = new MSXML2.DOMDocumentClass();
        }

        public void InitCO()
        {

            PUCo = new VoucherCO_PU.clsVoucherCO_PUClass();
            clsInfo = new Info_PU.ClsS_Infor();
            errMsg = clsInfo.Init(ref this.u8Login, _sBusType, _sPtCode);

            PUCo.Init(getVouchType(vouchType), ref this.u8Login, null, ref clsInfo, ref _bPositive,
                            ref _sBillType, ref _sBusType, ref _usMode, _sBufType, _sPtCode);
        }

        public void InitDom(int row)
        {
            domHead = new MSXML2.DOMDocumentClass();
            domBody = new MSXML2.DOMDocumentClass();

            PUCo.GetVoucherData(ref domHead, ref domBody);

            setDomHead(domHead);
            setDomBody(domBody, row);
            SetDefaultValue();
        }

        public LiU8COReponseModel Insert()
        {
            liU8COReponse = LiU8COReponseModel.getInstance();
            liU8COReponse.resultContent = PUCo.VoucherSave(domHead, domBody, VoucherState, ref vouchIdObject, ref _CurDom, _usMode, ref _sOverDetailsXml,ref _DomMsg );
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            liU8COReponse.vouchID = Convert.ToString(vouchIdObject);

            if (liU8COReponse.bSuccess)
            {
                GetVouchDataByID(liU8COReponse);
            }

            return liU8COReponse;
        }

        public LiU8COReponseModel Audit()
        {
            IXMLDOMDocument2 domHead = new MSXML2.DOMDocumentClass();
            IXMLDOMDocument2 domBody = new MSXML2.DOMDocumentClass();

            liU8COReponse = LiU8COReponseModel.getInstance();
            PUCo.GetVoucherData(ref domHead,ref domBody, "", vouchId);
            liU8COReponse.resultContent = PUCo.ConfirmPO(domHead, ref _DomMsg, domBody);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            liU8COReponse.bAuditSuccess = liU8COReponse.bSuccess;
            return liU8COReponse;
        }

        public LiU8COReponseModel UnAudit()
        {
            IXMLDOMDocument2 domHead = new MSXML2.DOMDocumentClass();
            IXMLDOMDocument2 domBody = new MSXML2.DOMDocumentClass();

            liU8COReponse = LiU8COReponseModel.getInstance();
            PUCo.GetVoucherData(ref domHead, ref domBody, "", vouchId);
            liU8COReponse.resultContent = PUCo.CancelconfirmPO(domHead,  domBody);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            liU8COReponse.bDeleteSuccess = liU8COReponse.bSuccess;
            return liU8COReponse;
        }

        public LiU8COReponseModel Delete()
        {
            IXMLDOMDocument2 domHead = new MSXML2.DOMDocumentClass();
            IXMLDOMDocument2 domBody = new MSXML2.DOMDocumentClass();

            liU8COReponse = LiU8COReponseModel.getInstance();
            PUCo.GetVoucherData(ref domHead, ref domBody, "", vouchId);
            liU8COReponse.resultContent = PUCo.Delete(domHead, domBody,ref _CurDom);
            liU8COReponse.bSuccess = string.IsNullOrEmpty(liU8COReponse.resultContent);
            return liU8COReponse;
        }
        public void SetApiContext(string paramName, object paramValue )
        {
            ModelUtil.setValue<LiU8PUCO>(paramName, paramValue, this);
        }
        public VoucherCO_PU.vouchertype getVouchType(string vouchType)
        {

            switch (vouchType)
            {
                case "0":
                    vouchertype = VoucherCO_PU.vouchertype.采购请购单;
                    break;
                case "1":
                    vouchertype = VoucherCO_PU.vouchertype.采购订单;
                    break;
                case "2":
                    vouchertype = VoucherCO_PU.vouchertype.采购到货单;
                    break;
                case "3":
                    vouchertype = VoucherCO_PU.vouchertype.采购入库单;
                    break;
                case "4":
                    vouchertype = VoucherCO_PU.vouchertype.采购发票;
                    break;
                case "5":
                    vouchertype = VoucherCO_PU.vouchertype.采购结算单;
                    break;
                case "6":
                    vouchertype = VoucherCO_PU.vouchertype.PU_STSettle;
                    break;
                case "7":
                    vouchertype = VoucherCO_PU.vouchertype.PU_ManSettle;
                    break;
                case "8":
                    vouchertype = VoucherCO_PU.vouchertype.VMIUsedVouch;
                    break;
                case "9":
                    vouchertype = VoucherCO_PU.vouchertype.供应商资格审批;
                    break;
                case "10":
                    vouchertype = VoucherCO_PU.vouchertype.供应商供货审批;
                    break;
                case "11":
                    vouchertype = VoucherCO_PU.vouchertype.PU_FYSettle;
                    break;
                case "12":
                    vouchertype = VoucherCO_PU.vouchertype.PU_AutoSettle;
                    break;
                case "13":
                    vouchertype = VoucherCO_PU.vouchertype.OM_ManSettle;
                    break;
                case "14":
                    vouchertype = VoucherCO_PU.vouchertype.OM_FYSettle;
                    break;
                case "15":
                    vouchertype = VoucherCO_PU.vouchertype.供应商存货调价单;
                    break;
            }
            return vouchertype;
        }
    }
}
