using Elmah.Web.Util;
using Elmah.Web.Util.Impl;

namespace Elmah.Web.Models.Api
{
    public class ApplicationState
    {
        public const string ERRORCLASS = "bg-danger";
        public const string WARNCLASS = "bg-warning";
        public const string OKCLASS = "bg-success";

        public IConfigSettings Settings { get; set; }

        public ApplicationState()
        {
            Settings = new ConfigSettings();
        }

        public string Application { get; set; }
        public int ErrorCount { get; set; }
        public string Class {
            get
            {
                int ok = int.Parse(Settings.AppSettings(ConfigSettings.OkCount));
                int warn = int.Parse(Settings.AppSettings(ConfigSettings.WarnCount));
                int error = int.Parse(Settings.AppSettings(ConfigSettings.ErrorCount));

                if (this.ErrorCount >= error)
                    return ERRORCLASS;
                else if (this.ErrorCount <= warn && this.ErrorCount >= ok)
                    return WARNCLASS;
                else
                    return OKCLASS;
            }}
    }
}