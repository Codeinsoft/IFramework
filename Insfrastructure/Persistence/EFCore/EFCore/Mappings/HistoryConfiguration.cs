using IFramework.Domain.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IFramework.Infrastructure.Persistence.EFCore.Mappings
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
    
        public HistoryConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.HasKey(t => t.ProcessId);
            builder.Property(t => t.ProcessInfo);
            builder.Property(t => t.MethodName);
            builder.Property(t => t.MethodInputParameters);
            builder.Property(t => t.MethodOutput);
            builder.Property(t => t.MethodExecutionTime);
            builder.HasOne<IFramework.Domain.User.User>(t => t.User);

        }

    }
}
