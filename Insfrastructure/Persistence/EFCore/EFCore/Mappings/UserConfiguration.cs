using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using IFramework.Domain.History;
using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.EFCore.Mappings
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            //TODO: has key ve prop çakışması olacak mı???
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.PasswordHash);
            builder.Property(t => t.SecurityStamp);
            builder.Property(t => t.Email);
            builder.Property(t => t.CreatedBy);
            builder.Property(t => t.CreatedDate);
            builder.Property(t => t.Status);
            builder.Property(t => t.UpdatedBy);
            builder.Property(t => t.UpdatedDate);
            builder.Property(t => t.RoleId);


            builder.HasOne<UserInfo>(t => t.UserInfo).WithOne(p => p.User).HasForeignKey<User>(p => p.UserInfoId);
            builder.HasMany<History>(t => t.Histories);
            builder.HasOne<Role>(t => t.Role).WithMany(p => p.User).HasForeignKey(p => p.RoleId);

            ////HasMany(u => u.Address).KeyColumn("UserId").ReadOnly().Cascade.All();
            ///
        }
    }
}
