using System.Web;

namespace IFramework.Infrastructure.Utility.Authentication
{
    public static class AuthenticationHelper
    {
        //private static readonly IUserRepository UserRepository = IoCBuilder.Instance.ReleaseInstance<IUserRepository>();

        public static string GetLogedInUserName()
        {
            return "canerbaki@gmail.com"; //HttpContext.Current.User.Identity.Name;
        }
        //public static User GetLogedInUser()
        //{
        //    return UserRepository.GetUserByEmail(HttpContext.Current.User.Identity.Name);
        //}
    }
}
