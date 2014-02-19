
using Elmah.Web.Models;

namespace Elmah.Web.Builders
{
    public interface IErrorLogBuilder
    {
        ErrorLogViewModel Build(string applicationName, int pageNumber);
    }
}
