using Elmah.Web.Builders.Api;
using Elmah.Web.Models.Api;
using System.Collections.Generic;
using System.Web.Http;

namespace Elmah.Web.Controllers.Api
{
    public class ApplicationStateApiController : ApiController
    {
        public IApplicationStateApiBuilder ApplicationStateApiBuilder { get; set; }

        // GET api/applicationstateapi
        public IEnumerable<ApplicationState> Get()
        {
            return ApplicationStateApiBuilder.Build();
        }
    }
}
