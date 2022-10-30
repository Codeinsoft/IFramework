using System;
using Microsoft.Extensions.Options;
using Castle.Core.Internal;
using Castle.DynamicProxy;

using IFramework.Infrastructure.Transversal.Cache.Interfaces;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Utility.Configuration;

namespace IFramework.Infrastructure.Transversal.Cache.Attributes
{
    public sealed class CachingInterceptor : IInterceptor
    {

        public ICacheProvider CacheProvider { get; set; }

        public void Intercept(IInvocation invocation)
        {
            if (!IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.UseCache || !invocation.MethodInvocationTarget.IsDefined(typeof(CachedAttribute), true))
            {
                invocation.Proceed();
                return;
            }

            var cacheAttribute = invocation.MethodInvocationTarget.GetAttribute<CachedAttribute>();
            if (cacheAttribute == null)
            {
                throw new Exception("CachedAttribute attribute object couldn't be got from reference");
            }

            // generate cache key
            var cacheKey = string.IsNullOrWhiteSpace(cacheAttribute?.CacheKey) == true
                ? CacheProvider.GetCacheKey(invocation.Method.Module.Name, invocation.Method.Name, invocation.Arguments)
                : cacheAttribute.CacheKey;

            Type cacheRepositoryType = typeof(ICacheRepository<>).MakeGenericType(invocation.Method.ReturnType);

            dynamic cacheRepository = IoCResolver.Instance.ReleaseInstance(cacheRepositoryType);

            switch (cacheAttribute.CacheAction)
            {
                case CacheAction.Get:

                    if (cacheRepository.KeyExists(cacheKey))
                    {
                        invocation.ReturnValue = cacheRepository.Get(cacheKey);
                    }
                    else
                    {
                        invocation.Proceed();
                        cacheRepository.Set(cacheKey, invocation.ReturnValue, cacheAttribute.Duration);
                    }
                    break;
                case CacheAction.Delete:

                    invocation.Proceed();
                    cacheRepository.Delete(cacheKey);

                    break;
            }

        }

    }
}
