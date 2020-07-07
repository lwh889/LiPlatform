using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression
{
    public class Expression
    {
        object instance;
        MethodInfo method;
        /// <summary>
        /// 表达试运算
        /// </summary>
        /// <param name="expression">表达试</param>
        public Expression(string returnType, string paramStr, string expression)
        {
            if (expression.IndexOf("return") < 0) expression = "return " + expression + ";";
            string className = "Expression";
            string methodName = "Compute";
            CompilerParameters p = new CompilerParameters();
            p.GenerateInMemory = true;
            CompilerResults cr = new CSharpCodeProvider().CompileAssemblyFromSource(p, string.
              Format("using System;sealed class {0}{{public {3} {1}({4}){{{2}}}}}",
              className, methodName, expression, returnType, paramStr));
            if (cr.Errors.Count > 0)
            {
                string msg = "Expression(\"" + expression + "\"): \n";
                foreach (CompilerError err in cr.Errors)
                { msg += err.ToString() + "\n"; }
                throw new Exception(msg);
            }
            instance = cr.CompiledAssembly.CreateInstance(className);
            method = instance.GetType().GetMethod(methodName);
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="x"></param>
        /// <returns>返回计算值</returns>
        public object Compute(object[] paramObject)
        {
            return method.Invoke(instance, paramObject);
        }
    }
}
