using LiCommon.Util;
using LiU8CO.Model;
using LiU8CO.Util;
using MSXML2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Service
{
    public abstract class ALiU8CO
    {
        public ALiU8CO()
        {
            rs = new ADODB.RecordsetClass();
            conn = new ADODB.ConnectionClass();
            oleDA = new OleDbDataAdapter();

            liGetVouchData = new LiGetVouchData();

            errMsg = string.Empty;
            vouchId = string.Empty;
        }
        public void Init(string voucherClassify, string voucherType, string sSubId, string sAccID, string sYear, string sUserID, string sPassword, string sDate)
        {
            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                throw new Exception("登陆失败，原因：" + u8Login.ShareString);
            }

            Init(voucherClassify, voucherType, u8Login);
        }

        /// <summary>
        /// 初化
        /// </summary>
        public void Init(string voucherClassify, string voucherType, U8Login.clsLogin u8Login)
        {
            liGetVouchData.Init(u8Login);

            this.voucherClassify = voucherClassify;
            this.vouchType = voucherType;
            this.u8Login = u8Login;

            strConn = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn.Open(strConn, "sa", u8Login.SysPassword, 0);


            //获取单据信息
            strSql = string.Format("select * from LiU8COVoucher where voucherClassify = '{0}' and voucherType = '{1}'", voucherClassify, voucherType);

            rs.Open(strSql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            DataTable dt = new DataTable();
            oleDA.Fill(dt, rs);
            rs.Close();
            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new Exception("获取LiU8COVoucher失败!");
            }
            liU8COVoucher = ModelConvertUtil.DataTableToModel<LiU8COVoucherModel>(dt);

            strSql = string.Format("select * from LiU8COField where fid = '{0}' ", liU8COVoucher.id);
            rs.Open(strSql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            dt = new DataTable();
            oleDA.Fill(dt, rs);
            rs.Close();
            if (dt == null || dt.Rows.Count <= 0)
            {
                throw new Exception("获取LiU8COFieldModel失败!");
            }

            liU8COVoucher.liU8COFields = ModelConvertUtil.DataTableToList<LiU8COFieldModel>(dt);

        }
        /// <summary>
        /// 设置表头值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public void setDomHeadValue(string fieldName, object fieldValue)
        {
            eleHead.setAttribute(fieldName,fieldValue); //主关键字段，int类型
        }

        public void setDomBodyRow( int iRow)
        {
            IXMLDOMNodeList ndbodylist = domBody.selectNodes("//rs:data/z:row");
            eleBody = ndbodylist[iRow] as IXMLDOMElement;
        }

        public void setDomBodyRow(IXMLDOMElement eleBody)
        {
            this.eleBody = eleBody;
        }
        /// <summary>
        /// 设置表体值
        /// </summary>
        /// <param name="iRow">行号 ，从0开始</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public void setDomBodyValue(string fieldName, object fieldValue)
        {
            eleBody.setAttribute(fieldName, fieldValue); //主关键字段，int类型
        }
        /// <summary>
        /// 设置表体值
        /// </summary>
        /// <param name="iRow">行号 ，从0开始</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        public void setDomBodyValue(int iRow, string fieldName, object fieldValue)
        {
            setDomBodyRow(iRow);
            eleBody.setAttribute(fieldName, fieldValue); //主关键字段，int类型
        }

        public string getTimestamp(string vouchId)
        {
            if (!string.IsNullOrEmpty(this.liU8COVoucher.timeStampSql))
            {
                string strConn = this.u8Login.UFDataConnstringForNet;
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = this.liU8COVoucher.timeStampSql;
                    cmd.CommandTimeout = 3600;
                    SqlParameter sp = new SqlParameter("@vouchId", vouchId);
                    cmd.Parameters.Add(sp);
                    String timeStamp = (String)cmd.ExecuteScalar();
                    return timeStamp;
                }
            }
            else
            {
                return null;
            }
        }

        public void SetDefaultValue()
        {
            List<LiU8COFieldModel> liU8COHeadFields = liU8COVoucher.liU8COFields.Where(m => m.fieldEntityType == "Head" && m.fieldbDefault == true).ToList();

            IXMLDOMNodeList ndheadlist = domBody.selectNodes("//rs:data/z:row");
            foreach (IXMLDOMElement head in ndheadlist)
            {
                foreach (LiU8COFieldModel liU8COField in liU8COHeadFields)
                {
                    setDomHeadValue(liU8COField.fieldName, liU8COField.fieldDefaultValue);
                }
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

        public void setDomHeadByDataBase(MSXML2.IXMLDOMDocument2 domHead)
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

        }

        public void setDomHead(MSXML2.IXMLDOMDocument2 domHead)
        {

            ////表头
            //rs.Open(liU8COVoucher.domHeadSql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            //rs.Save(domHead, ADODB.PersistFormatEnum.adPersistXML);
            //rs.Close();
            //清除原有数据
            foreach (IXMLDOMNode en in domHead.selectNodes("//z:row"))
            {
                domHead.selectSingleNode("//rs:data").removeChild(en);
            }
            //增加
            DomUtil.FormatDom(ref domHead, "A");
            eleHead = domHead.selectSingleNode("//z:row") as IXMLDOMElement;

        }
        public void setDomHeadTest(MSXML2.IXMLDOMDocument2 domHead)
        {

            //表头
            rs.Open(liU8COVoucher.domHeadSql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            rs.Save(domHead, ADODB.PersistFormatEnum.adPersistXML);
            rs.Close();
            //增加
            DomUtil.FormatDom(ref domHead, "A");

        }
        public void setDomBody(MSXML2.IXMLDOMDocument2 domBody, int row)
        {
            //清除原有数据
            foreach (IXMLDOMNode en in domBody.selectNodes("//z:row"))
            {
                domBody.selectSingleNode("//rs:data").removeChild(en);
            }
            //增加
            DomUtil.FormatDom(ref domBody, "A");
            MSXML2.DOMDocument domBodyCopy = (MSXML2.DOMDocument)domBody.cloneNode(true);
            foreach (IXMLDOMNode en in domBody.selectNodes("//z:row"))
            {
                domBody.selectSingleNode("//rs:data").removeChild(en);
            }
            IXMLDOMNode ele = domBodyCopy.selectSingleNode("//z:row");
            for (int i = 0; i < row; i++)
            {
                domBody.selectSingleNode("//rs:data").appendChild(ele.cloneNode(true));
            }
        }

        public void setDomBodyByDataBase(MSXML2.IXMLDOMDocument2 domBody, int row)
        {

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
            DomUtil.FormatDom(ref domBody, "A");
            MSXML2.DOMDocument domBodyCopy = (MSXML2.DOMDocument)domBody.cloneNode(true);
            foreach (IXMLDOMNode en in domBody.selectNodes("//z:row"))
            {
                domBody.selectSingleNode("//rs:data").removeChild(en);
            }
            IXMLDOMNode ele = domBodyCopy.selectSingleNode("//z:row");
            for (int i = 0; i < row; i++)
            {
                domBody.selectSingleNode("//rs:data").appendChild(ele.cloneNode(true));
            }
        }
        public void setDomBodyTest(MSXML2.IXMLDOMDocument2 domBody, int row)
        {

            //表体
            rs.Open(liU8COVoucher.domBodySql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);
            rs.Save(domBody, ADODB.PersistFormatEnum.adPersistXML);
            rs.Close();
            //增加
            DomUtil.FormatDom(ref domBody, "A");
        }
        public void SetVouchData(Dictionary<string, object> datas, string bodyEntityName = "datas")
        {
            foreach (KeyValuePair<string, object> keyValues in datas)
            {
                if (keyValues.Key == bodyEntityName)
                {
                    List<Dictionary<string, object>> bodyDatas = null;
                    if (keyValues.Value != null && keyValues.Value.GetType().Name == "JArray")
                    {
                        bodyDatas = JsonUtil.GetDictionaryToList(Convert.ToString(keyValues.Value));
                    }
                    else
                    {
                        bodyDatas = keyValues.Value as List<Dictionary<string, object>>;
                    }

                    int iRow = 0;
                    foreach (Dictionary<string, object> bodyData in bodyDatas)
                    {
                        setDomBodyRow(iRow++);
                        foreach (KeyValuePair<string, object> bodyKeyValues in bodyData)
                        {
                            setDomBodyValue(bodyKeyValues.Key, bodyKeyValues.Value);
                        }
                    }
                }
                else
                {
                    setDomHeadValue(keyValues.Key, keyValues.Value);
                }
            }
        }

        public void GetVouchDataByID(LiU8COReponseModel liU8COReponse)
        {

            LiU8ApiGetDataModel liU8ApiGetData = LiU8ApiGetDataModel.convertLiU8ApiGetDataModel(liU8ApiInfo);
            if (liU8ApiGetData != null)
            {
                liU8ApiGetData.sOrderByString = string.Format(" {0} DESC ", liU8COVoucher.headKeyFieldName);
                liU8ApiGetData.sWhereString = string.Format(" and {0} = '{1}'", liU8COVoucher.headKeyFieldName, liU8COReponse.vouchID);
                liU8ApiGetData.sCardNumber = liU8COVoucher.cardNumber;

                try
                {
                    DataTable dt = liGetVouchData.getU8VouchList(liU8ApiGetData);
                    liU8COReponse.vouchDatas = DataTableUtil.getDictionaryToListByDataTable(dt);
                }
                catch (Exception ex)
                {
                    liU8COReponse.resultContent = ex.Message;
                }

            }
        }
        public void SetVouchID(string vouchID)
        {
            this.vouchId = vouchID;
        }

        ~ALiU8CO(){
            if(conn != null)
            {
                conn.Close();
                conn = null;
            }
        }

        public string strSql;
        public string strConn;

        public string accountNo;
        public string voucherClassify;
        public string vouchType;

        /// <summary>
        /// U8Loginn
        /// </summary>
        public U8Login.clsLogin u8Login;

        /// <summary>
        /// 连接数据库
        /// </summary>
        public ADODB.Connection conn;
        public ADODB.Recordset rs;
        public OleDbDataAdapter oleDA;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMsg;

        /// <summary>
        /// 单据ID
        /// </summary>
        public string vouchId;
        /// <summary>
        /// 单据ID
        /// </summary>
        public object vouchIdObject;

        /// <summary>
        /// 表头DOM
        /// </summary>
        public MSXML2.IXMLDOMDocument2 domHead;

        public IXMLDOMElement eleHead;
        /// <summary>
        /// 表体DOM
        /// </summary>
        public MSXML2.IXMLDOMDocument2 domBody;
        public IXMLDOMElement eleBody;

        /// <summary>
        /// 
        /// </summary>
        public MSXML2.IXMLDOMDocument2 domMsg;

        /// <summary>
        /// 回复
        /// </summary>
        public LiU8COReponseModel liU8COReponse;

        /// <summary>
        /// 单据信息
        /// </summary>
        public LiU8COVoucherModel liU8COVoucher;


        private LiGetVouchData _liGetVouchData;

        /// <summary>
        /// 
        /// </summary>
        public LiGetVouchData liGetVouchData { set { _liGetVouchData = value; } get { return _liGetVouchData; } }

        private object _liU8ApiInfo;
        /// <summary>
        /// 操作U8CO信息模型
        /// </summary>
        public object liU8ApiInfo { set { _liU8ApiInfo = value; } get { return _liU8ApiInfo; } }

    }
}
