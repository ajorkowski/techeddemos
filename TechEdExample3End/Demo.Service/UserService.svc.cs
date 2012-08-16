using System.Security.Claims;
using System.Threading;

namespace Demo.Service
{
    public class UserService : IUserService
    {
        public string GetCurrentUserName()
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            return Thread.CurrentPrincipal.Identity.Name;
        }
    }
}
