using IFramework.Application.Contract.Core.Attributes;
using IFramework.Application.Contract.Core.Request;

namespace IFramework.Application.Contract.Authentication
{
    public class LoginUserRequestDto : RequestBase
    {
        public string Email { get; set; }
        [IgnoreLog]
        public string Password { get; set; }
    }
}