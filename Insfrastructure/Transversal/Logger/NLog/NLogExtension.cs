using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace IFramework.Infra.Transversal.Log.NLog
{
    public static class NLogExtension
    {
        public static IServiceCollection ConfigureNLog(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
                loggingBuilder.AddNLog("nlog.config");

                //loggingBuilder.ClearProviders();
                //IConfigurationSection section = configuration.GetSection("NLog");
                //loggingBuilder.AddConfiguration(configuration);
                //loggingBuilder.AddNLog(configuration);
            });
        }
    }
}
