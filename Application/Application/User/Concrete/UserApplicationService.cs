using System;

using IFramework.Application.Contract.UserDto;
using IFramework.Application.Core.Concrete;
using IFramework.Application.User.Abstract;

using IFramework.Domain.User;
using IFramework.Infrastructure.Transversal.Map.Abstract;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infra.Transversal.Validation.Attributes;
using IFramework.Infrastructure.Transversal.Aspect.Authorization;
using IFramework.Application.Contract.Core.Response;
using IFramework.Infrastructure.Transversal.Cache.Attributes;
using IFramework.Application.Contract.Core.Request;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using IFramework.Infrastructure.Transversal.Aspect.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IFramework.Application.User.Concrete
{
    public class UserService : GenericService<UserDto, Domain.User.User, Guid>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMap _map;
        public UserService(IUserRepository userRepository, IMap map) : base(map)
        {
            _userRepository = userRepository;
            _map = map;
        }

        [CustomAuthorize(Action = ActionType.Update, Container = "User", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        [Validation]
        public ResponseResult<UserDto> ChangeUserInfo(UserInfoDto userInfoDto)
        {

            Domain.User.User user = _userRepository.GetById(userInfoDto.UserId);
            if (user == null)
                throw new Exception("UserNotFound");//ValidationException(ErrorMessage.UserNotFound, ExceptionCodeConstants.UserNotFoundCode);

            //user.ChangeUserInfo(userInfoDto.Name, userInfoDto.LastName, userInfoDto.ProfileImage);
            ResponseResult<UserDto> result = IoCResolver.Instance.ReleaseInstance<ResponseResult<UserDto>>();
            result.Result = _map.Mapping<UserDto>(user);
            return result;
        }

        [Validation]
        [CustomAuthorize(Action = ActionType.Select, Container = "User", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public ResponseResult<PagingResponse<UserDto>> GetAllIncludePaging(PagingRequest request)
        {
            ResponseResult<PagingResponse<UserDto>> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<PagingResponse<UserDto>>>();
            int totalRowCount = _userRepository.Queryable(request.GetTrash).Count();
            List<Domain.User.User> entities = _userRepository.GetAllInclude(request.GetTrash, request.PageNo, request.PageRowCount);
            response.Result = new PagingResponse<UserDto>();
            response.Result.PageRowCount = request.PageRowCount;
            response.Result.TotalRowCount = totalRowCount;
            response.Result.List = _map.Mapping<List<UserDto>>(entities);
            response.ResultCode = HttpStatusCode.OK;
            return response;
        }

        [UnitOfWork]
        [Validation]
        [CustomAuthorize(Action = ActionType.Select, Container = "User", AuthorizationType = AuthorizationType.SystemUserAuthorization)]
        public ResponseResult<List<UserDto>> GetAllInclude(RequestBase request)
        {
            ResponseResult<List<UserDto>> response = IoCResolver.Instance.ReleaseInstance<ResponseResult<List<UserDto>>>();
            List<Domain.User.User> entities = _userRepository.Queryable(request.GetTrash).Include(p => p.UserInfo).Include(p => p.Role).ToList();
            response.Result = _map.Mapping<List<UserDto>>(entities);
            response.ResultCode = HttpStatusCode.OK;
            return response;
        }
    }
}
