using System.ServiceModel;

namespace Demo.Service
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string GetCurrentUserName();
    }
}
