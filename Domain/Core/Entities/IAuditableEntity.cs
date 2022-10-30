using System;

namespace IFramework.Domain.Core.Entities
{
    public interface IAuditableEntity
    {
        /// <summary>
        /// Entity'nin oluşturulma tarihi
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Entity'i oluşturan kullanıcı adı
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Entity'nin güncellenme tarihi
        /// </summary>
        DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Entity'i güncelleyen kullanıcı adı
        /// </summary>
        string UpdatedBy { get; set; }
    }
}
