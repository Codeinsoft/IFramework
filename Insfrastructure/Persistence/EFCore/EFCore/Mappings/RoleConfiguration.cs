using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.EFCore.Mappings
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {

            //TODO: has key ve prop çakışması olacak mı???
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name);
            builder.Property(t => t.AuthorizationListJson);
            builder.Property(t => t.CreatedBy);
            builder.Property(t => t.CreatedDate);
            builder.Property(t => t.Status);
            builder.Property(t => t.UpdatedBy);
            builder.Property(t => t.UpdatedDate);
        }

    }
}
