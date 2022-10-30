using IFramework.Api.Core.Api;
using IFramework.Application.Authorization.Abstract;
using IFramework.Application.User.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace IFramework.WebApi.Controllers
{
    //    [Route("api/[controller]")]
    //    [ApiController]
    //    public class ValuesController : UserController
    //    {
    //        private readonly IUserApplicationService _applicationService;
    //        private readonly IAuthenticationService _authenticationService;
    //        public ValuesController(IUserApplicationService applicationService, IAuthenticationService authenticationService) : base(applicationService, authenticationService)
    //        {
    //            _applicationService = applicationService;
    //            _authenticationService = authenticationService;
    //        }
    //    }


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
        }
        [HttpGet()]
        public string Get()
        {
            return "Caner";
        }
    }

}
