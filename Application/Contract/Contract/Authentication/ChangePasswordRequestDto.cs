using IFramework.Application.Contract.Core.Attributes;
using IFramework.Application.Contract.Core.Request;

namespace IFramework.Application.Contract.Authentication
{
    public class ChangePasswordRequestDto : RequestBase
    {
        [IgnoreLog]
        public string OldPassword { get; set; }
        [IgnoreLog]
        public string NewPassword { get; set; }
        [IgnoreLog]
        public string NewPasswordApproval { get; set; }

    }
}