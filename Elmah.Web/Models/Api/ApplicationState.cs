
using System.Configuration;

namespace Elmah.Web.Models.Api
{
    public class ApplicationState
    {
        private const string ERRORCLASS = "bg-danger";
        private const string WARNCLASS = "bg-warning";
        private const string OKCLASS = "bg-success";

        public string Application { get; set; }
        public int ErrorCount { get; set; }
        public string Class {
            get
            {
                int ok = int.Parse(ConfigurationManager.AppSettings["OkCount"]);
                int warn = int.Parse(ConfigurationManager.AppSettings["WarnCount"]);
                int error = int.Parse(ConfigurationManager.AppSettings["ErrorCount"]);

                if (this.ErrorCount >= error)
                    return ERRORCLASS;
                else if (this.ErrorCount <= warn && this.ErrorCount >= ok)
                    return WARNCLASS;
                else
                    return OKCLASS;
            }}
    }
}