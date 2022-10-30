using System;
using System.Collections.Generic;
using IFramework.Domain.Core.Repositories;

namespace IFramework.Domain.User
{
    public interface IUserRepository:IRepository<User,Guid>
    {
        User GetUserByEmail(string email, bool getTrash);
        User GetUserById(Guid id, bool getTrash);
        List<User> GetAllInclude(bool getTrash, int pageNo, int pageSize);
        bool CheckEmailRegistered(string email);
    }
}
