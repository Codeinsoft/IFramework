using IFramework.Application.Contract.Core.Request;

namespace IFramework.Application.Contract.Authentication
{
    public class RegisterUserDto : RequestBase
    {
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

    }

}