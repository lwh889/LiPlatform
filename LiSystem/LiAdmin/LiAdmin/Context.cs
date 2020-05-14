using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;

namespace LiAdmin
{
    public class Context
    {
        public Model.FormModel formModel { set; get; }

        public RibbonForm ribbonForm { set; get; }

        public string url { set; get; }

        public string username { set; get; }

        public string password { set; get; }
    }
}
