using System.Web.Mvc;

namespace Demo.Web.Controllers
{
    // Must be authorised to access methods in here
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}
