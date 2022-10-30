using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IFramework.Domain.Core.Entities;

namespace IFramework.Domain.Core.Repositories
{
    public interface IQuerableRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>, IAggregateRoot
    {
        IQueryable<TEntity> Queryable(bool getTrash);

        TEntity Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);

        TEntity GetById(TPrimaryKey id);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        IQueryable<TEntity> Find(bool getTrash, int pageNo, int pageSize, Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> GetPaging(bool getTrash, int pageNo, int pageSize);

        List<TEntity> GetList();
        Task<List<TEntity>> GetListAsync();
    }
}
