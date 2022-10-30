using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IFramework.Domain.Core.Entities;

namespace IFramework.Domain.User
{
    [Table("Role", Schema = "dbo")]
    public class Role : AuditableAndActivableEntity<Guid>, IAggregateRoot
    {
        #region Scalar Properties

        public virtual string Name { get; protected set; }
        public virtual string AuthorizationListJson { get; set; }
        public virtual ICollection<User> User { get; set; }
        #endregion

        protected Role()
        {
        }
        public Role(string name) : this()
        {
            Name = name;
        }
    }
}
