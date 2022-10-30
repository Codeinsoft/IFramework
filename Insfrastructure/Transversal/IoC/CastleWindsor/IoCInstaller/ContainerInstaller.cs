using System.Reflection;

using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using IFramework.Application.Authorization.Abstract;
using IFramework.Application.Authorization.Concrete;
using IFramework.Application.Contract.Core.Service;
using IFramework.Application.Contract.UserDto;
using IFramework.Application.Core.Abstract;
using IFramework.Application.Core.Concrete;
using IFramework.Application.User.Abstract;
using IFramework.Application.User.Concrete;
using IFramework.Domain.Core.Repositories;
using IFramework.Infra.Transversal.Validation;
using IFramework.Infra.Transversal.Validation.Fluent;
using IFramework.Infra.Transversal.Validation.Fluent.Attributes;
using IFramework.Infra.Transversal.Validation.Fluent.Validators;
using IFramework.Infra.Transversal.Validation.Fluent.Validators.Abstract;
using IFramework.Infrastructure.Persistence.EFCore;
using IFramework.Infrastructure.Persistence.Repository;
using IFramework.Infrastructure.Transversal.Aspect.ExceptionAspect;
using IFramework.Infrastructure.Transversal.Cache;
using IFramework.Infrastructure.Transversal.Cache.Attributes;
using IFramework.Infrastructure.Transversal.Cache.Interfaces;
using IFramework.Infrastructure.Transversal.Cache.Redis.Config;
using IFramework.Infrastructure.Transversal.Cache.Redis.Service;
using IFramework.Infrastructure.Transversal.Map.Abstract;
using IFramework.Infrastructure.Transversal.Mapper.AutoMapper;
using IFramework.Infrastructure.Utility.Cryptography;
using IFramework.Infrastructure.Transversal.Aspect.Authorization;
using IFramework.Infrastructure.Transversal.Aspect.Log;
using IFramework.Infrastructure.Transversal.Aspect.UnitOfWork;
using IFramework.Infrastructure.Persistence.UnitOfWork;
using IFramework.Application.Contract.Core.Response;
using IFramework.Infrastructure.Utility.Authentication;
using IFramework.Infra.Transversal.Log.NLog;
using IFramework.Infrastructure.Transversal.Logger;
using Microsoft.AspNetCore.Http;

namespace IFramework.Infra.Transversal.IoC.CastleWindsor
{
    public class ContainerInstaller : IWindsorInstaller
    {
        public virtual void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            // Mapper registration
            container.Register(Component.For<IMap>().ImplementedBy<AutoMapperMap>().LifestyleTransient(),
                Component.For<ILog>().ImplementedBy<NLogger>().LifestyleTransient(),
                Component.For<ITokenProvider>().ImplementedBy<TokenProvider>().LifestyleTransient(),
                Component.For<IHttpContextAccessor>().ImplementedBy<HttpContextAccessor>().LifestyleTransient()
                );

            RegisterInterceptors(container);
            RegisterRepositories(container);
            RegisterApplication(container);
            RegisterDomainTransferObject(container);
            RegisterCrossCuttings(container);

        }

        private static void RegisterCrossCuttings(IWindsorContainer container)
        {
            // Cryptography
            container.Register(Component.For<ICryptography>().ImplementedBy<Cryptography>());

            // Cache
            container.Register(
                Component.For<ICacheProvider>().ImplementedBy<CacheProvider>(),
                Component.For(typeof(IRedisConnectionFactory)).ImplementedBy(typeof(RedisConnectionFactory)).LifestyleTransient()
            );

            // Validation
            // Fluent Validation
            container.Register(
                Component.For<IIFrameworkValidator>().ImplementedBy<FluentValidator>()
            );
            container.Register(
                Classes.FromAssembly(Assembly.GetAssembly(typeof(BaseValidator<>))).BasedOn(typeof(BaseValidator<>)).WithServiceFirstInterface().LifestyleTransient()
            );

        }

