using System.Configuration;

namespace EduApi.Web.Providers
{

    public interface IApplicationSettingsProvider
    {
        string GetValue(string key);
    }

    public class ApplicationSettingsProvider : IApplicationSettingsProvider
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}