using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiEvent.Mediator
{

    /// <summary>
    /// 事件中介抽象类
    /// </summary>
    public abstract class LiAEventMediator
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="liEvent"></param>
        public abstract void register(LiAEvent liEvent);

        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract LiAEvent getLiEvent(string key);

        /// <summary>
        /// 转发事件
        /// </summary>
        /// <param name="liEvent"></param>
        public abstract void relay(LiAEvent liEvent); //转发
    }
}
