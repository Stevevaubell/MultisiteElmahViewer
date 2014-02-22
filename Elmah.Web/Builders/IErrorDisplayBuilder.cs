
using Elmah.Web.Models;

namespace Elmah.Web.Builders
{
    public interface IErrorDisplayBuilder
    {
        ErrorDisplayViewModel Build(string errorId);
    }
}
