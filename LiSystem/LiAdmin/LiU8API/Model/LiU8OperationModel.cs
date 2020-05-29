using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8OperationModel
    {
        public int id { set; get; }

        public int fid { set; get; }
        /// <summary>
        /// 操作编码
        /// </summary>
        public string operationCode { set; get; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string operationName { set; get; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string operationSymbol { set; get; }

        public List<LiU8ParamModel> paramModels;

        public List<LiU8FieldModel> fields;

        public List<LiU8EnvContextModel> contexts;
    }
}
