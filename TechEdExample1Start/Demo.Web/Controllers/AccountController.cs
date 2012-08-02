using System.Web.Mvc;
using Demo.Web.Models;
using Demo.Web.Code;
using System.Web.Security;

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
        // GET: /Account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
