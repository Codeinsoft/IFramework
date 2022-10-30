using System;

namespace IFramework.Infrastructure.Transversal.Aspect.Authorization
{
    public class CustomAuthorizeAttribute : Attribute
    {
        public AuthorizationType AuthorizationType { get; set; }
        public ActionType Action { get; set; }
        public string Container { get; set; }
    }
}
