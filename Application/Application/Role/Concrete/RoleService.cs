using System;

using IFramework.Application.Contract.UserDto;
using IFramework.Application.Core.Concrete;
using IFramework.Application.User.Abstract;

using IFramework.Domain.User;
using IFramework.Infrastructure.Transversal.Map.Abstract;

namespace IFramework.Application.User.Concrete
{
    public class RoleService :  GenericService<RoleDto, Domain.User.Role, Guid>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMap _map;
        public RoleService(IRoleRepository roleRepository, IMap map) : base(map)
        {
            _roleRepository = roleRepository;
            _map = map;
        }
    }
}
