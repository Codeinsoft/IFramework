using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;

namespace IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver
{
    public class IoCResolver : IDisposable
    {
        private static IoCResolver _instance;
        public WindsorContainer Container { get; private set; }
        public IoCResolver()
        {
            InitializeContainer();
        }

        private void InitializeContainer()
        {
            Container = new WindsorContainer();
        }

        public static IoCResolver Instance
        {
            get
            {
                return _instance ?? (_instance = new IoCResolver());
            }
        }

        public T ReleaseInstance<T>() where T : class
        {
            try
            {
                return Container.Resolve<T>();
            }
#pragma warning disable CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            catch (Exception ex)
#pragma warning restore CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            {
                return null;
            }
        }

        public T ReleaseInstance<T>(IEnumerable<KeyValuePair<string, object>> arguments) where T : class
        {
            try
            {
                return Container.Resolve<T>(arguments);
            }
#pragma warning disable CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            catch (Exception ex)
#pragma warning restore CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            {
                return null;
            }
        }

        public object ReleaseInstance(Type tip)
        {
            try
            {
                return Container.Resolve(tip);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Install(IWindsorInstaller[] installers)
        {
            Container.Install(installers);
        }

        public void Install(IWindsorInstaller installer)
        {
            Container.Install(installer);
        }

        public void Register(IRegistration registerItem)
        {
            Container.Register(registerItem);
        }

        public void Dispose()
        {
            if (Container != null)
                Container.Dispose();
            Container = null;
        }
    }
}
