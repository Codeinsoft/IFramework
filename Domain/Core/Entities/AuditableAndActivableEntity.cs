using System;

namespace IFramework.Domain.Core.Entities
{
    public abstract class AuditableAndActivableEntity<TPrimaryKey> : ActivableEntity<TPrimaryKey>, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
