using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using IFramework.Application.Core.Abstract;
using IFramework.Application.Contract.Core.Request;
using IFramework.Domain.Core.Entities;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Cache.Attributes;
using IFramework.Application.Contract.Core.Response;

namespace IFramework.Api.Core.Api
{
    /// <summary>
    /// test
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class BaseApiController<TDto, TEntity, TPrimaryKey> : ControllerBase where TEntity : AuditableAndActivableEntity<TPrimaryKey>, IAggregateRoot where TDto : RequestBase where TPrimaryKey : struct
    {
        protected readonly IGenericService<TDto, TEntity, TPrimaryKey> GenericService = IoCResolver.Instance.ReleaseInstance<IGenericService<TDto, TEntity, TPrimaryKey>>();

        /// <summary>
        /// Güncelleme işlemi için kullanılabilir.
        /// </summary>
        /// <param name="dataRequest"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public virtual ResponseResult<TDto> Update(TDto dataRequest)
        {
            return GenericService.Update(dataRequest);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="dataRequest"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public virtual ResponseResult<TPrimaryKey> Add(TDto dataRequest)
        {
            return GenericService.Add(dataRequest);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Remove")]
        public virtual ResponseBase Remove(IdRequest<TPrimaryKey> request)
        {
            return GenericService.Remove(request);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RemoveMultiple")]
        public virtual ResponseBase RemoveMultiple(ICollection<IdRequest<TPrimaryKey>> request)
        {
            return GenericService.RemoveMultiple(request);
        }
        /// <summary>
        /// test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Delete")]
        public virtual ResponseBase Delete(IdRequest<TPrimaryKey> request)
        {
            return GenericService.Delete(request);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("DeleteMultiple")]
        public virtual ResponseBase DeleteMultiple(ICollection<IdRequest<TPrimaryKey>> request)
        {
            return GenericService.DeleteMultiple(request);
        }
        /// <summary>
        /// test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Get")]
        public virtual ResponseResult<TDto> Get(IdRequest<TPrimaryKey> request)
        {
            return GenericService.Get(request);
        }

        /// <summary>
        /// test
        /// </summary>
        /// <param name="dataRequest"></param>
        /// <returns></returns>
        [HttpPost("GetPaging")]
        public virtual ResponseResult<PagingResponse<TDto>> GetPaging(PagingRequest dataRequest)
        {
            return GenericService.GetPaging(dataRequest);
        }

        ///// <summary>
        ///// test
        ///// </summary>
        ///// <param name="dataRequest"></param>
        ///// <returns></returns>
        //[BasicAuthentication]
        //[System.Web.Http.HttpPost]
        //public virtual ResponseResult<PagingResponse<TDto>> FindPaging(FindPagingRequest dataRequest)
        //{
        //    return GenericService.Find(dataRequest);
        //}

        ///// <summary>
        ///// test
        ///// </summary>
        ///// <param name="dataRequest"></param>
        ///// <returns></returns>
        //[BasicAuthentication]
        //[System.Web.Http.HttpPost]
        //public virtual ResponseResult<PagingResponse<dynamic>> FindDynamic(FindDynamicPagingRequest dataRequest)
        //{
        //    return GenericService.FindDynamic(dataRequest);
        //}

        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public virtual ResponseResult<List<TDto>> GetAll(RequestBase dataRequest)
        {
            return GenericService.GetAll(dataRequest);
        }
    }
}