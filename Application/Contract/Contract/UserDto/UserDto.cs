using IFramework.Application.Contract.Core.Request;
using System;

namespace IFramework.Application.Contract.UserDto
{
    public class UserDto : RequestBase
    {
        public string Email { get; set; }
        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }


        /// <summary>
        /// Entity'nin primary key property'sidir.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Entity'nin oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Entity'i oluşturan kullanıcı adı
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Entity'nin güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        public Guid EmailApprovedCode { get; set; }
        public bool EmailIsApproved { get; set; }

        /// <summary>
        /// Entity'i güncelleyen kullanıcı adı
        /// </summary>
        public string UpdatedBy { get; set; }
        public Status Status { get; set; }

        public Guid RoleId { get; set; }

        public int UserInfoId { get; set; }

        public RoleDto Role { get; set; }
        public UserInfoDto UserInfo { get; set; }

        public RequestBase RequestBase { get; set; }

    }
}