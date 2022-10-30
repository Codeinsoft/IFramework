using System.Collections.Generic;
using System.Linq;

using IFramework.Domain.Core.Entities;
using IFramework.Domain.Core.Repositories;
using IFramework.Infrastructure.Transversal.Map.Abstract;
using IFramework.Infrastructure.Transversal.Logger.Attributes;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Application.Core.Abstract;
using IFramework.Application.Contract.Core.Request;

using IFramework.Infrastructure.Transversal.Cache.Attributes;
using IFramework.Infrastructure.Transversal.Aspect.Authorization;
using IFramework.Application.Contract.Core.Response;
using IFramework.Infra.Transversal.Validation.Attributes;

namespace IFramework.Application.Core.Concrete
{
    /// <summary>
    /// Generic olarak hazırlanan base service'leri barındırmaktadır.
    /// </summary>
    /// <typeparam name="TDto">Base service'lerde Data Transfer Object olarak kullanılması istenilen tip verilmelidir.</typeparam>
    /// <typeparam name="TEntity">Base service'lerde entity olarak kullanılması istenilen tip verilmelidir.</typeparam>
    /// <typeparam name="TPrimaryKey">Base service'lerde kullanılması istenilen entity'nin primary key tipi verilmelidir.</typeparam>
    [LogException]
    [LogInfo]
    [LogPerformanceInfo]
    public class GenericService<TDto, TEntity, TPrimaryKey> :
        IGenericService<TDto, TEntity, TPrimaryKey> where TEntity : AuditableAndActivableEntity<TPrimaryKey>, IAggregateRoot where TDto : RequestBase where TPrimaryKey : struct
    {
        protected readonly IRepository<TEntity, TPrimaryKey> _repository= IoCResolver.Instance.ReleaseInstance<IRepository<TEntity, TPrimaryKey>>();
        protected readonly IMap _map;

        public GenericService(
            IMap map)
        {

            _map = map;
        }
        
        /// <summary>
        /// Generic olarak verilen entity tipinde nesnenin repository aracılığı ile eklenmesi işlemini yapar.
        /// NOT : Default olarak request validation ve Authorization aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen genericdto tipinde nesne verilmelidir.</param>
        /// <returns>Eklenen nesnenin id değerini dönmektedir.</returns>
        [Validation]
        [CustomAuthorize(Action = ActionType.Create, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseResult<TPrimaryKey> Add(TDto request)
        {
            TEntity entity = _map.Mapping<TEntity>(request);
            ResponseResult<TPrimaryKey> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<TPrimaryKey>>();
            TPrimaryKey id = _repository.Add(entity);
            response.Result = id;
            return response;
        }

        /// <summary>
        /// Generic olarak verilen entity tipinde nesnenin repository aracılığı ile güncellenmesi işlemini yapar.
        /// NOT : Default olarak request validation ve Authorization aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen genericdto tipinde nesne verilmelidir.</param>
        /// <returns>Güncellenen nesneyi dönmektedir.</returns>
        [Validation]
        [CustomAuthorize(Action = ActionType.Update, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseResult<TDto> Update(TDto request)
        {
            TEntity entity = _map.Mapping<TEntity>(request);
            ResponseResult<TDto> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<TDto>>();
            TEntity result = _repository.Update(entity);
            response.Result = _map.Mapping<TDto>(result);
            return response;
        }

        /// <summary>
        /// Generic olarak verilen entity primarykey değerine ait nesneyi repository aracılığı ile getirir.
        /// NOT : Default olarak request validation, Authorization ve Cache aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen generic primary key tipinde nesne verilmelidir.</param>
        /// <returns>Verilen primary key'e karşılık gelen nesneyi dto tipinde geri döner.</returns>
        [Validation]
        [Cached(CacheAction = CacheAction.Get)]
        [CustomAuthorize(Action = ActionType.Select, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseResult<TDto> Get(IdRequest<TPrimaryKey> request)
        {
            ResponseResult<TDto> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<TDto>>();
            TEntity entity = _repository.GetById(request.Id);

            TDto result = _map.Mapping<TDto>(entity);
            response.Result = result;
            return response;
        }

        /// <summary>
        /// Generic olarak verilen entity primarykey değerine ait nesneyi repository aracılığı ile gerçek olarak siler.
        /// NOT : Default olarak request validation ve Authorization aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen generic primary key tipinde nesne verilmelidir.</param>
        /// <returns>Verilen primary key'e karşılık gelen nesne silinir ise işlem success olarak dönecektir.</returns>
        [Validation]
        [CustomAuthorize(Action = ActionType.Delete, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseBase Delete(IdRequest<TPrimaryKey> request)
        {
            ResponseBase response = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
            TEntity entity = _repository.GetById(request.Id);

            _repository.Delete(entity);
            return response;
        }

        /// <summary>
        /// Generic olan entity primarykey tipinde verilen listede bulunan elemanları repository aracılığı ile gerçek olarak siler.
        /// NOT : Default olarak request validation ve Authorization aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen generic primary key tipinde liste verilmelidir.</param>
        /// <returns>Verilen primary key'lere karşılık gelen nesneler silinir ise işlem success olarak dönecektir.</returns>
        [Validation]
        [CustomAuthorize(Action = ActionType.Delete, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseBase DeleteMultiple(ICollection<IdRequest<TPrimaryKey>> request)
        {
            foreach (IdRequest<TPrimaryKey> primaryKey in request)
            {
                Delete(primaryKey);
            }
            ResponseBase result = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
            return result;
        }

        /// <summary>
        /// Generic olarak verilen entity primarykey değerine ait nesneyi repository aracılığı ile silindi olarak işaretler. Çöp kutusuna taşımış olur.
        /// NOT : Default olarak request validation ve Authorization aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen generic primary key tipinde nesne verilmelidir.</param>
        /// <returns>Verilen primary key'e karşılık gelen nesne silindi olarak işaretlenir ise işlem success olarak dönecektir.</returns>
        [Validation]
        [CustomAuthorize(Action = ActionType.Delete, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseBase Remove(IdRequest<TPrimaryKey> request)
        {
            TEntity entity = _repository.GetById(request.Id);
            // TODO 
            //if (entity == null) throw new ValidationException(ErrorMessage.NotFound, ExceptionCodeConstants.NotFoundCode);

            _repository.Remove(entity);
            ResponseBase result = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
            return result;
        }

        /// <summary>
        /// Generic olan entity primarykey tipinde verilen listede bulunan elemanları repository aracılığı ile silindi olarak işaretler. Çöp kutusuna taşımış olur.
        /// NOT : Default olarak request validation ve Authorization aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Class instance alınırken belirlenen generic primary key tipinde liste verilmelidir.</param>
        /// <returns>Verilen primary key'lere karşılık gelen nesneler silindi olarak işaretlenir ise işlem success olarak dönecektir.</returns>
        [Validation]
        [CustomAuthorize(Action = ActionType.Delete, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseBase RemoveMultiple(ICollection<IdRequest<TPrimaryKey>> request)
        {
            foreach (IdRequest<TPrimaryKey> primaryKey in request)
            {
                Remove(primaryKey);
            }
            ResponseBase result = IoCResolver.Instance.ReleaseInstance<ResponseBase>();
            return result;
        }

        /// <summary>
        /// Verilen paging parametrelerine göre dataları geri dönecektir.
        /// NOT : Default olarak request validation, Authorization ve Cache aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">İstenilen paging parametreleri set edilip verilmelidir.</param>
        /// <returns>Verilen paging parametrelerine göre dataları geri dönecektir.</returns>
        [Validation]
        [Cached(CacheAction = CacheAction.Get)]
        [CustomAuthorize(Action = ActionType.Select, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseResult<PagingResponse<TDto>> GetPaging(PagingRequest request)
        {
            ResponseResult<PagingResponse<TDto>> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<PagingResponse<TDto>>>();
            int totalRowCount = _repository.Queryable(request.GetTrash).Count(p => p.Status == Status.Active || p.Status == Status.Passive);
            List<TEntity> result = _repository.GetPaging(request.GetTrash, request.PageNo, request.PageRowCount).ToList();
            List<TDto> resultDto = _map.Mapping<List<TDto>>(result);
            response.Result = new PagingResponse<TDto>();
            response.Result.PageRowCount = request.PageRowCount;
            response.Result.TotalRowCount = totalRowCount;
            response.Result.List = resultDto;
            return response;
        }

        /// <summary>
        /// Class instance alınırken belirlenen entity tipinde db'de bulunan tüm dataları geri dönecektir.
        /// NOT : Default olarak request validation, Authorization ve Cache aspect'leri eklenmiştir. Bunlar kullanılmak istenmediği takdirde override edilmelidir.
        /// </summary>
        /// <param name="dto">Silindi olarak işaretlenenlerin getirilmesi ile ilgili ayar yapılarak requestbase tipinde nesne verilmelidir.</param>
        /// <returns>Class instance alınırken belirlenen entity tipinde db'de bulunan tüm dataları geri dönecektir.</returns>
        [Validation]
        [Cached(CacheAction = CacheAction.Get)]
        [CustomAuthorize(Action = ActionType.Select, Container = "", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public virtual ResponseResult<List<TDto>> GetAll(RequestBase request)
        {
            List<TEntity> entities = _repository.GetList(request.GetTrash);
            ResponseResult<List<TDto>> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<List<TDto>>>();
            response.Result = _map.Mapping<List<TDto>>(entities);
            return response;
        }
    }
}
