using IFramework.Domain.History;
using IFramework.Domain.User;
using IFramework.Infrastructure.Persistence.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IFramework.Infrastructure.Persistence.EFCore
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions options) : base(options)
        {
        }

        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<History> History { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration)));

            //Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
