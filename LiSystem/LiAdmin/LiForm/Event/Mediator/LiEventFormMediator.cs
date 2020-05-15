using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Event.Mediator
{
    public class LiEventFormMediator : LiAEventMediator
    {
        private Dictionary<string, LiAEvent> liEventDict = new Dictionary<string, LiAEvent>();
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="liEvent"></param>
        public override void register(LiAEvent liEvent)
        {
            if (!liEventDict.Keys.Contains(liEvent.id))
            {
                liEventDict.Add(liEvent.id, liEvent);
                liEvent.setMedium(this);
            }
        }

        /// <summary>
        /// 转化
        /// </summary>
        /// <param name="rLiEvent"></param>
        public override bool relay(LiAEvent rLiEvent)
        {
            foreach (LiAEvent sLiEvent in liEventDict.Values)
            {
                if (sLiEvent.id != rLiEvent.id)
                {
                    return ((LiAEvent)sLiEvent).receiveEvent();
                }
            }
            return false;
        }

        /// <summary>
        /// 获取事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override LiAEvent getLiEvent(string key)
        {
            return liEventDict[key];
        }
    }
}
