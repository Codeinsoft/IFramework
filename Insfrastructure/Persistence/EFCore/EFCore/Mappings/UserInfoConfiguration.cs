using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.EFCore.Mappings
{
    public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.Property(t => t.Name);
            builder.Property(t => t.LastName);
            builder.Property(t => t.ProfileImage);
            builder.HasOne<User>(t => t.User).WithOne(p => p.UserInfo).HasForeignKey<User>(p => p.UserInfoId);

        }
    }
}
