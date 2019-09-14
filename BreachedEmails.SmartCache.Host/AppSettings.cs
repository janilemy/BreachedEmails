using System;
using System.Configuration;
using System.Reflection;

namespace BreachedEmails.SmartCache.Host
{
    /// <summary>
    /// Application settings class that contains configuration for storage provider
    /// </summary>
    public static class AppSettings
    {
        public static string GetSetting(string key)
        {
            // Explicitly specify the configuration file, otherwise it takes the configuration file of the running program
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            var setting = config.AppSettings.Settings[key]?.Value;

            if (string.IsNullOrEmpty(setting))
            {
                throw new ArgumentException(
                    $"Missing required configuration value for key '{key}'.");
            }

            return setting;
        }
    }
}
