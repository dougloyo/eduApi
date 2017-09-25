using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

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