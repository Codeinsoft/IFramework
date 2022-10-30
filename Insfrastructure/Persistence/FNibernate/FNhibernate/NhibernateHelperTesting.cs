using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using IFramework.Infrastructure.Persistence.FNhibernate.Mapping;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using Microsoft.Extensions.Options;
using IFramework.Infrastructure.Utility.Configuration;

namespace IFramework.Infrastructure.Persistence.FNhibernate
{
    public class NhibernateHelperTesting
    {
        public NhibernateHelperTesting()
        {
        }

        /// <summary>
        /// Nhibernate Session oluşturulması için SessionFactory oluşturulması işlemini yapar.
        /// </summary>
        /// <returns>NHibernate Session Factory</returns>
        public ISessionFactory CreateNhSessionFactory()
        {
            //var mapping = new PersistenceModel();
            //mapping.AddMappingsFromAssembly(Assembly.GetAssembly(typeof(UserMap)));
            //var connStr = ConfigurationManager.ConnectionStrings["CbFramework"].ConnectionString;
            ////var config = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(connStr).ShowSql()).BuildConfiguration();
            //var config = Fluently.Configure().Database(SQLiteConfiguration.Standard.InMemory().ShowSql()).BuildConfiguration();
            //mapping.Configure(config);

            //#region Veri tabanı oluşturulması için kullanılır.
            //if (bool.Parse(ConfigurationManager.AppSettings["GenerateDatabase"]))
            //{
            //    var exporter = new SchemaExport(config);
            //    exporter.Execute(false, true, false);
            //}
            //#endregion

            //return config.BuildSessionFactory();


            var mapping = new PersistenceModel();
            mapping.AddMappingsFromAssembly(Assembly.GetAssembly(typeof(UserMap)));
            //var config = Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(connStr).ShowSql()).BuildConfiguration();
            var config = Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.SqliteDbFileName))
                //.Mappings(m=>m.FluentMappings.AddFromAssembly(typeof(UserMap).Assembly))
                //.ExposeConfiguration(x => x.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close"))
                //.CurrentSessionContext<ThreadStaticSessionContext>()
                .BuildConfiguration();
            mapping.Configure(config);

            #region Veri tabanı oluşturulması için kullanılır.
            if (IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.GenerateDatabase)
            {
                var exporter = new SchemaExport(config);
                exporter.Execute(true, true, false);
            }
            #endregion

            ISessionFactory sessionFactory = config.BuildSessionFactory();
            // SeedData(sessionFactory);
            return sessionFactory;
        }


        /// <summary>
        /// Unit Test işlemi için veritabanına test verilerini yükler.
        /// </summary>
        /// <param name="sessionFactory"></param>
        public static void SeedData(ISessionFactory sessionFactory)
        {
            ISession session = sessionFactory.OpenSession();
            ITransaction transaction = session.BeginTransaction();

            transaction.Commit();
            session.Close();
        }
    }
}