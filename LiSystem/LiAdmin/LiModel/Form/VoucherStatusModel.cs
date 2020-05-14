using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    public class VoucherStatusModel
    {
        public int id { set; get; }

        public string code { set; get; }
        public string name { set; get; }

        public List<StatusModel> dataStatuss { set; get; }
    }
}
