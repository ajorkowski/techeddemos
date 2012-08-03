# Forms -> WS-Federation

Part of the series of examples used in my teched talk (Sept 2012)

In this example we want to 'upgrade' a website that uses forms authentication to be able to use alternative
user providers as well (Such as windows live, google, and facebook). However we want to do it using WS-Federation
(from something like Azure ACS). From the initial project we need to do the following steps:

1. In VS2012 go to tools -> Extensions and updates, find the "Identity and Access tool" and install it
2. On your project right click and run "Identity and Access..." to add your WS-Federation provider
3. Clean up the web.config and add reference to System.IdentityModel.Services
4. Change your FormsAuthentication calls to FederatedAuthentication calls
5. By default the identity.Name comes from the claim http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name - so make sure your WS-Federation provider uses that