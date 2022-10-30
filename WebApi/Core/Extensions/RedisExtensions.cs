using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IFramework.Api.Core.Extensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            // TODO: refactor -> constant
            //var redisConnectionString = Environment.GetEnvironmentVariable("IFRAMEWORK_REDIS_CONNECTION_STRING");
            //if (string.IsNullOrEmpty(redisConnectionString))
            //{
            //    throw new Exception("Environment Variables doesn't have named IFRAMEWORK_REDIS_CONNECTION_STRING in .env");
            //}


            var redisConnectionString = configuration.GetConnectionString("IFrameworkRedis");
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new Exception("Environment Variables doesn't have named IFRAMEWORK_REDIS_CONNECTION_STRING in .env");
            }

            // for local 
            // app settingse ekle

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            // for docker
            ////services.Configure<RedisConfiguration>(configuration.GetSection("redis"));
            //services.AddSession();

            //return services.AddDistributedRedisCache(option =>
            //{
            //    //option.Configuration = configuration.GetConnectionString("redis");
            //});

            return services.AddSession();

        }
    }
}
