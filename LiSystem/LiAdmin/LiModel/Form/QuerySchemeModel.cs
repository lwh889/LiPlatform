using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class QuerySchemeModel
    {
        [NotCopy]
        public int id;

        public string entityKey;
        
        public string userCode;

        public string querySchemeName;

        [NotCopy]
        public List<QueryModel> querys;

        [NotCopy]
        public List<EntityModel> entitys;

        [NotCopy]
        public List<FieldModel> fields;
    }
}
