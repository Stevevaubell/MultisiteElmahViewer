
using System.Configuration;

namespace Elmah.Web.Util.Impl
{
    public class ConfigSettings : IConfigSettings
    {
        public static readonly string OkCount = "OkCount";
        public static readonly string WarnCount = "WarnCount";
        public static readonly string ErrorCount = "ErrorCount";

        public string AppSettings(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}