using Elmah.Web.Builders;
using System.Web.Mvc;

namespace Elmah.Web.Controllers
{
    public class ErrorLogController : Controller
    {
        public IErrorLogBuilder ErrorLogBuilder { get; set; }
        
        public ActionResult Index(string application, int? pageNumber)
        {
            if(string.IsNullOrEmpty(application) || !pageNumber.HasValue)
                return RedirectToAction("Index", "Home");

            return View(ErrorLogBuilder.Build(application, pageNumber.Value));
        }

    }
}
