using System;
using System.Windows.Forms;
using System.Linq;
using LiCommon.Util;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace LiAdmin
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

            LiForm.LiLoginForm loginForm = new LiForm.LiLoginForm();
            while (DialogResult.Yes == loginForm.ShowDialog())
            {
                Application.Run(new LiAdminForm());
                return;
            }
            //Application.Run(new RibbonForm4());

        }

    }

}
