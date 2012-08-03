using System.IdentityModel.Services;
using System.Web.Mvc;
using Demo.Web.Models;

namespace Demo.Web.Controllers
{
    public class AccountController : Controller
    {
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
            FederatedAuthentication.WSFederationAuthenticationModule.SignIn(null);
            return null;
        }

        //
        // GET: /Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FederatedAuthentication.WSFederationAuthenticationModule.SignOut();
            return RedirectToAction("Login");
        }
    }
}
