using System;
using System.Net;

using IFramework.Application.Authorization.Abstract;
using IFramework.Application.Contract.Authentication;
using IFramework.Application.Contract.Core;
using IFramework.Application.Contract.UserDto;

using IFramework.Domain.User;
using IFramework.Infrastructure.Utility.Cryptography;
using IFramework.Infrastructure.Utility.Extensions;
using IFramework.Infrastructure.Transversal.Map.Abstract;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Aspect.UnitOfWork;
using IFramework.Infrastructure.Transversal.Logger.Attributes;
using IFramework.Application.Contract.Core.Response;

using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using IFramework.Infrastructure.Transversal.Resources.Languages;
using IFramework.Infrastructure.Utility.Authentication;
using Microsoft.AspNetCore.Http;

namespace IFramework.Application.Authorization.Concrete
{
    [LogException]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        private readonly IMap _map;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IUserRepository userRepository, IMap map
            , IHttpContextAccessor httpContextAccessor
            )
        {
            _userRepository = userRepository;
            _map = map;
            _httpContextAccessor = httpContextAccessor;
        }


        [UnitOfWork]
        public ResponseResult<UserDto> Login(LoginUserRequestDto loginUserDto)
        {
            ResponseResult<UserDto> resultResponse = new ResponseResult<UserDto>();
            Domain.User.User user = _userRepository.GetUserByEmail(loginUserDto.Email, false);

            if (user == null)
                throw new Exception(ErrorMessage.EmailNotRegistered); //ValidationException(ErrorMessage.EmailNotRegister, ExceptionCodeConstants.EmailNotRegisterCode);

            user.PasswordControl(loginUserDto.Password);

            resultResponse.Result = _map.Mapping<UserDto>(user);
            resultResponse.ErrorMessages = null;
            resultResponse.ResultCode = HttpStatusCode.OK;

            // authentication successful so generate jwt token
            _httpContextAccessor.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "token");
            _httpContextAccessor.HttpContext.Response.Headers.Add("token", IoCResolver.Instance.ReleaseInstance<ITokenProvider>().CreateToken(user.Id.ToString()));
            return resultResponse;
        }

        public ResponseResult<UserDto> Register(RegisterUserDto registerUserDto)
        {
            ResponseResult<UserDto> result = IoCResolver.Instance.ReleaseInstance<ResponseResult<UserDto>>();
            bool emailRegistered = _userRepository.CheckEmailRegistered(registerUserDto.Email);
            if (emailRegistered)
                throw new Exception("EmailRegisteredAlready");// ValidationException(ErrorMessage.EmailRegisteredAlready, ExceptionCodeConstants.EmailRegisteredAlreadyCode);

            Domain.User.User user = _map.Mapping<Domain.User.User>(registerUserDto);
            user.ChangeUserInfo(registerUserDto.Name, registerUserDto.LastName, registerUserDto.ProfileImage);
            user.Id = _userRepository.Add(user);
            //Helper.QueueHelper.SendQueue(user.Id, "EmailApproved");
            result.Result = _map.Mapping<UserDto>(user);
            return result;
        }

        public ResponseBase ApproveEmailAddress(EmailApprovedCodeDto emailApprovedCodeDto)
        {
            ResponseBase result = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
            Domain.User.User user = _userRepository.GetById(emailApprovedCodeDto.UserId);
            if (user == null) throw new Exception("UserNotFound");//ValidationException(ErrorMessage.UserNotFound, ExceptionCodeConstants.UserNotFoundCode);
            if (user.EmailApprovedCode != emailApprovedCodeDto.ApprovedCode)
                throw new Exception("EmailApprovedCodeException"); //ValidationException(ErrorMessage.EmailApprovedCodeException, ExceptionCodeConstants.EmailApprovedCodeExceptionCode);

            user.EmailApproved(emailApprovedCodeDto.ApprovedCode, emailApprovedCodeDto.IpAddress);
            return result;
        }

        public ResponseBase ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
        {
            ResponseBase result = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
            Domain.User.User user = _userRepository.GetById(changePasswordRequestDto.UserId);
            if (user == null)
                throw new Exception("UserNotFound");//ValidationException(ErrorMessage.UserNotFound, ExceptionCodeConstants.UserNotFoundCode);

            ErrorMessageDto errorMessage = user.ChangePassword(changePasswordRequestDto.OldPassword, changePasswordRequestDto.NewPassword, changePasswordRequestDto.NewPasswordApproval);
            result.SetResponse(errorMessage != null ? HttpStatusCode.BadRequest : HttpStatusCode.OK, errorMessage);
            return result;
        }
    }
}
