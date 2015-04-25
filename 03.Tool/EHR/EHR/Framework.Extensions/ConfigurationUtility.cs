using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Extensions
{
    public static class ConfigurationUtility
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static string GetConfigurationSettingValue(string configName)
        {
            return ConfigurationManager.AppSettings[configName];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static string GetConnectionStringValue(string configName)
        {
            return ConfigurationManager.ConnectionStrings[configName].ConnectionString;
        }
    }
}
