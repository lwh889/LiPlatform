using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8VoucherModel
    {
        public int id { set; get; }

        public string code { set; get; }

        public string name { set; get; }

        public string voucherType { set; get; }

        public List<LiU8OperationModel> operations;

    }
}
