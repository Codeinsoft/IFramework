using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using IFramework.Domain.Core.Entities;
using IFramework.Domain.Core.Repositories;
using IFramework.Infrastructure.Transversal.Resources.Languages;

namespace IFramework.Infrastructure.Persistence.EFCore
{
    public class EFCoreGenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : AuditableAndActivableEntity<TPrimaryKey>, IAggregateRoot where TPrimaryKey : struct
    {
        protected EFDbContext _dbContext => EFCoreUnitOfWork.Current.Context;
        //private readonly DbSet<TEntity> _dbSet = _dbContext.Set<TEntity>();

        public TPrimaryKey Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public Task<TPrimaryKey> AddAsnyc(TEntity entity)
        {
            return Task.Run(() => { return Add(entity); });
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Attach(entity);
            }
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() => { Delete(entity); });
        }

        public IQueryable<TEntity> Find(bool getTrash, int pageNo, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null) return null;
            IQueryable<TEntity> queryable = getTrash ? _dbContext.Set<TEntity>().Where(p => p.Status == Status.Delete) : _dbContext.Set<TEntity>().Where(p => p.Status != Status.Delete);
            return queryable.Where(predicate).Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>().SingleOrDefault(filter);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() => { return Get(filter); });
        }

        public TEntity GetById(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return Task.Run(() => { return GetById(id); });
        }

        public List<TEntity> GetList(bool getTrash)
        {
            IQueryable<TEntity> queryable = getTrash ? _dbContext.Set<TEntity>().Where(p => p.Status == Status.Delete) : _dbContext.Set<TEntity>().Where(p => p.Status != Status.Delete);
            return queryable.ToList();
        }

        public Task<List<TEntity>> GetListAsync(bool getTrash)
        {
            return Task.Run(() => { return GetList(getTrash); });
        }

        public IQueryable<TEntity> GetPaging(bool getTrash, int pageNo, int pageSize)
        {
            IQueryable<TEntity> queryable = getTrash ? _dbContext.Set<TEntity>().Where(p => p.Status == Status.Delete) : _dbContext.Set<TEntity>().Where(p => p.Status != Status.Delete);
            return queryable.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<TEntity> Queryable(bool getTrash)
        {
            IQueryable<TEntity> queryable = getTrash ? _dbContext.Set<TEntity>().Where(p => p.Status == Status.Delete) : _dbContext.Set<TEntity>().Where(p => p.Status != Status.Delete);
            return queryable;
        }

        public void Remove(TEntity entity)
        {
            TEntity dataEntity = GetById(entity.Id);
            if (dataEntity == null) throw new SqlNullValueException(ErrorMessage.NotFound);
            dataEntity.Deleted();
            _dbContext.Set<TEntity>().Update(dataEntity);
        }

        public Task RemoveAsnyc(TEntity entity)
        {
            return Task.Run(() => { Remove(entity); });
        }

        public TEntity Update(TEntity entity)
        {
            TEntity entityDb= GetById(entity.Id);
            _dbContext.Entry(entityDb).CurrentValues.SetValues(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsnyc(TEntity entity)
        {
            return Task.Run(() => { return Update(entity); });
        }
    }
}
