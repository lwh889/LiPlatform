﻿using System;
using System.Collections;
using LiFlow.Element;

namespace LiFlow.Collections
{
    public class ConnectorCollection : CollectionBase
    {
        public ConnectorCollection()
        {
        }

        public int Add(Connector con)
        {
            return this.InnerList.Add(con);
        }

        public Connector this[int index]
        {
            get
            {
                return this.InnerList[index] as Connector;
            }
        }

        public void Remove(Connector c)
        {
            if (this.InnerList.Contains(c))
            {
                System.Diagnostics.Trace.WriteLine("yep, removed.");
            }
            this.InnerList.Remove(c);
        }
    }
}
