using System.Threading;

namespace Demo.Service
{
    public class UserService : IUserService
    {
        public string GetCurrentUserName()
        {
            return Thread.CurrentPrincipal.Identity.Name;
        }
    }
}
