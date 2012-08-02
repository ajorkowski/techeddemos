using System;
using System.Web.Mvc;
using Demo.Web.Models;
using Demo.Web.Code;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;

namespace Demo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticator _authenticator;

        public AccountController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        //
        // GET: /Account/login
        public ActionResult Login()
        {
            // If we are already authenticated then just send on their merry way
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginModel());
        }

        //
        // POST: /Account/login
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                if(_authenticator.Authenticate(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username/password");
                    model.Password = null;
                    return View(model);
                }
            }
            else
            {
                return View(new LoginModel());
            }
        }

        //
        // GET: /Account/Google
        public ActionResult Google()
        {
            var googleClient = new GoogleOpenIdClient();
            return Authenticate(googleClient);
        }

        //
        // GET: /Account/Live
        public ActionResult Live()
        {
            var liveClient = new MicrosoftClient("appId", "appSecret");
            return Authenticate(liveClient);
        }

        // GET: /Accounts/Facebook
        public ActionResult Facebook()
        {
            var facebookClient = new FacebookClient("appId", "appSecret");
            return Authenticate(facebookClient);
        }

        private ActionResult Authenticate(IAuthenticationClient client)
        {
            try
            {
                var auth = client.VerifyAuthentication(HttpContext);
                if (auth.IsSuccessful)
                {
                    FormsAuthentication.SetAuthCookie(auth.UserName, false);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException)
            {
                // If the verification failed we have to redirect
                client.RequestAuthentication(HttpContext, Request.Url);
                return null;
            }
        }

        //
        // GET: /Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
