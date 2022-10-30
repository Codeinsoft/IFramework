using System;
using System.Linq;
using System.Net;
using System.Text;

using Castle.DynamicProxy;
using Newtonsoft.Json;

using IFramework.Application.Contract.Core.Request;
using IFramework.Application.Contract.Core.Response;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;

namespace IFramework.Infrastructure.Utility.Extensions
{
    public static class InvocationExtension
    {
        public static string GetOutput(this IInvocation invocation)
        {
            if (invocation.Method.ReturnType != typeof(void))
            {
                return JsonConvert.SerializeObject(invocation.ReturnValue, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            else return "void";
        }

        /// <summary>
        /// Methoda gelen parametleri ve çağrılmasını loglar.
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public static string GetInput(this IInvocation invocation)
        {
            if (!invocation.Arguments.Any())
            {
                return "void";
            }
            else
            {
                var stringBuilder = new StringBuilder("{" + invocation.Arguments.Length + "}");
                foreach (object argument in invocation.Arguments)
                {
                    var argumentDescription = JsonConvert.SerializeObject(argument);
                    stringBuilder.Append(argumentDescription).Append(",");
                }
                return stringBuilder.ToString();
            }
        }

        public static void SetReturnValueException(this IInvocation invocation, HttpStatusCode resultCode, params ErrorMessageDto[] errorMessages)
        {
            invocation.SetResponseBaseToRequestBase();
            if (invocation.Method.ReturnType != typeof(void))
            {
                var obje = IoCResolver.Instance.ReleaseInstance(invocation.Method.ReturnType);
                ResponseBase response = (ResponseBase)Activator.CreateInstance(obje.GetType());
                response.ResultCode = resultCode;
                response.ErrorMessages = errorMessages;
                invocation.ReturnValue = response;
            }
            else
            {
                ResponseBase response = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
                RequestBase requestBase = (RequestBase)invocation.Arguments[0];
                if (requestBase != null)
                {
                    response.ProcessId = requestBase.ProcessId;
                }
                response = invocation.ReturnValue as ResponseBase;
                if (response != null)
                {
                    response.ErrorMessages = errorMessages;
                    response.ResultCode = resultCode;
                }
                invocation.ReturnValue = response;
            }
        }

        public static void SetResponseBaseToRequestBase(this IInvocation invocation)
        {
            if (invocation.Method.ReturnType != typeof(void))
            {
                var obje = IoCResolver.Instance.ReleaseInstance(invocation.Method.ReturnType);
                ResponseBase response = (ResponseBase)Activator.CreateInstance(obje.GetType());
                RequestBase requestBase = (RequestBase)invocation.Arguments[0];
                if (requestBase != null)
                {
                    response.ProcessId = requestBase.ProcessId;
                }
                response = (ResponseBase)invocation.ReturnValue;
                invocation.ReturnValue = response;
            }
            else
            {
                ResponseBase response = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
                RequestBase requestBase = (RequestBase)invocation.Arguments[0];
                if (requestBase != null)
                {
                    response.ProcessId = requestBase.ProcessId;
                }
                response = (ResponseBase)invocation.ReturnValue;
                invocation.ReturnValue = response;
            }
        }
    }
}
