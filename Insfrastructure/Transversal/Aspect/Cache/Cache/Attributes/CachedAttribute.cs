using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Utility.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Cache.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CachedAttribute : Attribute
    {

        public CachedAttribute()
        {

        }
        public string CacheKey { get; set; }
        public CacheAction CacheAction { get; set; } = CacheAction.Get;

        private int _duration;
        /// <summary>
        /// Cache expiration time ms
        /// </summary>
        public int Duration
        {
            get
            {
                if (_duration == 0)
                {
                    _duration = IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.CacheDuration;
                    if (_duration == 0)
                        _duration = (24 * 1000); // 24 saat
                }
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }
    }

    public enum CacheAction
    {
        Get = 0,
        Delete = 1
    }
}
