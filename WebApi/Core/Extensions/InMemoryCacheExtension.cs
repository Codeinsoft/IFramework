using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Api.Core.Extensions
{
    public static class InMemoryCacheExtension
    {
        public static IServiceCollection ConfigureInMemoryCache(this IServiceCollection services)
        {
            return services.AddMemoryCache();
        }
    }
}
