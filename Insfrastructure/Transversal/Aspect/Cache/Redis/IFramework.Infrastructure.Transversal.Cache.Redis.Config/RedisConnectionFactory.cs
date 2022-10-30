using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache.Redis.Config
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        /// <summary>
        ///     The _connection.
        /// </summary>
        private readonly Lazy<ConnectionMultiplexer> _connection;

        public IConfiguration Configuration { get; set; }

        public RedisConnectionFactory(IConfiguration Configuration)
        {
            this.Configuration = Configuration;

            var redisConnectionString = Configuration.GetConnectionString("IFrameworkRedis");
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                throw new Exception("Environment Variables doesn't have named IFRAMEWORK_REDIS_CONNECTION_STRING in .env");
            }

            this._connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisConnectionString));
        }

        public ConnectionMultiplexer Connection()
        {
            return this._connection.Value;
        }

    }
}
