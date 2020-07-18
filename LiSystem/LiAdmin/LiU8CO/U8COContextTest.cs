using LiCommon.Util;
using LiU8CO.Model;
using LiU8CO.Service;
using LiU8CO.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO
{
    public class U8COContextTest
    {

        public static void Text32Delete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"32\",\"vouchIds\":[\"1000000500\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Delete();
            }
        }
        public static void Text32UnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"32\",\"vouchIds\":[\"1000000500\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.UnAudit();
            }
        }
        public static void Text32Audit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"32\",\"vouchIds\":[\"1000000500\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Audit();
            }
        }
        public static void Text32New()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"32\",\"vouchData\":[{\"id\":\"\",\"cbustype\":\"委托代销\",\"cbuscode\":\"0000000003\",\"bisstqc\":\"0\",\"csource\":\"委托代销\",\"cwhcode\":\"30\",\"ddate\":\"2015-01-12\",\"ccode\":\"0000000051\",\"crdcode\":\"22\",\"cdepcode\":\"0301\",\"cpersoncode\":\"00024\",\"cstcode\":\"01\",\"ccuscode\":\"00000002\",\"cdlcode\":\"1000000103\",\"cmaker\":\"demo\",\"cusiid\":\"00000002\",\"cinvoicecompany\":\"00000002\",\"cfactorycode\":\"001\",\"editprop\":\"A\",\"datas\":[{\"id\":\"\",\"cinvcode\":\"01019002065\",\"inquantity\":\"1\",\"iquantity\":\"1\",\"autoid\":\"\",\"idlsid\":\"1000000165\",\"cbdlcode\":\"0000000003\",\"irowno\":\"1\",\"bneedbill\":\"True\",\"editprop\":\"A\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8STCO.InitDom(bodyDatas.Count);
                liU8STCO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
            }
        }

        public static void Text08Delete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"08\",\"vouchIds\":[\"1000000492\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Delete();
            }
        }
        public static void Text08UnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"08\",\"vouchIds\":[\"1000000492\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.UnAudit();
            }
        }
        public static void Text08Audit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"08\",\"vouchIds\":[\"1000000492\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Audit();
            }
        }
        public static void Text08New()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"08\",\"vouchData\":[{\"id\":\"\",\"brdflag\":\"1\",\"cvouchtype\":\"08\",\"csysbarcode\":\"||st08|0000000025\",\"cbustype\":\"其他入库\",\"cwhcode\":\"30\",\"ddate\":\"2015-01-09\",\"ccode\":\"0000000025\",\"cmaker\":\"demo\",\"vt_id\":\"67\",\"bisstqc\":\"0\",\"csource\":\"库存\",\"bpufirst\":\"0\",\"biafirst\":\"0\",\"bislsquery\":\"0\",\"ireturncount\":\"0\",\"iverifystate\":\"0\",\"iswfcontrolled\":\"0\",\"bomfirst\":\"0\",\"iPrintCount\":\"0\",\"cfactorycode\":\"001\",\"editprop\":\"A\",\"datas\":[{\"id\":\"\",\"cinvcode\":\"01019002068\",\"iquantity\":\"20.0000000000\",\"iunitcost\":\"25.0000000000\",\"iprice\":\"500\",\"autoid\":\"\",\"isotype\":\"0\",\"bcosting\":\"True\",\"bvmiused\":\"0\",\"csrpolicy\":\"PE\",\"iexpiratdatecalcu\":\"0\",\"irowno\":\"1\",\"avaInQuantity\":\"0\",\"avaInNum\":\"0\",\"editprop\":\"A\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8STCO.InitDom(bodyDatas.Count);
                liU8STCO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
            }
        }
        public static void Text09Delete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"09\",\"vouchIds\":[\"1000000487\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Delete();
            }
        }
        public static void Text09UnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"09\",\"vouchIds\":[\"1000000487\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.UnAudit();
            }
        }
        public static void Text09Audit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"09\",\"vouchIds\":[\"1000000487\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Audit();
            }
        }
        public static void Text09New()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"09\",\"vouchData\":[{\"id\":\"\",\"cbustype\":\"其他出库\",\"cwhcode\":\"30\",\"ddate\":\"2015-01-09\",\"ccode\":\"0000000016\",\"cmaker\":\"demo\",\"cfactorycode\":\"001\",\"editprop\":\"A\",\"datas\":[{\"id\":\"\",\"cinvcode\":\"01019002065\",\"iquantity\":\"11.0000000000\",\"iunitcost\":\"9.0000000000\",\"iprice\":\"99\",\"irowno\":\"1\",\"editprop\":\"A\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8STCO.InitDom(bodyDatas.Count);
                liU8STCO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
            }
        }
        public static void Text10Delete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"10\",\"vouchIds\":[\"1000000480\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Delete();
            }
        }
        public static void Text10UnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"10\",\"vouchIds\":[\"1000000480\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.UnAudit();
            }
        }
        public static void Text10Audit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"10\",\"vouchIds\":[\"1000000480\"]}";
            LiU8ApiIdModel u8ApiId = JsonUtil.GetEntity<LiU8ApiIdModel>(json);
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Audit();
            }
        }
        public static void Text10New()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"10\",\"vouchData\":[{\"id\":\"\",\"cwhcode\":\"04\",\"ddate\":\"2015-01-06\",\"ccode\":\"0000000016\",\"crdcode\":\"12\",\"cdepcode\":\"0401\",\"cmaker\":\"demo\",\"cbustype\":\"成品入库\",\"cfactorycode\":\"001\",\"editprop\":\"A\",\"datas\":[{\"autoid\":\"\",\"id\":\"\",\"cinvcode\":\"F010-80100001\",\"iquantity\":\"2.0000000000\",\"brelated\":\"0\",\"isotype\":\"0\",\"iordertype\":\"0\",\"bcosting\":\"True\",\"bvmiused\":\"0\",\"iuninvsncount\":\"2.0000000000\",\"iexpiratdatecalcu\":\"0\",\"irowno\":\"1\",\"bmergecheck\":\"0\",\"avaInQuantity\":\"0\",\"avaInNum\":\"0\",\"editprop\":\"A\"}]}]}";
            LiU8ApiDataModel u8ApiId = JsonUtil.GetEntity<LiU8ApiDataModel>(json);
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8STCO.InitDom(bodyDatas.Count);
                liU8STCO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
            }
        }
        public static void Text11New()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"11\",\"vouchData\":[{\"id\":\"\",\"cvouchtype\":\"11\",\"cwhcode\":\"02\",\"ddate\":\"2015-01-06\",\"ccode\":\"0000000058\",\"crdcode\":\"21\",\"cdepcode\":\"0501\",\"cmaker\":\"demo\",\"csource\":\"库存\",\"cbustype\":\"领料\",\"cfactorycode\":\"002\",\"editprop\":\"A\",\"datas\":[{\"autoid\":\"\",\"id\":\"\",\"cinvcode\":\"01019002066\",\"iquantity\":\"2.0000000000\",\"irowno\":\"1\",\"editprop\":\"A\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8STCO.InitDom(bodyDatas.Count);
                liU8STCO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
            }
        }

        public static U8Login.clsLogin getLogin(Dictionary<string, object> dict)
        {

            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            String sSubId = Convert.ToString(dict["sSubId"]);
            String sAccID = Convert.ToString(dict["sAccID"]);
            String sYear = Convert.ToString(dict["sYear"]);
            String sUserID = Convert.ToString(dict["sUserID"]);
            String sPassword = Convert.ToString(dict["sPassword"]);
            String sDate = Convert.ToString(dict["sDate"]);

            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return null;
            }

            return u8Login;
        }
        public static void PUNew()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"PU\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"1\",\"bPositive\":true,\"sBillType\":\"\",\"sBusType\":\"普通采购\",\"VoucherState\":2,\"vouchData\":[{\"ivtid\":\"8173\",\"poid\":\"\",\"cbustype\":\"普通采购\",\"dpodate\":\"2015-01-09\",\"cvencode\":\"01005\",\"cdepcode\":\"0401\",\"cpersoncode\":\"00043\",\"itaxrate\":\"17\",\"cexch_name\":\"人民币\",\"cmaker\":\"demo\",\"bstorageorder\":\"True\",\"editprop\":\"A\",\"datas\":[{\"id\":\"\",\"poid\":\"\",\"cinvcode\":\"SJK001\",\"iquantity\":\"1080\",\"darrivedate\":\"2015-01-21\",\"ipertaxrate\":\"17.0\",\"ivouchrowno\":\"1\",\"cfactorycode\":\"001\",\"cfactoryname\":\"工厂一\",\"editprop\":\"A\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);

            ILiU8CO liU8PUCO = new LiU8PUCO();
            liU8PUCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);

            liU8PUCO.SetApiContext("bPositive", Convert.ToBoolean(dict["bPositive"]));
            liU8PUCO.SetApiContext("sBillType", Convert.ToString(dict["sBillType"]));
            liU8PUCO.SetApiContext("sBusType", Convert.ToString(dict["sBusType"]));
            liU8PUCO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8PUCO.InitDom(bodyDatas.Count);
                liU8PUCO.SetVouchData(vouchData);
                liU8PUCO.SetApiContext("VoucherState", Convert.ToInt16(dict["VoucherState"]));

                LiU8COReponseModel liU8COReponse = liU8PUCO.Insert();
            }
        }
        public static void SONew()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"SA\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"12\",\"vouchData\":[{\"cstcode\":\"01\",\"ddate\":\"2015-01-21\",\"ccuscode\":\"00000002\",\"cdepcode\":\"0302\",\"cpersoncode\":\"00023\",\"itaxrate\":\"17\",\"cmaker\":\"demo\",\"ccreditcuscode\":\"00000002\",\"cinvoicecompany\":\"00000002\",\"editprop\":\"A\",\"datas\":[{\"autoid\":\"\",\"id\":\"\",\"cinvcode\":\"01019002065\",\"iquantity\":\"3\",\"iquotedprice\":\"100\",\"iunitprice\":\"85.47\",\"iinvsprice\":\"650\",\"iinvncost\":\"550\",\"imoney\":\"170.94\",\"itax\":\"29.06\",\"isum\":\"200\",\"inatunitprice\":\"85.47\",\"inatmoney\":\"170.94\",\"inattax\":\"29.06\",\"inatsum\":\"200\",\"cbsysbarcode\":\"1\",\"dpredate\":\"2015-02-10\",\"dpremodate\":\"2015-02-10\",\"itaxunitprice\":\"100\",\"irowno\":\"1\",\"editprop\":\"A\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8SACO = new LiU8SACO();
            liU8SACO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8SACO.InitCO();
            foreach (Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8SACO.InitDom(bodyDatas.Count);
                liU8SACO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8SACO.Insert();
            }
        }
        public static void TextDelete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"01\",\"vouchIds\":[\"1000000466\",\"1000000465\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Delete();
            }
        }
        public static void PUDelete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"PU\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"bPositive\":true,\"sBillType\":\"\",\"sBusType\":\"普通采购\",\"sVouchType\":\"1\",\"vouchIds\":[\"1000000051\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8PUCO = new LiU8PUCO();
            liU8PUCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8PUCO.SetApiContext("bPositive", Convert.ToBoolean(dict["bPositive"]));
            liU8PUCO.SetApiContext("sBillType", Convert.ToString(dict["sBillType"]));
            liU8PUCO.SetApiContext("sBusType", Convert.ToString(dict["sBusType"]));
            liU8PUCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8PUCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8PUCO.Delete();
            }
        }
        public static void SODelete()
        {
            string json = "{\"sOperationType\":\"Delete\",\"sSubId\":\"SA\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"12\",\"vouchIds\":[\"1000000313\",\"1000000312\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login  = getLogin(dict);
            ILiU8CO liU8SACO = new LiU8SACO();
            liU8SACO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8SACO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8SACO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8SACO.Delete();
            }
        }
        public static void TextUnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"01\",\"vouchIds\":[\"1000000466\",\"1000000465\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.UnAudit();
            }
        }
        public static void TextAudit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"01\",\"vouchIds\":[\"1000000466\",\"1000000465\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8STCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8STCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8STCO.Audit();
            }
        }
        public static void PUUnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"PU\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"bPositive\":true,\"sBillType\":\"\",\"sBusType\":\"普通采购\",\"sVouchType\":\"1\",\"vouchIds\":[\"1000000053\",\"1000000052\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8PUCO = new LiU8PUCO();
            liU8PUCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8PUCO.SetApiContext("bPositive", Convert.ToBoolean(dict["bPositive"]));
            liU8PUCO.SetApiContext("sBillType", Convert.ToString(dict["sBillType"]));
            liU8PUCO.SetApiContext("sBusType", Convert.ToString(dict["sBusType"]));
            liU8PUCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8PUCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8PUCO.UnAudit();
            }
        }
        public static void SOUnAudit()
        {
            string json = "{\"sOperationType\":\"UnAudit\",\"sSubId\":\"SA\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"12\",\"vouchIds\":[\"1000000313\",\"1000000312\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8SACO = new LiU8SACO();
            liU8SACO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8SACO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8SACO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8SACO.UnAudit();
            }
        }
        public static void PUAudit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"PU\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"bPositive\":true,\"sBillType\":\"\",\"sBusType\":\"普通采购\",\"sVouchType\":\"1\",\"vouchIds\":[\"1000000053\",\"1000000052\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8PUCO = new LiU8PUCO();
            liU8PUCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8PUCO.SetApiContext("bPositive", Convert.ToBoolean(dict["bPositive"]));
            liU8PUCO.SetApiContext("sBillType", Convert.ToString(dict["sBillType"]));
            liU8PUCO.SetApiContext("sBusType", Convert.ToString(dict["sBusType"]));
            liU8PUCO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8PUCO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8PUCO.Audit();
            }
        }
        public static void SOAudit()
        {
            string json = "{\"sOperationType\":\"Audit\",\"sSubId\":\"SA\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"12\",\"vouchIds\":[\"1000000313\",\"1000000312\"]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<object> vouchDatas = dict["vouchIds"] as List<object>;

            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8SACO = new LiU8SACO();
            liU8SACO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]), u8Login);
            liU8SACO.InitCO();
            foreach (object vouchData in vouchDatas)
            {
                liU8SACO.SetVouchID(Convert.ToString(vouchData));
                LiU8COReponseModel liU8COReponse = liU8SACO.Audit();
            }
        }
        public static void TextNew()
        {
            string json = "{\"sOperationType\":\"NEW\",\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"01\",\"vouchData\":[{\"cvouchtype\":\"01\",\"cbustype\":\"普通采购\",\"csource\":\"库存\",\"cwhcode\":\"50\",\"ddate\":\"2015-01-18\",\"caddcode\":\"0401\",\"cdepcode\":\"0401\",\"cpersoncode\":\"00043\",\"cvencode\":\"01002\",\"cmaker\":\"demo\",\"itaxrate\":\"17\",\"iexchrate\":\"1\",\"cexch_name\":\"人民币\",\"datas\":[{\"cinvcode\":\"0340\",\"iquantity\":\"100\",\"iunitcost\":\"75\",\"iprice\":\"7500\",\"iaprice\":\"7500\",\"facost\":\"75\",\"ioritaxcost\":\"87.75\",\"ioricost\":\"75\",\"iorimoney\":\"7500\",\"ioritaxprice\":\"1275\",\"iorisum\":\"8775\",\"itaxprice\":\"1275\",\"isum\":\"8775\",\"itaxrate\":\"17\",\"irowno\":\"1\"},{\"cinvcode\":\"0340\",\"iquantity\":\"100\",\"iunitcost\":\"75\",\"iprice\":\"7500\",\"iaprice\":\"7500\",\"facost\":\"75\",\"ioritaxcost\":\"87.75\",\"ioricost\":\"75\",\"iorimoney\":\"7500\",\"ioritaxprice\":\"1275\",\"iorisum\":\"8775\",\"itaxprice\":\"1275\",\"isum\":\"8775\",\"itaxrate\":\"17\",\"irowno\":\"2\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);
            List<Dictionary<string, object>> vouchDatas = dict["vouchData"] as List<Dictionary<string, object>>;
            
            U8Login.clsLogin u8Login = getLogin(dict);
            ILiU8CO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]),u8Login);
            liU8STCO.InitCO();
            foreach(Dictionary<string, object> vouchData in vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8STCO.InitDom(bodyDatas.Count);
                liU8STCO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
            }
        }
    }
}
