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
            var adfsMessafe = adfsBinding.Security.Message;
            adfsMessafe.ClientCredentialType = MessageCredentialType.Windows;
            adfsMessafe.EstablishSecurityContext = false;
            adfsMessafe.NegotiateServiceCredential = true;

            // ACS Binding
            var acsBinding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            var acsMessage = acsBinding.Security.Message;
            acsMessage.IssuedKeyType = SecurityKeyType.BearerKey;
            acsMessage.IssuerAddress = new EndpointAddress("https://sts.planetsoftware.com.au/adfs/services/trust/13/windowsmixed");
            acsMessage.IssuerBinding = adfsBinding;
            
            // Service Binding
            var binding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            var message = binding.Security.Message;
            message.IssuedKeyType = SecurityKeyType.BearerKey;
            message.IssuerAddress = new EndpointAddress("https://soniatest.accesscontrol.windows.net/v2/wstrust/13/issuedtoken-bearer");
            message.IssuerBinding = acsBinding;

            // Create the factory to local service
            var factory = new ChannelFactory<IUserService>(binding, "https://felix-home.planetsoftware.local:446/Demo3End/UserService.svc");

            factory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;

            var service = factory.CreateChannel();
            var serverUserName = service.GetCurrentUserName();

            factory.Close();

            return serverUserName;
        }
    }
}
