using Elmah.Web.Builders;
using System.Web.Mvc;

namespace Elmah.Web.Controllers
{
    public class ErrorDisplayController : Controller
    {
        public IErrorDisplayBuilder ErrorDisplayBuilder { get; set; }
        
        public ActionResult Index(string errorId)
        {
            if(string.IsNullOrEmpty(errorId))
                return RedirectToAction("Index", "Home");

            return View(ErrorDisplayBuilder.Build(errorId));
        }

    }
}
