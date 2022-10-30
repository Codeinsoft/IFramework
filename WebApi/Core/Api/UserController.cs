using System;

using Microsoft.AspNetCore.Mvc;

using IFramework.Application.Authorization.Abstract;
using IFramework.Application.Contract.Authentication;
using IFramework.Application.Contract.UserDto;
using IFramework.Application.User.Abstract;

using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Application.Contract.Core.Response;
using System.Collections.Generic;
using IFramework.Application.Contract.Core.Request;

namespace IFramework.Api.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<UserDto, Domain.User.User, Guid>
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = IoCResolver.Instance.ReleaseInstance<IUserService>(); //applicationService;
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public ResponseResult<UserDto> Login(LoginUserRequestDto loginUserDto)
        {
            return _authenticationService.Login(loginUserDto);
        }

        [HttpPost("Register")]
        public ResponseResult<UserDto> Register(RegisterUserDto registerUserDto)
        {
            ResponseResult<UserDto> result = _authenticationService.Register(registerUserDto);
            return result;
        }

        [HttpPost("ApproveEmailAddress")]
        public ResponseBase ApproveEmailAddress(EmailApprovedCodeDto emailApprovedCodeDto)
        {
            return _authenticationService.ApproveEmailAddress(emailApprovedCodeDto);
        }

        [HttpPost("ChangePassword")]
        public ResponseBase ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
        {
            return _authenticationService.ChangePassword(changePasswordRequestDto);
        }

        [HttpPost("ChangeUserInfo")]
        public ResponseResult<UserDto> ChangeUserInfo(UserInfoDto userInfoDto)
        {
            return _userService.ChangeUserInfo(userInfoDto);
        }

        [HttpPost("GetAllIncludePaging")]
        public ResponseResult<PagingResponse<UserDto>> GetAllIncludePaging(PagingRequest request)
        {
            return _userService.GetAllIncludePaging(request);
        }

        [HttpPost("GetAllInclude")]
        public ResponseResult<List<UserDto>> GetAllInclude(RequestBase request)
        {
            return _userService.GetAllInclude(request);
        }
    }
}
