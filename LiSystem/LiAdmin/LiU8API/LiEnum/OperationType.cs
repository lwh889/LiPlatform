using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.LiEnum
{
    public struct OperationType
    {
        public const string NEW = "New";
        public const string MODIFY = "Modify";
        public const string DELETE = "Delete";
        public const string AUDIT = "Audit";
        public const string UNAUDIT = "UnAudit";
        public const string LOAD = "Load";
        public const string LOCK = "Lock";
        public const string UNLOCK = "UnLock";
        public const string CLOSE = "Close";
        public const string OPEN = "Open";

    }
}
