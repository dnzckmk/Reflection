// <copyright file="ConfigurationManagerConfigurationProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json;
using Common;

namespace ConfigurationManagerConfigurationProviderPlugin
{
    /// <summary>
    /// Configuration manager provider.
    /// appsettings.json.
    /// </summary>
    public class ConfigurationManagerConfigurationProvider : IConfigurationProvider
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManagerConfigurationProvider"/> class.
        /// </summary>
        /// <param name="path">Configuration file path.</param>
        /// <exception cref="ArgumentNullException">If the path is null, throws ArgumentNullException.</exception>
        public ConfigurationManagerConfigurationProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.path = path;

            FileHelper.CreateFileIfNotExist(path, "{\"appSettings\":{}}");
        }

        /// <summary>
        /// Get value of the provided key from appsettings.json.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>Value of the setting. If not found retuns null.</returns>
        public object GetSetting(string settingName)
        {
            var file = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<JsonDocument>(file);

            if (json.RootElement.TryGetProperty("appSettings", out var appSettings))
            {
                if (appSettings.TryGetProperty(settingName, out var value))
                {
                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// Save the provided setting key with value to the appsettings.json.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">Value of the setting.</param>
        public void SaveSetting(string settingName, object value)
        {
            var file = File.ReadAllText(path);

            var json = JsonSerializer.Deserialize<JsonDocument>(file);
            var appSettings = json.RootElement.GetProperty("appSettings");
            var appSettingsDict = new Dictionary<string, string>(appSettings.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString()))
            {
                [settingName] = value.ToString()
            };

            File.WriteAllText(path, JsonSerializer.Serialize(new { appSettings = appSettingsDict }, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
