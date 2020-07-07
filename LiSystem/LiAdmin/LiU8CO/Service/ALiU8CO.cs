using LiCommon.Util;
using LiU8CO.Model;
using MSXML2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

            errMsg = string.Empty;
            vouchId = string.Empty;
        }

        /// <summary>
        /// 初化
        /// </summary>
        public void Init(string voucherClassify, string voucherType, U8Login.clsLogin u8Login)
        {
            this.voucherClassify = voucherClassify;
            this.voucherType = voucherType;
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
        public string voucherType;

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
        /// 表头DOM
        /// </summary>
        public MSXML2.DOMDocument domHead;

        public IXMLDOMElement eleHead;
        /// <summary>
        /// 表体DOM
        /// </summary>
        public MSXML2.DOMDocument domBody;
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
    }
}
