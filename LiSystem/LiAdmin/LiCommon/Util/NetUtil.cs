using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LiCommon.Util
{
    public class NetUtil
    {
        public static string getIPAddress()
        {
            string strLocalIp = string.Empty;
            string strPcName = Dns.GetHostName();
            IPHostEntry iPHostEntry = Dns.GetHostEntry(strPcName);
            foreach (var IPadd in iPHostEntry.AddressList)
            {
                strLocalIp += string.Format("{0};", IPadd.ToString());
            }

            return strLocalIp;
        }

        public static string getHostName()
        {
            return Dns.GetHostName();
        }
    }
}
