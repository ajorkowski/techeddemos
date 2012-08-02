
namespace Demo.Web.Code
{
    public class Authenticator : IAuthenticator
    {
        public bool Authenticate(string username, string password)
        {
            // Normally you would check the hash from a db etc etc
            return username == "test" && password == "password";
        }
    }
}