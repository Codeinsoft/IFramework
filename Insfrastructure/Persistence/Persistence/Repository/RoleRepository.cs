using IFramework.Domain.User;
using IFramework.Infrastructure.Persistence.EFCore;
using System;

namespace IFramework.Infrastructure.Persistence.Repository
{
    public class RoleRepository : EFCoreGenericRepository<Role, Guid>, IRoleRepository
    {
        public RoleRepository()
        {

        }    
    }
}
