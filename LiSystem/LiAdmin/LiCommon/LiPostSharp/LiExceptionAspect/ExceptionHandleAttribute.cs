using System;
using PostSharp.Aspects;
using PostSharp.Serialization;

using LiCommon.Util;

namespace LiCommon.LiPostSharp.LiExceptionAspect
{
    [PSerializable]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class ExceptionHandleAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            // 设置流程行为（继续执行，不抛出）
            args.FlowBehavior = FlowBehavior.Continue;

            MessageUtil.Show(args.Exception.Message, "系统提示");
            //Console.WriteLine($"发生异常:{args.Exception.GetType().FullName} ----> {args.Exception.Message}");
        }
    }
}
