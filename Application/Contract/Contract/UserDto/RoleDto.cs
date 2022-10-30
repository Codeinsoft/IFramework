using IFramework.Application.Contract.Core.Request;
using System;

namespace IFramework.Application.Contract.UserDto
{
    public class RoleDto : RequestBase
    {
        public string Name { get; set; }
        public bool IsCreate { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsSelect { get; set; }


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

        /// <summary>
        /// Entity'i güncelleyen kullanıcı adı
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Entity'nin durum bilgisini belirtir.
        /// </summary>
        public Status Status { get; set; }
    }
}