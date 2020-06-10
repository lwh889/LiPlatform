using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.LiPush
{
    public class PushEventModel
    {
        public static PushEventModel getInstance(int pushFormId, int pushListButtonId)
        {
            return new PushEventModel() { pushFormId = pushFormId, pushListButtonId = pushListButtonId };
        }
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int id { set; get; }


        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int pushFormId { set; get; }
        
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int pushListButtonId { set; get; }

        /// <summary>
        /// 全名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("全名称")]
        public string fullName { set; get; }

        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("程序集")]
        public string assemblyName { set; get; }
        
        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("备注")]
        public string eventMemo { set; get; }
        
    }
}
