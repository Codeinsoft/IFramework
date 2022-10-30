using IFramework.Domain.User;
using IFramework.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IFramework.Infrastructure.Persistence.Repository
{
    public class UserRepository : EFCoreGenericRepository<User, Guid>, IUserRepository
    {
        public UserRepository() 
        {

        }

        public User GetUserByEmail(string email, bool getTrash)
        {
            IQueryable<User> userQueryable = Queryable(getTrash);
            return userQueryable.Include("UserInfo").Include("Role").FirstOrDefault(p => p.Email.Equals(email));
        }
        public User GetUserById(Guid id, bool getTrash)
        {
            IQueryable<User> userQueryable = Queryable(getTrash);
            return userQueryable.Include(p => p.Role).FirstOrDefault(p => p.Id.Equals(id));
        }

        public List<User> GetAllInclude(bool getTrash, int pageNo, int pageSize)
        {
            IQueryable<User> users= GetPaging(getTrash, pageNo, pageSize);
            //IQueryable<User> userQueryable = Queryable(getTrash);
            return users.Include(p=>p.UserInfo).Include(p => p.Role).ToList();
        }
        public bool CheckEmailRegistered(string email)
        {
            IQueryable<User> userQueryable = Queryable(false);
            return userQueryable.Include("UserInfo").Any(p => p.Email.Equals(email));
        }
    }
}
