using System;

using Microsoft.Extensions.DependencyInjection;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;

using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;


namespace IFramework.Api.Core.Extensions
{
    public static class CastleIoCExtension
    {
        public static IServiceProvider AddCastleIoC(this IServiceCollection services, IWindsorInstaller windsorInstaller)
        {
            IoCResolver.Instance.Install(windsorInstaller);
            return WindsorRegistrationHelper.CreateServiceProvider(IoCResolver.Instance.Container, services);
        }
    }
}
