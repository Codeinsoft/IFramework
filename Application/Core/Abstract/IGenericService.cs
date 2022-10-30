using System.Collections.Generic;

using IFramework.Application.Contract.Core.Service;
using IFramework.Application.Contract.Core.Request;
using IFramework.Domain.Core.Entities;
using IFramework.Application.Contract.Core.Response;

namespace IFramework.Application.Core.Abstract
{
    public interface IGenericService<TDto, TEntity, TPrimaryKey> : IApplicationService where TEntity : AuditableAndActivableEntity<TPrimaryKey>, IAggregateRoot where TDto : RequestBase where TPrimaryKey : struct
    {
        ResponseResult<TPrimaryKey> Add(TDto request);
        ResponseBase Remove(IdRequest<TPrimaryKey> request);
        ResponseBase RemoveMultiple(ICollection<IdRequest<TPrimaryKey>> request);
        ResponseBase Delete(IdRequest<TPrimaryKey> request);
        ResponseBase DeleteMultiple(ICollection<IdRequest<TPrimaryKey>> request);
        ResponseResult<TDto> Get(IdRequest<TPrimaryKey> request);
        ResponseResult<PagingResponse<TDto>> GetPaging(PagingRequest request);
        ResponseResult<List<TDto>> GetAll(RequestBase request);
        ResponseResult<TDto> Update(TDto request);
        // TODO
        //ResponseResult<PagingResponse<dynamic>> FindDynamic(FindDynamicPagingRequest dataRequest);
        //ResponseResult<PagingResponse<TDto>> Find(FindPagingRequest dataRequest);
    }
}
