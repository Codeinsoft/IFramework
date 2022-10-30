
using IFramework.Domain.Core.Entities;

namespace IFramework.Domain.User
{
    public class AuthorizationList : AuditableAndActivableEntity<int>, IAggregateRoot
    {
        public virtual string Container { get; set; }
        public virtual string Action { get; set; }
        public virtual string Title { get; set; }
    }
}

