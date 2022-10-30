using IFramework.Application.Authorization.Abstract;
using IFramework.Application.Contract.Authentication;
using IFramework.Application.Contract.Core.Response;
using IFramework.Application.Contract.UserDto;

using Microsoft.AspNetCore.Mvc;

namespace IFramework.Api.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Sistemi kullanacak olan kullanıcının login olacağı servistir.
        /// </summary>
        /// <param name="registerUserDto">Kullanıcı adı ve şifre gönderilmelidir.</param>
        /// <returns>Login işlemi başarılı olur ise Token ile birlikte kullanıcı bilgileri de geri dönecektir.</returns>
        [HttpPost("Login")]
        public ResponseResult<UserDto> Login(LoginUserRequestDto registerUserDto)
        {
            return _authenticationService.Login(registerUserDto);
        }

        /// <summary>
        /// Sistemi kullanacak olan kullanıcının kayıt olacağı servistir.
        /// </summary>
        /// <param name="registerUserDto">Kayıt olma işlemi için email adresi ve şifre gönderilmelidir.</param>
        /// <returns>Kayıt işlemi başarılı olur ise kullanıcının bilgileri geri dönülecektir.</returns>
        [HttpPost("Register")]
        public ResponseResult<UserDto> Register(RegisterUserDto registerUserDto)
        {
            return _authenticationService.Register(registerUserDto);
        }

        /// <summary>
        /// Email adresine gönderilen kodu doğrulama için kullanılacak olan servistir.
        /// </summary>
        /// <param name="emailApprovedCodeDto">Email adresi ve email adresine gönderilen doğrulama kodu gönderilmelidir.</param>
        /// <returns>Başarılı bir doğrulama kodu gönderilmiş ve doğrulama yapılmış ise ResultCode 200 olarak geri dönülecektir.</returns>
        [HttpPost("ApproveEmailAddress")]
        public ResponseBase ApproveEmailAddress(EmailApprovedCodeDto emailApprovedCodeDto)
        {
            return _authenticationService.ApproveEmailAddress(emailApprovedCodeDto);
        }

        /// <summary>
        /// Kullanıcının şifresini değiştirme işlemi yapan servistir.
        /// </summary>
        /// <param name="changePasswordRequestDto">Kullanıcının eski şifresi ve yeni şifresi gönderilmelidir.</param>
        /// <returns>Şifresi başarılı bir şekilde değiştirilir ise ResultCode 200 olarak geri dönülecektir.</returns>
        [HttpPost("ChangePassword")]
        public ResponseBase ChangePassword(ChangePasswordRequestDto changePasswordRequestDto)
        {
            return _authenticationService.ChangePassword(changePasswordRequestDto);
        }
    }
}
