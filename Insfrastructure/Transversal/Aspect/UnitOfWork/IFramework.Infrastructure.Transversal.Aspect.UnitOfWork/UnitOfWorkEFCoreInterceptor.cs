using Castle.Core.Internal;
using Castle.DynamicProxy;
using IFramework.Domain.Core.Entities;
using IFramework.Infrastructure.Persistence.EFCore;
using IFramework.Infrastructure.Persistence.UnitOfWork;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Utility.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace IFramework.Infrastructure.Transversal.Aspect.UnitOfWork
{
    public class UnitOfWorkEFCoreInterceptor : IInterceptor
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly EFDbContext _dbContext;
        /// <summary>
        /// Creates a new UnitOfWorkEFCoreInterceptor object.
        /// </summary>
        /// <param name="dbContext">EFDbContext.</param>
        public UnitOfWorkEFCoreInterceptor(EFDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetStringBearerToken()
        {
            StringValues authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (authorizationHeader.Count > 0 && !string.IsNullOrEmpty(authorizationHeader.ToString()))
            {
                return _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            }
            return null;
        }
        /// <summary>
        /// Intercepts a method.
        /// </summary>
        /// <param name="invocation">Method invocation arguments</param>
        public void Intercept(IInvocation invocation)
        {

            //If there is a running transaction, just run the method
            if (EFCoreUnitOfWork.Current != null || !RequiresDbConnection(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                EFCoreUnitOfWork.Current = new EFCoreUnitOfWork(_dbContext);

                //EFCoreUnitOfWork.Current.BeginTransaction();

                try
                {
                    invocation.Proceed();

                    string token = GetStringBearerToken();
                    var entries = _dbContext.ChangeTracker
                        .Entries()
                        .Where(e => e.Entity is IAuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
                    Guid userId = Guid.Empty;
                    if (!string.IsNullOrEmpty(token))
                    {
                        ITokenProvider tokenProvider = IoCResolver.Instance.ReleaseInstance<ITokenProvider>();
                        Claim sIdClaim = tokenProvider.GetTokenClaims(token).FirstOrDefault(p => p.Type == ClaimTypes.Sid);
                        userId = Guid.Parse(sIdClaim.Value);
                    }
                    foreach (var entityEntry in entries)
                    {
                        (entityEntry.Entity as IAuditableEntity).UpdatedDate = DateTime.Now;
                        (entityEntry.Entity as IAuditableEntity).UpdatedBy = userId.ToString();

                        if (entityEntry.State == EntityState.Added)
                        {
                            ((IAuditableEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                            ((IAuditableEntity)entityEntry.Entity).CreatedBy = userId.ToString();
                        }
                    }


                    int result = _dbContext.SaveChanges();
                    //EFCoreUnitOfWork.Current.Commit();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    try
                    {
                        //EFCoreUnitOfWork.Current.Rollback();
                    }
                    catch
                    {
                        throw;
                    }
                    throw ex;
                }
            }
            finally
            {
                EFCoreUnitOfWork.Current = null;
            }
        }

        private static bool RequiresDbConnection(MethodInfo methodInfo)
        {
            if (methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true))
            {
                return true;
            }

            if (UnitOfWorkHelper.IsRepositoryMethod(methodInfo))
            {
                return true;
            }

            return false;
        }
    }
}
