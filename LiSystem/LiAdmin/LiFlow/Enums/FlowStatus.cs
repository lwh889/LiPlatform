using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiFlow.Enums
{
    public struct FlowStatus
    {
        public const string FINISH = "已完成";
        public const string RUN = "正在运行";
        public const string REVOKE = "已撤销";
        public const string HANGUP = "已挂起";
        public const string STOP = "已中止";
    }
}
