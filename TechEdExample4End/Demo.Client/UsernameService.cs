using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using Demo.Service;

namespace Demo.Client
{
    public static class UsernameService
    {
        public static string FindCurrentUserOnServer(string password)
        {
            // Accept any old certificate... DO NOT DO THIS IN PRODUCTION
            // Used for self-signed localhost certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => { return true; };

            // ADFS Binding
            var adfsBinding = new WS2007HttpBinding(SecurityMode.TransportWithMessageCredential);
            adfsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            var adfsMessage = adfsBinding.Security.Message;
            adfsMessage.ClientCredentialType = MessageCredentialType.UserName;
            adfsMessage.EstablishSecurityContext = false;
            adfsMessage.NegotiateServiceCredential = true;

            // ACS Binding
            var acsBinding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            var acsMessage = acsBinding.Security.Message;
            acsMessage.IssuedKeyType = SecurityKeyType.BearerKey;
            acsMessage.EstablishSecurityContext = false;
            acsMessage.IssuerAddress = new EndpointAddress("https://sts.planetsoftware.com.au/adfs/services/trust/13/usernamemixed");
            acsMessage.IssuerBinding = adfsBinding;
            
            // Service Binding
            var binding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            var message = binding.Security.Message;
            message.IssuedKeyType = SecurityKeyType.BearerKey;
            message.EstablishSecurityContext = false;
            message.IssuerMetadataAddress = new EndpointAddress("https://soniatest.accesscontrol.windows.net/v2/wstrust/mex");
            message.IssuerAddress = new EndpointAddress("https://soniatest.accesscontrol.windows.net/v2/wstrust/13/issuedtoken-bearer");
            message.IssuerBinding = acsBinding;
            message.ClaimTypeRequirements.Add(new ClaimTypeRequirement("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", true));

            // Create the factory to local service
            var factory = new ChannelFactory<IUserService>(binding, "https://localhost:446/Demo3End/UserService.svc");

            //factory.Credentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
            factory.Credentials.UserName.UserName = "felix";
            factory.Credentials.UserName.Password = password;

            var service = factory.CreateChannel();
            var serverUserName = service.GetCurrentUserName();

            factory.Close();

            return serverUserName;
        }
    }
}
