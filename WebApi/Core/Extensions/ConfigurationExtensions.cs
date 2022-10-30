using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using IFramework.Infrastructure.Utility.Configuration;

namespace IFramework.Api.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddIFrameworkConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IFrameworkConfig>(configuration.GetSection(ConfigurationConstants.IFrameworkConfigSectionName));
            return services;
        }
    }
}
