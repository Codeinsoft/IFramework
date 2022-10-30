using IFramework.Domain.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFramework.Infrastructure.Persistence.EFCore.Mappings
{

    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {

        public LogConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Log> builder)
        {

            //TODO: has key ve prop çakışması olacak mı???
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Date);
            builder.Property(t => t.Exception);
            builder.Property(t => t.Level);
            builder.Property(t => t.Logger);
            builder.Property(t => t.Message);
            builder.Property(t => t.Thread);
            builder.Property(t => t.Username);
            builder.Property(t => t.CreatedBy);
            builder.Property(t => t.CreatedDate);
            builder.Property(t => t.Status);
            builder.Property(t => t.UpdatedBy);
            builder.Property(t => t.UpdatedDate);

        }

    }
}
