using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiForm.Event.Mediator;
using LiForm.Event.EventForm;
using LiContexts;

using LiForm.Dev;

namespace LiForm.Event
{
    public abstract class LiAEvent
    {

        public object Tag { set; get; }

        private string _focusEntityKey;
        /// <summary>
        /// 当前表体实体
        /// </summary>
        public string focusEntityKey { set { _focusEntityKey = value; } get { return _focusEntityKey; } }


        /// <summary>
        /// 唯一ID
        /// </summary>
        private string _id;

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string id { set { _id = value; } get { return _id; } }

        public LiContext _liContext ;

        public LiContext liContext { set { _liContext = value; } get { return _liContext; } }

        public LiAEvent()
        {
            this.id = Guid.NewGuid().ToString();
        }


        public LiAEvent(string id)
        {
            this.id = id;
        }

        
        private LiForm.Dev.LiForm _liForm;
        public LiForm.Dev.LiForm liForm { set { _liForm = value; } get { return _liForm; } }

        private LiForm.Dev.LiListForm _liListForm;
        public LiForm.Dev.LiListForm liListForm { set { _liListForm = value; } get { return _liListForm; } }
 
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
        public abstract bool receiveEvent();

        /// <summary>
        /// 发送事件
        /// </summary>
        public abstract bool sendEvent();
    }
}
