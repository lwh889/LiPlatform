using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiLog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LogUtil.LoggerName = "LogDefaultName";
                LogUtil.UserID = "123456";
                LogUtil.UserName = "J";
                LogUtil.Fatal("test1");
                LogUtil.Fatal("test2", new Exception("异常信息"));

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
