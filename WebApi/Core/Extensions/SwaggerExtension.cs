using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using IFramework.Api.Core.Extensions;
using Microsoft.AspNetCore.Builder;

namespace IFramework.Api.Core.Extensions
{

    public class SwaggerOptions
    {
        public bool Enable { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
    }

    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration, string swaggerConfigurationKey)
        {
            IEnumerable<SwaggerOptions> swaggerOptions = GetSwaggerOptions(configuration, swaggerConfigurationKey);

            if (swaggerOptions.Any())
            {
                services.AddSwaggerGen(c =>
                {
                    foreach (SwaggerOptions item in swaggerOptions)
                    {
                        c.SwaggerDoc(item.Version + "-" + item.FileName, new OpenApiInfo { Title = item.Title, Version = item.Version });
                    }
                });
            }

            return services;
        }

        public static void ConfigureSwagger(this IApplicationBuilder app,IConfiguration configuration, string swaggerConfigurationKey)
        {
            IEnumerable<SwaggerOptions> swaggerOptions = GetSwaggerOptions(configuration, swaggerConfigurationKey);

            if (swaggerOptions != null && swaggerOptions.Any())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    foreach (SwaggerOptions item in swaggerOptions)
                    {
                        c.SwaggerEndpoint("/swagger/" + item.Version + "-" + item.FileName + "/swagger.json", item.Title);
                    }
                });
            }
        }

        private static IEnumerable<SwaggerOptions> GetSwaggerOptions(IConfiguration configuration, string configurationKey)
        {

            IEnumerable<SwaggerOptions> swaggerOptions = configuration.GetConfigurationObjects<SwaggerOptions>(configurationKey);

            return swaggerOptions;
        }

    }
}
