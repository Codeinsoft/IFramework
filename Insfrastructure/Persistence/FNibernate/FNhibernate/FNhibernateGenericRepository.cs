using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

using NHibernate;

using IFramework.Infrastructure.Transversal.Resources.Languages;
using IFramework.Infrastructure.Utility.Check;
using IFramework.Domain.Core.Repositories;
using IFramework.Domain.Core.Entities;

namespace IFramework.Infrastructure.Persistence.FNhibernate
{
    public class FNhibernateGenericRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : AuditableAndActivableEntity<TPrimaryKey>, IAggregateRoot where TPrimaryKey : struct
    {
        /// <summary>
        /// Veri tabanı işlemleri için nhibernate session nesnesini verir.
        /// </summary>
        protected ISession Session => FNhibernateUnitOfWork.Current.Session;

        /// <summary>
        /// Verilen entity'yi veri tabanına kayıt işlemini yapar.
        /// </summary>
        /// <param name="entity">Kayıt edilmek istenen entity nesnesi verilmelidir.</param>
        public TPrimaryKey Add(TEntity entity)
        {
            ParameterCheck.ThrowExceptionIsNull(entity, "entity");
            object id = Session.Save(entity);
            return (TPrimaryKey)id;
        }
        public Task<TPrimaryKey> AddAsnyc(TEntity entity)
        {
            return Task.Run(() => { return Add(entity); });
        }

        /// <summary>
        /// Silinmek istenen entity'nin nesnesi verilerek entity'nin veri tabanında gerçek anlamda silinmesi işlemini yapar.
        /// </summary>
        /// <param name="entity">Silinmek istenen entity'nin nesnesi verilmelidir.</param>
        public void Remove(TEntity entity)
        {
            ParameterCheck.ThrowExceptionIsNull(entity, "entity");
            TEntity dataEntity = GetById(entity.Id);
            if (dataEntity == null) throw new SqlNullValueException(ErrorMessage.NotFound);
            dataEntity.Deleted();
            Session.Update(dataEntity);
        }

        public Task RemoveAsnyc(TEntity entity)
        {
            return Task.Run(() => { Remove(entity); });
        }

        /// <summary>
        /// Silinmek istenen entity'nin nesnesi verilerek entity'nin veri tabanında gerçek anlamda silinmesi işlemini yapar.
        /// </summary>
        /// <param name="entity">Silinmek istenen entity'nin nesnesi verilmelidir.</param>
        public void Delete(TEntity entity)
        {
            ParameterCheck.ThrowExceptionIsNull(entity, "entity");
            Session.Delete(entity);
        }
        public Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() => { Delete(entity); });
        }

        /// <summary>
        /// Primary key değeri verilen entity'yi veri tabanından getirme işlemini yapar.
        /// </summary>
        /// <param name="id">Verileri istenilen entity'nin primary key değeri verilmelidir.</param>
        /// <returns>TEntity tipinden nesne geri dönülecektir.</returns>
        public TEntity GetById(TPrimaryKey id)
        {
            ParameterCheck.ThrowExceptionIsNullOrEmpty(id, "id");
            TEntity result = Session.Get<TEntity>(id);
            return result;
        }
        public Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return Task.Run(() => { return GetById(id); });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            ParameterCheck.ThrowExceptionIsNull(entity, "entity");
            Session.Update(entity);
            return GetById(entity.Id);
        }
        public Task<TEntity> UpdateAsnyc(TEntity entity)
        {
            return Task.Run(() => { return Update(entity); });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getTrash"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Find(bool getTrash, int pageNo, int pageSize, Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null) return null;
            IQueryable<TEntity> queryable = getTrash ? Session.Query<TEntity>().Where(p => p.Status == Status.Delete) : Session.Query<TEntity>().Where(p => p.Status != Status.Delete);
            return queryable.Where(predicate);
        }

        /// <summary>
        /// İşlem yapılan Tentity tipinden verilen sayfa bilgilerine göre sayfadaki kayıtları IQueryable tipinden liste olarak döner.
        /// </summary>
        /// <param name="getTrash"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetPaging(bool getTrash, int pageNo, int pageSize)
        {
            IQueryable<TEntity> queryable = getTrash ? Session.Query<TEntity>().Where(p => p.Status == Status.Delete) : Session.Query<TEntity>().Where(p => p.Status != Status.Delete);

            return queryable.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// TEntity tipindeki değerler ile ilgili veri tabanı sorgulamalarının yapılmasını sağlar.
        /// </summary>
        /// <returns>IQueryable tipinden sorgulababilir nesne geri dönmektedir.
        /// Sorgu çalıştırılmamıştır, istenilen sorgulamalar eklendikten sonra ilk çağırım yapıldığında sorgu çalışacaktır. Örn: ToList()
        /// </returns>
        /// <param name="getTrash"></param>
        public IQueryable<TEntity> Queryable(bool getTrash)
        {
            return getTrash ? Session.Query<TEntity>().Where(p => p.Status == Status.Delete) : Session.Query<TEntity>().Where(p => p.Status != Status.Delete);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Session.Query<TEntity>().FirstOrDefault(filter);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() => { return Get(filter); });
        }

        public List<TEntity> GetList(bool getTrash)
        {
            var querable = getTrash? Session.Query<TEntity>().Where(p => p.Status == Status.Delete) : Session.Query<TEntity>().Where(p => p.Status != Status.Delete);
            return querable.ToList();
        }

        public Task<List<TEntity>> GetListAsync(bool getTrash)
        {
            return Task.Run(() => { return GetList(getTrash); });
        }
    }
}