        private void RegisterRepositories(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssembly(Assembly.GetAssembly(typeof(UserRepository))).InSameNamespaceAs<UserRepository>().WithService.DefaultInterfaces().LifestyleTransient(),
                Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(EFCoreGenericRepository<,>)).LifestyleTransient(),
                Component.For(typeof(ICacheRepository<>)).ImplementedBy(typeof(RedisCacheRepository<>)).LifestyleTransient()
                );
        }
        private void RegisterDomain(IWindsorContainer container)
        {

        }
        private void RegisterApplication(IWindsorContainer container)
        {
            container.Register(
                // TODO Roleservis register edilecek mi?
                Classes.FromAssembly(Assembly.GetAssembly(typeof(UserService))).InSameNamespaceAs<UserService>().WithService.DefaultInterfaces().LifestyleTransient(),
                Component.For(typeof(IGenericService<,,>)).ImplementedBy(typeof(GenericService<,,>)),
                //Component.For<IUserService>().ImplementedBy<UserService>(),
                Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>()
                );
        }
        public void RegisterDomainTransferObject(IWindsorContainer container)
        {
            container.Register(
            Component.For<UserDto>().ImplementedBy<UserDto>(),
            Component.For<RoleDto>().ImplementedBy<RoleDto>(),
            //Component.For(typeof(Core.Dto.Abstraction.PagedResult<>)).ImplementedBy(typeof(Core.Dto.Concrete.PagedResult<>)).LifestylePerWebRequest(),
            Component.For<ErrorMessageDto>().ImplementedBy<ErrorMessageDto>().LifestyleTransient(),
            Component.For<ResponseBase>().ImplementedBy<ResponseBase>(),
            Component.For(typeof(ResponseResult<>)).ImplementedBy(typeof(ResponseResult<>))
            //Component.For<IClientDto>().ImplementedBy<ClientDto>().LifestylePerWebRequest(),
            //Component.For<IRefreshTokenDto>().ImplementedBy<RefreshTokenDto>().LifestylePerWebRequest()
            );
        }

        public void RegisterInterceptors(IWindsorContainer container)
        {
            // interceptors
            container.Register(
                Component.For<ExceptionInterceptor>().LifestyleTransient(),
                Component.For<ValidationInterceptor>().LifestyleTransient(),
                Component.For<CachingInterceptor>().LifestyleTransient(),
                Component.For<AuthorizationAspect>().LifestyleTransient(),
                Component.For<LogInfoAspect>().LifestyleTransient(),
                Component.For<UnitOfWorkEFCoreInterceptor>().LifestyleTransient());
        }

        void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkEFCoreInterceptor)));
            }
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                // interceptors

                InterceptorReference exceptionInterceptor = new InterceptorReference(typeof(ExceptionInterceptor));
                InterceptorReference unitOfWorkEFCoreInterceptor = new InterceptorReference(typeof(UnitOfWorkEFCoreInterceptor));
                InterceptorReference logInfoInterceptor = new InterceptorReference(typeof(LogInfoAspect));

                InterceptorReference authorizationInterceptor = new InterceptorReference(typeof(AuthorizationAspect));

                InterceptorReference validationInterceptor = new InterceptorReference(typeof(ValidationInterceptor));
                InterceptorReference cachingInterceptor = new InterceptorReference(typeof(CachingInterceptor));

                handler.ComponentModel.Interceptors.AddLast(exceptionInterceptor);
                handler.ComponentModel.Interceptors.AddLast(unitOfWorkEFCoreInterceptor);
                handler.ComponentModel.Interceptors.AddLast(logInfoInterceptor);
                handler.ComponentModel.Interceptors.AddLast(authorizationInterceptor);
                handler.ComponentModel.Interceptors.AddLast(validationInterceptor);
                handler.ComponentModel.Interceptors.AddLast(cachingInterceptor);
            }
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {

                //if (method.IsDefined(typeof(LogInfoAttribute), true))
                //{
                //    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ApplicationServiceInterceptor)));
                //}
                if (method.IsDefined(typeof(UnitOfWorkAttribute), true))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkEFCoreInterceptor)));
                    return;
                }
            }
        }
    }
}
