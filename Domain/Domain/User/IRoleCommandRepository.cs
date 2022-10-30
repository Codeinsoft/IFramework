using System;
using IFramework.Domain.Core.Repositories;

namespace IFramework.Domain.User
{
    public interface IRoleCommandRepository : ICommandRepository<Role,Guid>
    {
    }
}
