using System.Web.Mvc;
using Elmah.Web.Builders;

namespace Elmah.Web.Controllers
{
    public class ErrorLogController : Controller
    {
        public IErrorLogBuilder ErrorLogBuilder { get; set; }

        public ActionResult Index(string application, int pageNumber)
        {
            return View(ErrorLogBuilder.Build(application, pageNumber));
        }

    }
}
