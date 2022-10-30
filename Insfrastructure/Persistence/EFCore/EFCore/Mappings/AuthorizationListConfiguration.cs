using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using IFramework.Domain.User;

namespace IFramework.Infrastructure.Persistence.EFCore.Mappings
{
    public class AuthorizationListConfiguration : IEntityTypeConfiguration<AuthorizationList>
    {
        public void Configure(EntityTypeBuilder<AuthorizationList> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Container);
            builder.Property(t => t.Action);
            builder.Property(t => t.Title);

        }
    }   
}