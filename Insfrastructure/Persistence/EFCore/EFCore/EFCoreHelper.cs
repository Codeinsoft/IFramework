using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using Microsoft.Extensions.Options;
using IFramework.Infrastructure.Utility.Configuration;

namespace IFramework.Infrastructure.Persistence.EFCore
{
    public static class EFCoreHelper
    {
        /// <summary>
        /// Sets up entity framework core
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection UseEFCore<TDbContext>(this IServiceCollection services, IHostingEnvironment hostingEnvironment, IConfiguration configuration) where TDbContext : DbContext
        {
            return services.AddEntityFrameworkSqlServer().AddDbContext<TDbContext>((serviceProvider, options) =>
                    options.UseSqlServer(configuration.GetConnectionString(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DbConnectionStringName))
                    .UseInternalServiceProvider(serviceProvider), ServiceLifetime.Transient);
            //if (hostingEnvironment.IsDevelopment())
            //{
            //    return services.AddEntityFrameworkSqlServer().AddDbContext<TDbContext>((serviceProvider, options) =>
            //        options.UseSqlServer(configuration.GetConnectionString(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DbConnectionStringName))
            //        .UseInternalServiceProvider(serviceProvider), ServiceLifetime.Transient);
            //}
            //else
            //{
            //    return services.AddEntityFrameworkSqlServer().AddDbContext<TDbContext>((serviceProvider, options) =>
            //        options.UseSqlServer(configuration.GetConnectionString(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DbConnectionStringName))
            //        .UseInternalServiceProvider(serviceProvider), ServiceLifetime.Transient);
            //}
        }

        /// <summary>
        /// Sets up entity framework core
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection UseEFCore<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
        {
            return services.AddDbContext<TDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DbConnectionStringName)), ServiceLifetime.Transient);
        }

        /// <summary>
        /// EFCore DBContext oluşturulması işlemini yapar.
        /// </summary>
        /// <returns>EFDbContext</returns>
        public static EFDbContext CreateEFDbContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DbConnectionStringName));

            return new EFDbContext(optionsBuilder.Options);
        }
    }
}
