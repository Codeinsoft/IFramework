using System;
using IFramework.Domain.Core.Repositories;

namespace IFramework.Domain.User
{
    public interface IUserQuerableRepository : IQuerableRepository<User,Guid>
    {
        User GetUserByEmail(string email, bool getTrash);
    }
}
