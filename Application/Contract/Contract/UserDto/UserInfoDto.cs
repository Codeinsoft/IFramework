using IFramework.Application.Contract.Core.Request;
using System;

namespace IFramework.Application.Contract.UserDto
{
    public class UserInfoDto : RequestBase
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public Status Status { get; set; }
    }
}
