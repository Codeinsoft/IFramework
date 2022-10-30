using System;
using System.Collections.Generic;
using IFramework.Application.Contract.Core.Request;
using IFramework.Application.Contract.Core.Response;
using IFramework.Application.Contract.Core.Service;
using IFramework.Application.Contract.UserDto;
using IFramework.Application.Core.Abstract;


namespace IFramework.Application.User.Abstract
{
    public interface IUserService : IApplicationService, IGenericService<UserDto, Domain.User.User, Guid>
    {
        ResponseResult<UserDto> ChangeUserInfo(UserInfoDto userInfoDto);
        ResponseResult<PagingResponse<UserDto>> GetAllIncludePaging(PagingRequest request);
        ResponseResult<List<UserDto>> GetAllInclude(RequestBase request);
    }
}
