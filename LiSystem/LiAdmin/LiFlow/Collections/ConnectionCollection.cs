using System;
using System.Collections;
using LiFlow.Element;

namespace LiFlow.Collections
{
    /// <summary>
    /// 连接集合
    /// </summary>
    public class ConnectionCollection : CollectionBase
    {
        public ConnectionCollection()
        {
        }

        public int Add(Connection con)
        {
            return this.InnerList.Add(con);
        }

        public Connection this[int index]
        {
            get
            {
                return this.InnerList[index] as Connection;
            }
        }

        public void Remove(Connection con)
        {
            this.InnerList.Remove(con);
        }
    }
}
