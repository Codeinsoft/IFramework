using System;

using Microsoft.AspNetCore.Mvc;
using IFramework.Application.Contract.UserDto;

namespace IFramework.Api.Core.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController<RoleDto, Domain.User.Role, Guid>
    {
        public RoleController()
        {
        }        
    }
}
