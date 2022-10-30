using System;

namespace IFramework.Domain.Core.Entities
{
    public class AuditableEntity<TPrimaryKey> : Entity<TPrimaryKey>, IAuditableEntity
    {
        /// Entity'nin oluşturulma tarihi
        /// </summary>
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Entity'i oluşturan kullanıcı adı
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Entity'nin güncellenme tarihi
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Entity'i güncelleyen kullanıcı adı
        /// </summary>
        public virtual string UpdatedBy { get; set; }
    }
}
