using System;
using IFramework.Application.Contract.Core.Service;
using IFramework.Application.Contract.UserDto;
using IFramework.Application.Core.Abstract;


namespace IFramework.Application.User.Abstract
{
    public interface IRoleService : IApplicationService, IGenericService<RoleDto, Domain.User.Role, Guid>
    {
    }
}
