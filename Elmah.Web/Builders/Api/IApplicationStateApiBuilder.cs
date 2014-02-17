using Elmah.Web.Models.Api;
using System.Collections.Generic;

namespace Elmah.Web.Builders.Api
{
    public interface IApplicationStateApiBuilder
    {
        IEnumerable<ApplicationState> Build();
    }
}
