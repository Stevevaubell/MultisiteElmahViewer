
namespace Elmah.Core.Util
{
    public interface IAppSettingsHelper
    {
        string GetConfig(string key);
        int GetIntConfig(string key);
    }
}
