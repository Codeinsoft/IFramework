using System;
using System.Collections.Generic;
using IFramework.Application.Contract.UserDto;

namespace IFramework.Application.Contract.Authentication
{
    public class LoginUserResponseDto
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
        public DateTime CreatedDate { get; protected set; }

        /// <summary>
        /// Entity'i oluşturan kullanıcı adı
        /// </summary>
        public string CreatedBy { get; protected set; }

        /// <summary>
        /// Entity'nin güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; protected set; }

        /// <summary>
        /// Entity'i güncelleyen kullanıcı adı
        /// </summary>
        public string UpdatedBy { get; protected set; }
        public Status Status { get; set; }


        public virtual IEnumerable<RoleDto> Roles { get; set; }
        public UserInfoDto UserInfo { get; set; }

    }
}