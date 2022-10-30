using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache.Redis.Config
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
