using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IFramework.Domain.Core.Entities;

namespace IFramework.Domain.Core.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : AuditableAndActivableEntity<TPrimaryKey>, IAggregateRoot 
    {
        TPrimaryKey Add(TEntity entity);
        Task<TPrimaryKey> AddAsnyc(TEntity entity);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);

        void Remove(TEntity entity);
        Task RemoveAsnyc(TEntity entity);

        IQueryable<TEntity> Queryable(bool getTrash);

        TEntity Get(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);

        TEntity GetById(TPrimaryKey id);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsnyc(TEntity entity);

        IQueryable<TEntity> Find(bool getTrash, int pageNo, int pageSize, Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> GetPaging(bool getTrash, int pageNo, int pageSize);

        List<TEntity> GetList(bool getTrash);
        Task<List<TEntity>> GetListAsync(bool getTrash);
    }
}
