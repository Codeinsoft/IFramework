using System.Threading.Tasks;
using IFramework.Domain.Core.Entities;

namespace IFramework.Domain.Core.Repositories
{
    public interface ICommandRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>, IAggregateRoot
    {
        TPrimaryKey Add(TEntity entity);
        Task<TPrimaryKey> AddAsnyc(TEntity entity);

        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);

        void Remove(TEntity entity);
        Task RemoveAsnyc(TEntity entity);

        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsnyc(TEntity entity);
    }
}
