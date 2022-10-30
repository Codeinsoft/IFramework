using IFramework.Domain.Core.Entities;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Utility.Configuration;
using Microsoft.Extensions.Options;

namespace IFramework.Domain.User
{
    public class UserInfo : AuditableAndActivableEntity<int>
    {
        public virtual string Name { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string ProfileImage { get; protected set; }
        public virtual User User { get; protected set; }

        protected UserInfo() { }

        protected UserInfo(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }
        protected internal UserInfo(string name, string lastName, string profileImage) : this(name, lastName)
        {
            Name = name;
            LastName = lastName;
            ProfileImage = string.IsNullOrEmpty(profileImage) ? IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DefaultProfileImage : profileImage;
        }

        protected internal virtual void ChangeName(string name)
        {
            Name = name;
        }
        protected internal virtual void ChangeLastName(string lastName)
        {
            LastName = lastName;
        }
        protected internal virtual void ChangeProfileImage(string profileImage)
        {
            ProfileImage = string.IsNullOrEmpty(profileImage) ? IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DefaultProfileImage : profileImage;
        }
        protected internal virtual void Change(string name, string lastName)
        {
            ChangeName(name);
            ChangeLastName(lastName);
        }
        protected internal virtual void Change(string name, string lastName, string profileImage)
        {
            ChangeName(name);
            ChangeLastName(lastName);
            ChangeProfileImage(profileImage);
        }
    }
}
