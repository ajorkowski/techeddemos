# Forms -> OAuth/OpenID

Part of the series of examples used in my teched talk (Sept 2012)

In this example we want to 'upgrade' a website that uses forms authentication to be able to use alternative
user providers as well (Such as windows live, google, and facebook). From the initial project we need to do
the following steps:

1. Add the DotNetOpenAuth.AspNet nuget package
2. For each provider add to the accounts controller
3. Link to these new providers in the view