using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Castle.DynamicProxy;

using IFramework.Application.Contract.Core;
using IFramework.Application.Contract.Core.Request;
using IFramework.Application.Contract.Core.Response;
using IFramework.Domain.User;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Resources.Languages;
using IFramework.Infrastructure.Utility.Authentication;
using IFramework.Infrastructure.Utility.Configuration;
using IFramework.Infrastructure.Utility.Cryptography;
using IFramework.Infrastructure.Utility.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace IFramework.Infrastructure.Transversal.Aspect.Authorization
{
    public class AuthorizationAspect : IInterceptor
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AuthorizationAspect(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Intercept(IInvocation invocation)
        {
            if (UseNotAuthorization(invocation))
            {
                invocation.Proceed();
                return;
            }

            CustomAuthorizeAttribute authorizeAttribute = Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(CustomAuthorizeAttribute), true) as CustomAuthorizeAttribute;
            if (authorizeAttribute == null)
                authorizeAttribute = Attribute.GetCustomAttribute(invocation.TargetType, typeof(CustomAuthorizeAttribute), true) as CustomAuthorizeAttribute;

            string token = GetStringBearerToken();
            ITokenProvider tokenProvider= IoCResolver.Instance.ReleaseInstance<ITokenProvider>();
            if (!tokenProvider.ValidateToken(token))
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = ErrorMessage.InvalidToken
                });
                return;
            }
            
            if (!ValidateAuthorizationHeaders() || !ValidateIsNullToken(token))
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = ErrorMessage.NullTokenException
                });
                return;
            }
            Claim sIdClaim = tokenProvider.GetTokenClaims(token).FirstOrDefault(p => p.Type == ClaimTypes.Sid);
            if (!ValidateIsNullSidClaim(sIdClaim))
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = ErrorMessage.NullTokenException
                });
                return;
            }

            Guid userId = Guid.Parse(sIdClaim.Value);
            RequestBase requestBase = (RequestBase)invocation.Arguments[0];
            if (ValidateUserIdTokenAndRequestBody(requestBase, userId))
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = ErrorMessage.IsNotMatchTokenUserIdAndRequestUserId
                });
                return;
            }
            User resultUser = IoCResolver.Instance.ReleaseInstance<IUserRepository>().GetUserById(userId, false);
            if (resultUser == null)
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = string.Format(ErrorMessage.InvalidParameter, "kullanıcı numarası", "kullanıcı numarası")
                });
                return;
            }
            else if (!resultUser.EmailIsApproved)
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = ErrorMessage.EmailIsNotApproved
                });
                return;
            }
            else if (authorizeAttribute.AuthorizationType == AuthorizationType.BasicAuthorization)
            {
                // Kullanıcı giriş yaptı, basic authorization
                invocation.Proceed();
                SetNewTokenIfNeeded(invocation, string.Empty);
                return;

            }
            else if (authorizeAttribute.AuthorizationType == AuthorizationType.SystemUserAuthorization)
            {
                Type type = invocation.TargetType;
                CustomAuthorizeAttribute topLevelAuthorizeAttribute = type.GetCustomAttribute<CustomAuthorizeAttribute>(false);
                string container = topLevelAuthorizeAttribute == null ? authorizeAttribute.Container : topLevelAuthorizeAttribute.Container;
                container = string.IsNullOrEmpty(container) ? (invocation.TargetType.GetGenericArguments()[1]).Name : container;
                string action = authorizeAttribute.Action.ToString();

                if (IsAuthorizedService(resultUser, container, action))
                {
                    invocation.SetReturnValueException(HttpStatusCode.Forbidden, new ErrorMessageDto()
                    {
                        ErrorType = ErrorType.Exception,
                        Message = ErrorMessage.UnAuthorizedTransaction
                    });
                    return;
                }
                else
                {
                    invocation.Proceed();
                    ResponseBase response = (ResponseBase)invocation.ReturnValue;
                    response.Token = IoCResolver.Instance.ReleaseInstance<ITokenProvider>().CreateToken(userId.ToString());
                    invocation.ReturnValue = response;
                    return;
                }
            }
            else
            {
                invocation.SetReturnValueException(HttpStatusCode.Unauthorized, new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Exception,
                    Message = ErrorMessage.InvalidAuthorizationType
                });
                return;
            }

        }
        private bool UseNotAuthorization(IInvocation invocation)
        {
            return !IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.UseAuthentication || (!invocation.TargetType.IsDefined(typeof(CustomAuthorizeAttribute), true) && !invocation.MethodInvocationTarget.IsDefined(typeof(CustomAuthorizeAttribute), true));
        }
        private bool ValidateAuthorizationHeaders()
        {
            bool a = (string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]));
            return !a;
        }
        private string GetStringBearerToken()
        {
            StringValues authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (authorizationHeader.Count > 0 && !string.IsNullOrEmpty(authorizationHeader.ToString()))
            {
                return _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            }
            return null;
        }
        private bool ValidateIsNullToken(string token)
        {
            return !string.IsNullOrEmpty(token);
        }
        
        public bool ValidateIsNullSidClaim(Claim sIdClaim)
        {
            return sIdClaim != null;
        }
        private bool ValidateUserIdTokenAndRequestBody(RequestBase requestBase, Guid userId)
        {
            return userId != requestBase.UserId;
        }
        
        private bool IsAuthorizedService(User resultUser, string container, string action)
        {
            List<AuthorizationList> authorizationList = resultUser.Role.AuthorizationListJson.ToObject<List<AuthorizationList>>();
            return authorizationList != null ? (!authorizationList.Any(p => p.Container == container && p.Action == action)) : false;
        }
        private void SetNewTokenIfNeeded(IInvocation invocation, string lastToken)
        {
            if (lastToken != null)
            {
                ResponseBase response = invocation.ReturnValue as ResponseBase;
                if (response != null)
                    response.Token = lastToken;
            }
        }
    }
}
