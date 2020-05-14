using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiEvent.Mediator;
using LiEvent.EventForm;
using LiContexts;

namespace LiEvent
{
    public abstract class LiAEvent
    {

        /// <summary>
        /// 唯一ID
        /// </summary>
        private string _id;

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { set { _id = value; } get { return _id; } }

        private LiContext _liContext;

        public LiContext LiContext { set { _liContext = value; } get { return _liContext; } }

        public LiAEvent()
        {
            this.id = Guid.NewGuid().ToString();
        }


        public LiAEvent(string id)
        {
            this.id = id;
        }

        /// <summary>
        /// 事件中介
        /// </summary>
        protected LiAEventMediator eventMediator;
        /// <summary>
        /// 设置事件中介
        /// </summary>
        /// <param name="eventMediator"></param>
        public void setMedium(LiAEventMediator eventMediator)
        {
            this.eventMediator = eventMediator;
        }

        /// <summary>
        /// 接受事件
        /// </summary>
        public abstract void receiveEvent();

        /// <summary>
        /// 发送事件
        /// </summary>
        public abstract void sendEvent();
    }
}
