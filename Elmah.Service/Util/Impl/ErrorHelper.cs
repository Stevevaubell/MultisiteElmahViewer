
using System;

namespace Elmah.Service.Util.Impl
{
    public class ErrorHelper : IErrorHelper
    {
        public void LogError(Exception error)
        {
            //Log this using Elmah error signal
            var errorLog =
                new SqlErrorLog(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString)
                {
                    ApplicationName = "Application Health Service"
                };
            errorLog.Log(new Error(error));
        }
    }
}
