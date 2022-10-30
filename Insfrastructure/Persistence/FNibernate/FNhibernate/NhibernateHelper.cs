using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using IFramework.Infrastructure.Persistence.FNhibernate.Mapping;
using Microsoft.Extensions.Configuration;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using Microsoft.Extensions.Options;
using IFramework.Infrastructure.Utility.Configuration;

namespace IFramework.Infrastructure.Persistence.FNhibernate
{

    public class NhibernateHelper
    {
        private readonly IConfiguration _configuration;
        public NhibernateHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Nhibernate Session oluşturulması için SessionFactory oluşturulması işlemini yapar.
        /// </summary>
        /// <returns>NHibernate Session Factory</returns>
        public ISessionFactory CreateNhSessionFactory()
        {
            var mapping = new PersistenceModel();
            mapping.AddMappingsFromAssembly(Assembly.GetAssembly(typeof(UserMap)));
            var connStr = _configuration.GetConnectionString(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.DbConnectionStringName);
            var config = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(connStr).ShowSql()).BuildConfiguration();
            mapping.Configure(config);

            #region Veri tabanı oluşturulması için kullanılır.
            if (IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.GenerateDatabase)
            {
                var exporter = new SchemaExport(config);
                exporter.Execute(true, true, false);
            }
            #endregion

            return config.BuildSessionFactory();
        }
    }
}
