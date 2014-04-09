using System.Configuration;

namespace Elmah.Core.Util.Impl
{
    public class AppSettingsHelper : IAppSettingsHelper
    {
        public string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public int GetIntConfig(string key)
        {
            return int.Parse(ConfigurationManager.AppSettings[key]);
        }
    }
}
