using System.Net;
using System.Net.Security;
using System.ServiceModel;
using Demo.Service;

namespace Demo.Client
{
    public static class UsernameService
    {
        public static string FindCurrentUserOnServer()
        {
            // Setup the binding
            var binding = new NetTcpBinding(SecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;

            // Create the factory to local service
            var factory = new ChannelFactory<IUserService>(binding, "net.tcp://localhost:808/Demo3/UserService.svc");

            factory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            var service = factory.CreateChannel();
            var serverUserName = service.GetCurrentUserName();

            factory.Close();

            return serverUserName;
        }
    }
}
