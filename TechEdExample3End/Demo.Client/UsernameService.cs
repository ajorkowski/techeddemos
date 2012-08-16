using System.IdentityModel.Tokens;
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
            // ADFS Binding
            var adfsBinding = new WS2007HttpBinding(SecurityMode.TransportWithMessageCredential);

            
            // Service Binding
            var serviceBinding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            var message = serviceBinding.Security.Message;
            message.IssuedKeyType = SecurityKeyType.BearerKey;
            message.IssuerAddress = new EndpointAddress("https://soniatest.accesscontrol.windows.net/v2/wstrust/13/issuedtoken-bearer");
            message.IssuerBinding

            // Setup the binding (transport security using windows authentication)
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
