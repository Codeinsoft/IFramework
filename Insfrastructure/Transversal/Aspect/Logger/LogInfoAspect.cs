using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;

using Castle.DynamicProxy;

using IFramework.Application.Contract.Core.Attributes;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Logger;
using IFramework.Infrastructure.Transversal.Logger.Attributes;
using IFramework.Infrastructure.Utility.Configuration;

namespace IFramework.Infrastructure.Transversal.Aspect.Log
{
    public class LogInfoAspect : IInterceptor
    {
        private readonly ILog _logger;

        public LogInfoAspect(ILog logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            LogInfoBefore(invocation);
            invocation.Proceed();
            LogInfoAfter(invocation);
        }

        /// <summary>
        /// Methoda sonunda geri dönen sonucun loglanması işlemini yapar.
        /// </summary>
        /// <param name="invocation"></param>
        private void LogInfoAfter(IInvocation invocation)
        {
            if (IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.UseLogInfoAfter && (invocation.TargetType.IsDefined(typeof(LogInfoAttribute), true) || invocation.MethodInvocationTarget.IsDefined(typeof(LogInfoAttribute), true)))
            {
                if (invocation.Method.ReturnType != typeof(void))
                {
                    _logger.Info("CallAfter '{0}' ReturnValue : {1}", invocation.Method, invocation.ReturnValue != null ? invocation.ReturnValue.ToSerializeIgnoreAttribute<IgnoreLog>() : "null");
                }
            }
        }

        /// <summary>
        /// Methoda gelen parametleri ve çağrılmasını loglar.
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private void LogInfoBefore(IInvocation invocation)
        {
            if (IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.UseLogInfoBefore && (invocation.TargetType.IsDefined(typeof(LogInfoAttribute), true) || invocation.MethodInvocationTarget.IsDefined(typeof(LogInfoAttribute), true)))
            {
                if (!invocation.Arguments.Any())
                {
                    _logger.Info("CallBefore '{0}' Args: Void", invocation.Method);
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var argument in invocation.Arguments)
                    {
                        if (argument != null)
                        {
                            string argumentString = argument.ToSerializeIgnoreAttribute<IgnoreLog>();
                            stringBuilder.Append(argumentString);
                        }
                    }
                    _logger.Info("CallBefore '{0}' Args: {1}", invocation.Method, stringBuilder.ToString());
                }
            }
        }
    }
}
