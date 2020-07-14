using System;
using System.Collections.Generic;

using System.Text;
using System.Xml;
using System.Data;

using MSXML2;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;
using USCOMMON;
using LiU8CO.Model;
using LiCommon.Util;

namespace LiU8CO
{
    public partial class Form1 : Form
    {
        private MSXML2.DOMDocument headLoaddom;
        private MSXML2.DOMDocument bodyLoaddom;

        private MSXML2.DOMDocument headdom;
        private MSXML2.DOMDocument bodydom;


        private U8Login.clsLogin u8Login;
        private USERPCO.VoucherCO STCo;
        private ADODB.Connection cnn;
        private string errMsg = string.Empty;
        private string vouchId = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {            
            //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
            //如果当前环境中有login对象则可以省去第一步
            u8Login = new U8Login.clsLogin();
            String sSubId = "AS";
            String sAccID = "999";
            String sYear = "2015";
            String sUserID = "demo";
            String sPassword = "DEMO";
            String sDate = "2015-01-21";
            String sServer = "localhost";
            String sSerial = "";
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate ))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return;
            }


            ADODB.Connection conn = new ADODB.ConnectionClass();
            ADODB.Recordset rs = new ADODB.RecordsetClass();

            STCo = new USERPCO.VoucherCO();

            STCo.IniLogin(u8Login, errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }


            MSXML2.DOMDocument domhead = new MSXML2.DOMDocumentClass();
            string strConn = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn.Open(strConn, "sa", u8Login.SysPassword, 0);
            string sql = "Select top 1 zpurRkdHead.*,N'' as editprop  From zpurRkdHead order by id desc  where id=1000000450";
            rs.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs.Save(domhead, ADODB.PersistFormatEnum.adPersistXML);

            foreach (IXMLDOMNode en in domhead.selectNodes("//z:row"))
            {
                domhead.selectSingleNode("//rs:data").removeChild(en);
            }

            FormatDom(ref domhead, "A");
            domhead.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";

            MSXML2.DOMDocument domBody = new MSXML2.DOMDocumentClass();
            //domBody.RowCount = 1;

            ADODB.Connection conn1 = new ADODB.ConnectionClass();
            ADODB.Recordset rs1 = new ADODB.RecordsetClass();
            string strConn1 = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn1.Open(strConn, "sa", u8Login.SysPassword, 0);
            sql = "Select top 1 zpurRkdTail.*,convert(float,0) as avaInQuantity ,convert(float,0) as avaInNum ,convert(nvarchar(40),'') as taskguid,'' as editprop  From zpurRkdTail order by id desc  where id=1000000450";
            rs1.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs1.Save(domBody, ADODB.PersistFormatEnum.adPersistXML);

            foreach (IXMLDOMNode en in domBody.selectNodes("//z:row"))
            {
                domBody.selectSingleNode("//rs:data").removeChild(en);
            }


            FormatDom(ref domBody, "");
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("autoid").nodeValue = "";

            setHeadDomST01(domhead);
            setbodyDomST01(domBody);

            MSXML2.IXMLDOMDocument2 domPos = new MSXML2.DOMDocumentClass();
            MSXML2.IXMLDOMDocument2 domMsg = new MSXML2.DOMDocumentClass();
            bool bret = STCo.Insert("01", domhead, domBody, domPos,ref errMsg, null, ref vouchId);


            //headLoaddom = new MSXML2.DOMDocumentClass();
            //bodyLoaddom = new MSXML2.DOMDocumentClass();
            //domPos = new MSXML2.DOMDocumentClass();
            //STCo.Load("01", " id ='1000000043'",ref headLoaddom,ref bodyLoaddom,ref domPos,ref errMsg);
            //bool bret = STCo.Insert("01", headLoaddom, bodyLoaddom, domPos, errMsg);

            //STCo.GetDefaultVoucherDom("01", "24", headdom, bodydom, domPos, errMsg);
            //STCo.HeadDefValue(VouchType.PurchaseIn, headdom, errMsg);
            //STCo.BodyDefValue(VouchType.PurchaseIn, headdom, errMsg);
            //bool bret = STCo.Insert("01", headdom, bodydom, domPos, errMsg);


            if (bret)
            {
                MessageBox.Show("采购入库单新增成功");
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }

        public void setHeadDomST01(DOMDocument domHead)
        {

            IXMLDOMElement ele;
            ele = domHead.selectSingleNode("//z:row") as IXMLDOMElement;
            ele.setAttribute("id", "");

            ele.setAttribute("cvouchtype", "01");
            ele.setAttribute("cbustype", "普通采购");
            ele.setAttribute("csource", "库存");
            ele.setAttribute("cwhcode", "50");
            ele.setAttribute("ddate", "2015-01-18");
            ele.setAttribute("caddcode", "0401");
            ele.setAttribute("cdepcode", "0401");
            ele.setAttribute("cpersoncode", "00043");
            ele.setAttribute("cvencode", "01002");
            ele.setAttribute("cmaker", "demo");
            ele.setAttribute("itaxrate", "17");
            ele.setAttribute("iexchrate", "1");
            ele.setAttribute("cexch_name", "人民币");


            //编码需要，否则报错
            ele.setAttribute("ccode", "0000000052");
            ele.setAttribute("idiscounttaxtype", "0");
            ele.setAttribute("vt_id", "27");
            ele.setAttribute("bisstqc", "0");
            ele.setAttribute("bpufirst", "0");
            ele.setAttribute("biafirst", "0");
            ele.setAttribute("bomfirst", "0");
            ele.setAttribute("ireturncount", "0");
            ele.setAttribute("iverifystate", "0");
            ele.setAttribute("iswfcontrolled", "0");
            ele.setAttribute("bredvouch", "0");
            ele.setAttribute("bcredit", "0");
            ele.setAttribute("chinvsn", "");
            ele.setAttribute("iPrintCount", "0");
            ele.setAttribute("bincost", "True");
            ele.setAttribute("brdflag", "1");
            ele.setAttribute("cfactorycode", "001");
            ele.setAttribute("editprop", "A");
        }

        public void setbodyDomST01(DOMDocument dombody)
        {

            IXMLDOMElement ele;
            IXMLDOMElement eleA;

            DOMDocument DOMBodyRow;
            DOMBodyRow = dombody.cloneNode(true) as DOMDocument;
            foreach(IXMLDOMNode en in dombody.selectNodes("//z:row"))
            {
                dombody.selectSingleNode("//rs:data").removeChild(en);
            }


            ele = DOMBodyRow.selectSingleNode("//z:row") as IXMLDOMElement;
            eleA = ele.cloneNode(true) as IXMLDOMElement;


            eleA.setAttribute("autoid", "");
            eleA.setAttribute("id", "");
            eleA.setAttribute("cinvcode", "0340");
            eleA.setAttribute("iquantity", "100");
            eleA.setAttribute("iunitcost", "75");
            eleA.setAttribute("iprice", "7500");
            eleA.setAttribute("iaprice", "7500");
            eleA.setAttribute("facost", "75");
            eleA.setAttribute("ioritaxcost", "87.75");
            eleA.setAttribute("ioricost", "75");
            eleA.setAttribute("iorimoney", "7500");
            eleA.setAttribute("ioritaxprice", "1275");
            eleA.setAttribute("iorisum", "8775");
            eleA.setAttribute("itaxprice", "1275");
            eleA.setAttribute("isum", "8775");
            eleA.setAttribute("itaxrate", "17");
            //eleA.setAttribute("cWhCodeforBack", "50");
            //eleA.setAttribute("cgcgroupcode", "01");
            eleA.setAttribute("irowno", "1");

            eleA.setAttribute("iflag", "0");
            eleA.setAttribute("isnum", "0");
            eleA.setAttribute("isquantity", "0");
            eleA.setAttribute("imoney", "0");
            eleA.setAttribute("cposition", "0200000001");
            eleA.setAttribute("binvtype", "0");
            eleA.setAttribute("btaxcost", "0");
            eleA.setAttribute("imatsettlestate", "0");
            eleA.setAttribute("isotype", "0");
            eleA.setAttribute("iordertype", "0");
            eleA.setAttribute("bcosting", "True");
            eleA.setAttribute("bvmiused", "0");
            eleA.setAttribute("iexpiratdatecalcu", "0");
            eleA.setAttribute("bmergecheck", "0");
            eleA.setAttribute("iproducttype", "0");
            eleA.setAttribute("iposflag", "0");
            eleA.setAttribute("bgift", "0");
            eleA.setAttribute("igcgrouptype", "0");
            eleA.setAttribute("avaInQuantity", "0");
            eleA.setAttribute("avaInNum", "0");

            dombody.selectSingleNode("//rs:data").appendChild(eleA);
        }
        public static void FormatDom(ref MSXML2.DOMDocument SourceDom, string editprop)
        {
            //IXMLDOMElement element;
            //IXMLDOMElement ele_head;
            IXMLDOMElement ele_body;
            //IXMLDOMNode nd;
            //MSXML2.DOMDocument tempnd;
            IXMLDOMNodeList ndheadlist;
            IXMLDOMNodeList ndbodylist;
            //DistDom.loadXML("SourceDom.xml");
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

        private void Button2_Click(object sender, EventArgs e)
        {
            u8Login = new U8Login.clsLogin();
            String sSubId = "AS";
            String sAccID = "999";
            String sYear = "2015";
            String sUserID = "demo";
            String sPassword = "DEMO";
            String sDate = "2015-01-21";
            String sServer = "localhost";
            String sSerial = "";
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return;
            }


            ADODB.Connection conn = new ADODB.ConnectionClass();
            ADODB.Recordset rs = new ADODB.RecordsetClass();

            STCo = new USERPCO.VoucherCO();

            STCo.IniLogin(u8Login, errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }


            MSXML2.DOMDocument domhead = new MSXML2.DOMDocumentClass();
            string strConn = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFDATA_998_2014", u8Login.dbServerName);
            conn.Open(strConn, "sa", u8Login.SysPassword, 0);
            string sql = "select top 1 * from KCSaleOutH where id=1000000401 ";
            rs.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs.Save(domhead, ADODB.PersistFormatEnum.adPersistXML);

            FormatDom(ref domhead, "A");
            domhead.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";

            MSXML2.DOMDocument domBody = new MSXML2.DOMDocumentClass();
            //domBody.RowCount = 1;

            ADODB.Connection conn1 = new ADODB.ConnectionClass();
            ADODB.Recordset rs1 = new ADODB.RecordsetClass();
            string strConn1 = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFDATA_998_2014", u8Login.dbServerName);
            conn1.Open(strConn, "sa", u8Login.SysPassword, 0);
            sql = "select top 1 * from KCSaleOutB where id=1000000401";
            rs1.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs1.Save(domBody, ADODB.PersistFormatEnum.adPersistXML);

            FormatDom(ref domBody, "");
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("autoid").nodeValue = "";

            bool checkFlag = false;
            bool bret = STCo.Insert("32", domhead, domBody, null, errMsg, null, null, null, ref checkFlag);

            if (bret)
            {
                MessageBox.Show("销售出库单新增成功");
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            u8Login = new U8Login.clsLogin();
            String sSubId = "AS";
            String sAccID = "999";
            String sYear = "2015";
            String sUserID = "demo";
            String sPassword = "DEMO";
            String sDate = "2015-01-21";
            String sServer = "localhost";
            String sSerial = "";
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return;
            }


            ADODB.Connection conn = new ADODB.ConnectionClass();
            ADODB.Recordset rs = new ADODB.RecordsetClass();

            STCo = new USERPCO.VoucherCO();

            STCo.IniLogin(u8Login, errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }


            MSXML2.DOMDocument domhead = new MSXML2.DOMDocumentClass();
            string strConn = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn.Open(strConn, "sa", u8Login.SysPassword, 0);
            string sql = "select top 1 * from RecordOutQ where id=1000000438 ";
            rs.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs.Save(domhead, ADODB.PersistFormatEnum.adPersistXML);

            FormatDom(ref domhead, "A");
            domhead.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";

            MSXML2.DOMDocument domBody = new MSXML2.DOMDocumentClass();
            //domBody.RowCount = 1;

            ADODB.Connection conn1 = new ADODB.ConnectionClass();
            ADODB.Recordset rs1 = new ADODB.RecordsetClass();
            string strConn1 = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn1.Open(strConn, "sa", u8Login.SysPassword, 0);
            sql = "select top 1 * from RecordOutSQ where id=1000000438";
            rs1.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs1.Save(domBody, ADODB.PersistFormatEnum.adPersistXML);

            FormatDom(ref domBody, "");
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("autoid").nodeValue = "";

            bool checkFlag = false;
            bool bret = STCo.Insert("11", domhead, domBody, null, errMsg,null, null, null, ref checkFlag);

            if (bret)
            {
                MessageBox.Show("材料出库单新增成功");
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }

        /// <summary>
        /// 可以
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {

            u8Login = new U8Login.clsLogin();
            String sSubId = "AS";
            String sAccID = "999";
            String sYear = "2015";
            String sUserID = "demo";
            String sPassword = "DEMO";
            String sDate = "2015-01-21";
            String sServer = "localhost";
            String sSerial = "";
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return;
            }


            ADODB.Connection conn = new ADODB.ConnectionClass();
            ADODB.Recordset rs = new ADODB.RecordsetClass();

            STCo = new USERPCO.VoucherCO();

            STCo.IniLogin(u8Login, errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }


            MSXML2.DOMDocument domhead = new MSXML2.DOMDocumentClass();
            string strConn = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn.Open(strConn, "sa", u8Login.SysPassword, 0);
            string sql = "select top 1 * from RecordInQ where id=1000000441 ";
            rs.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs.Save(domhead, ADODB.PersistFormatEnum.adPersistXML);

            FormatDom(ref domhead, "A");
            domhead.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";

            MSXML2.DOMDocument domBody = new MSXML2.DOMDocumentClass();
            //domBody.RowCount = 1;

            ADODB.Connection conn1 = new ADODB.ConnectionClass();
            ADODB.Recordset rs1 = new ADODB.RecordsetClass();
            string strConn1 = string.Format("Provider=SQLOLEDB;Initial Catalog={0};Data Source={1};", "UFData_" + u8Login.get_cAcc_Id() + "_" + u8Login.cBeginYear, u8Login.dbServerName);
            conn1.Open(strConn, "sa", u8Login.SysPassword, 0);
            sql = "select top 1 * from RecordInSQ where id=1000000441";
            rs1.Open(sql, conn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic, -1);

            rs1.Save(domBody, ADODB.PersistFormatEnum.adPersistXML);

            FormatDom(ref domBody, "");
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("id").nodeValue = "";
            domBody.selectSingleNode("//rs:data/z:row").attributes.getNamedItem("autoid").nodeValue = "";

            bool bret = STCo.Insert("10", domhead, domBody, null, errMsg);

            if (bret)
            {
                MessageBox.Show("产成品入库单新增成功");
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            U8COContextTest.TextNew();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            U8COContextTest.TextAudit();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            U8COContextTest.TextUnAudit();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            U8COContextTest.TextDelete();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            U8COContextTest.SONew();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            U8COContextTest.PUNew();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            U8COContextTest.SOAudit();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            U8COContextTest.PUAudit();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            U8COContextTest.PUUnAudit();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            U8COContextTest.PUDelete();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            U8COContextTest.SOUnAudit();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            U8COContextTest.SODelete();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text11New();
        }

        private void 新增ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text10New();
        }

        private void 新增ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text09New();
        }

        private void 新增ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text08New();
        }

        private void 新增ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text32New();
        }

        private void 审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text32Audit();
        }

        private void 反审ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text32UnAudit();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text32Delete();
        }

        private void 审核ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text08Audit();
        }

        private void 反审ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text08UnAudit();
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text08Delete();
        }

        private void 审核ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text09Audit();
        }

        private void 反审ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text09UnAudit();
        }

        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text09Delete();
        }

        private void 审核ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text10Audit();
        }

        private void 反审ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text10UnAudit();
        }

        private void 删除ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            U8COContextTest.Text10Delete();
        }

        private void 获取数据ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LiU8ApiGetDataModel liU8ApiGetData = new LiU8ApiGetDataModel();
            liU8ApiGetData.iEnd = 10;
            liU8ApiGetData.iStart = 1;
            liU8ApiGetData.sAccID = "999";
            liU8ApiGetData.sCardNumber = "0301";
            liU8ApiGetData.sDate = "2015-01-10";
            liU8ApiGetData.sPassword = "DEMO";
            liU8ApiGetData.sSubId = "ST";
            liU8ApiGetData.sUserID = "demo";
            liU8ApiGetData.sYear = "2015";
            liU8ApiGetData.sWhereString = "";
            liU8ApiGetData.sSelectFields = "*";
            liU8ApiGetData.sOrderByString = "ID desc";

            U8COContext.getU8VouchListCount(liU8ApiGetData);
        }

        private void 获取数据列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LiU8ApiGetDataModel liU8ApiGetData = new LiU8ApiGetDataModel();
            liU8ApiGetData.iEnd = 1000;
            liU8ApiGetData.iStart = 1;
            liU8ApiGetData.sAccID = "999";
            liU8ApiGetData.sCardNumber = "0301";
            liU8ApiGetData.sDate = "2015-01-10";
            liU8ApiGetData.sPassword = "DEMO";
            liU8ApiGetData.sSubId = "ST";
            liU8ApiGetData.sUserID = "demo";
            liU8ApiGetData.sYear = "2015";
            liU8ApiGetData.sWhereString = "";
            liU8ApiGetData.sSelectFields = " cvouchtype cinvname,id,cinvcode,crdcode,crdname ";
            liU8ApiGetData.sOrderByString = "ID desc";

            string json = JsonUtil.GetJson(liU8ApiGetData);
            U8COContext.getU8VouchList(liU8ApiGetData);
        }
    }
}
