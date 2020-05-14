using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

using LiCommon.Util;
namespace LiContexts.LiRealProxy
{
    public class HttpEntityShowMessageProxy<T> : RealProxy
    {
        public readonly T _httpEntity;
        public HttpEntityShowMessageProxy(T httpEntity)
            : base(typeof(T))
        {
            _httpEntity = httpEntity;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            try
            {
                var result = methodInfo.Invoke(_httpEntity, methodCall.InArgs);

                return new ReturnMessage(result, null, 0,
                  methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                //Log(string.Format(
                //  "In Dynamic Proxy- Exception {0} executing '{1}'", e),
                //  methodCall.MethodName);
                return new ReturnMessage(e, methodCall);
            }

            throw new NotImplementedException();
        }


        #region 显示窗口
        private void ShowMessageTips(string msg)
        {
            ShowMessage(msg, true, "温馨提示");
        }

        private void ShowMessageTips(string msg, bool isShow)
        {
            ShowMessage(msg, isShow, "温馨提示");
        }

        private void ShowMessageSystem(string msg)
        {
            ShowMessage(msg, true, "系统提示");
        }

        private void ShowMessageSystem(string msg, bool isShow)
        {
            ShowMessage(msg, isShow, "系统提示");
        }

        private void ShowMessageYonyou(string msg)
        {
            ShowMessage(msg, true, "用友提示");
        }

        private void ShowMessageYonyou(string msg, bool isShow)
        {
            ShowMessage(msg, isShow, "用友提示");
        }

        private void ShowMessage(string msg, bool isShow, string title)
        {
            if (isShow)
            {
                MessageUtil.Show(msg, "温馨提示");
            }
        }
        #endregion

    }
}
