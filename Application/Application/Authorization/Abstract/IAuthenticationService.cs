using IFramework.Application.Contract.Authentication;
using IFramework.Application.Contract.Core.Response;
using IFramework.Application.Contract.Core.Service;
using IFramework.Application.Contract.UserDto;


namespace IFramework.Application.Authorization.Abstract
{
    public interface IAuthenticationService : IApplicationService
    {
        ResponseResult<UserDto> Login(LoginUserRequestDto loginUserDto);
        ResponseResult<UserDto> Register(RegisterUserDto registerUserDto);
        ResponseBase ApproveEmailAddress(EmailApprovedCodeDto emailApprovedCodeDto);
        ResponseBase ChangePassword(ChangePasswordRequestDto changePasswordRequestDto);
    }
}
