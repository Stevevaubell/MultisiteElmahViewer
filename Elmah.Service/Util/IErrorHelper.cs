
using System;

namespace Elmah.Service.Util
{
    public interface IErrorHelper
    {
        void LogError(Exception error);
    }
}
