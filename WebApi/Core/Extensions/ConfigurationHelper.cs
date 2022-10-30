using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IFramework.Api.Core.Extensions
{

    public static class ConfigurationHelper
    {
        /// <summary>
        /// appsettingste tanimlanmis objenin nesnesini doner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configurationTag"></param>
        public static T GetConfigurationObject<T>(
            this IConfiguration configuration,
            string configurationKey = null)
            where T : class, new()
        {
            List<T> configurationItems = new List<T>();
            configuration.Bind(configurationKey, configurationItems);

            if (configurationItems != null && configurationItems.Count == 1)
            {
                return configurationItems[0];
            }
            else
            {
                throw new Exception("Multiple ConfigurationKey detected:  " + configurationKey);
            }

        }

        /// <summary>
        /// appsettingste tanimlanmis objenin nesnelerini doner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configurationTag"></param>
        public static IEnumerable<T> GetConfigurationObjects<T>(
                  this IConfiguration configuration,
            string configurationKey = null)
            where T : class, new()
        {
            List<T> configurationItems = new List<T>();
            configuration.Bind(configurationKey, configurationItems);

            if (configurationItems != null && configurationItems.Any())
            {
                return configurationItems;
            }
            else
            {
                throw new Exception("No ConfigurationKey detected:  " + configurationKey);
            }

        }

    }

}
