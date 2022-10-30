using System;
using System.Collections.Generic;
using System.Net;

using Castle.DynamicProxy;
using IFramework.Application.Contract.Core.Response;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Logger;
using IFramework.Infrastructure.Transversal.Logger.Attributes;
using IFramework.Infrastructure.Utility.Configuration;
using IFramework.Infrastructure.Utility.Extensions;
using Microsoft.Extensions.Options;

namespace IFramework.Infrastructure.Transversal.Aspect.ExceptionAspect
{
    public class ExceptionInterceptor : IInterceptor
    {
        private readonly ILog _logger;
        public ExceptionInterceptor(ILog logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.UseException || !invocation.TargetType.IsDefined(typeof(LogExceptionAttribute), true) && !invocation.MethodInvocationTarget.IsDefined(typeof(LogExceptionAttribute), true))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                invocation.Proceed();
                invocation.SetResponseBaseToRequestBase();
            }
            catch (Exception ex)
            {
                string exceptionId = Guid.NewGuid().ToString();
                _logger.Error(exceptionId, ex);
                invocation.SetReturnValueException(HttpStatusCode.InternalServerError, new ErrorMessageDto()
                {
                    Code = exceptionId,
                    ErrorType = ErrorType.Exception,
                    Message = ex.Message
                });
            }
        }
    }
}
