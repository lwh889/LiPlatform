using LiCommon.Util;
using LiU8CO.Model;
using LiU8CO.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO
{
    public class U8COContext
    {
        public static void Text()
        {
            string json = "{\"sSubId\":\"ST\",\"sAccID\":\"999\",\"sYear\":\"2015\",\"sUserID\":\"demo\",\"sPassword\":\"DEMO\",\"sDate\":\"2015-01-21\",\"sVouchType\":\"01\",\"vouchData\":[{\"cvouchtype\":\"01\",\"cbustype\":\"普通采购\",\"csource\":\"库存\",\"cwhcode\":\"50\",\"ddate\":\"2015-01-18\",\"caddcode\":\"0401\",\"cdepcode\":\"0401\",\"cpersoncode\":\"00043\",\"cvencode\":\"01002\",\"cmaker\":\"demo\",\"itaxrate\":\"17\",\"iexchrate\":\"1\",\"cexch_name\":\"人民币\",\"datas\":[{\"cinvcode\":\"0340\",\"iquantity\":\"100\",\"iunitcost\":\"75\",\"iprice\":\"7500\",\"iaprice\":\"7500\",\"facost\":\"75\",\"ioritaxcost\":\"87.75\",\"ioricost\":\"75\",\"iorimoney\":\"7500\",\"ioritaxprice\":\"1275\",\"iorisum\":\"8775\",\"itaxprice\":\"1275\",\"isum\":\"8775\",\"itaxrate\":\"17\",\"irowno\":\"1\"},{\"cinvcode\":\"0340\",\"iquantity\":\"100\",\"iunitcost\":\"75\",\"iprice\":\"7500\",\"iaprice\":\"7500\",\"facost\":\"75\",\"ioritaxcost\":\"87.75\",\"ioricost\":\"75\",\"iorimoney\":\"7500\",\"ioritaxprice\":\"1275\",\"iorisum\":\"8775\",\"itaxprice\":\"1275\",\"isum\":\"8775\",\"itaxrate\":\"17\",\"irowno\":\"2\"}]}]}";
            Dictionary<string, object> dict = JsonUtil.GetDictionary(json);

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
                return;
            }
            LiU8STCO liU8STCO = new LiU8STCO();
            liU8STCO.Init(Convert.ToString(dict["sSubId"]), Convert.ToString(dict["sVouchType"]),u8Login);
            liU8STCO.InitCO();
            liU8STCO.InitDom(2);
            LiU8COReponseModel liU8COReponse = liU8STCO.Insert();
        }
    }
}
