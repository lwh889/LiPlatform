using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using LiCommon.Util;
using LiModel.Basic;
using System.Drawing;

namespace LiManage
{
    static class Program
    {
        

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ImageUtil.loadAllImage();
            //ZGVtbw==
            //string str = EncryptUtil.Base64Encrypt("demo");
            LiForm.LiLoginForm loginForm = new LiForm.LiLoginForm();
            while(DialogResult.Yes == loginForm.ShowDialog()){
                Application.Run(new LiManageForm2());
                return;
            }
        }

    }
}
