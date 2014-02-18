using Elmah.Web.Builders;
using System.Web.Mvc;

namespace Elmah.Web.Controllers
{
    public class HomeController : Controller
    {
        public IHomeBuilder HomeBuilder { get; set; }

        public ActionResult Index()
        {
            return View(HomeBuilder.Build());
        }
    }
}
