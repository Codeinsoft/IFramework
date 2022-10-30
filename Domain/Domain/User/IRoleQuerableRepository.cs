using System;
using IFramework.Domain.Core.Repositories;

namespace IFramework.Domain.User
{
    public interface IRoleQuerableRepository : IQuerableRepository<Role, Guid>
    {
    }
}
