using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.GetEntity
{
    public class NewDataEntity : AHttpEntity
    {
        public NewDataEntity(string entityKey, string systemCode) : base(entityKey, systemCode)
        {
        }
        //public Dictionary<string, object> getEntityNewData1(string entityKey)
        //{
        //    this.entityKey = entityKey;
        //    return getEntityNewData();
        //}
    }
}
