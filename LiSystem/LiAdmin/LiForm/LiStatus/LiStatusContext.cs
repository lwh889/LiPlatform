using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.LiStatus
{
    public class LiStatusContext
    {
        private LiAStatus status;
        private LiAStatus previousStatus;
        private Dictionary<string, LiAStatus> stateDict = new Dictionary<string, LiAStatus>();
        public LiStatusContext()
        {

        }

        public bool addStatus(string statusName, LiAStatus liStatus)
        {
            if (!stateDict.ContainsKey(statusName))
            {
                liStatus.statusName = statusName;
                stateDict.Add(statusName, liStatus);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setStatus(LiAStatus liStatus)
        {
            this.status = liStatus;
        }

        public void setPreviousStatus(LiAStatus liStatus)
        {
            this.previousStatus = liStatus;
        }
        public LiAStatus getPreviousStatus()
        {
            return previousStatus;
        }

        public LiAStatus getStatus(string statusName)
        {
            if (stateDict.ContainsKey(statusName))
            {
                return stateDict[statusName];
            }
            else
            {
                return null;
            }
        }

        public LiAStatus getCurrentStatus()
        {
            return this.status;
        }

        public Dictionary<string, LiAStatus> getStatusDict()
        {
            return stateDict;
        }

        //public LiAStatus getNewStatus()
        //{
        //    foreach (KeyValuePair<string, LiAStatus> kvp in stateDict)
        //    {
        //        if(kvp.Value.
        //    }
        //    return this.status;
        //}


        public void Handle()
        {
            status.Handle(this);
        }
    }
}
