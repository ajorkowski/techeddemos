namespace Demo.Web.Code
{
    public interface IAuthenticator
    {
        bool Authenticate(string username, string password);
    }
}