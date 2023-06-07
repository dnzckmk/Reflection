// <copyright file="ConfigurationComponentBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Reflection;
using Common;
using ConfigurationManagerConfigurationProviderPlugin;
using FileConfigurationProviderPlugin;

namespace ConsoleApp
{
    /// <summary>
    /// Base class for Configuration components.
    /// </summary>
    public class ConfigurationComponentBase
    {
        /// <summary>
        /// Save settings based on configuration provider.
        /// </summary>
        public void SaveSettings()
        {
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                {
                    var provider = CreateConfigurationProvider(attribute.ProviderType);
                    var value = property.GetValue(this)?.ToString();
                    provider.SaveSetting(attribute.SettingName, value);
                }
            }
        }

        /// <summary>
        /// Load settings based on configuration provider.
        /// </summary>
        public void LoadSettings()
        {
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                if (Attribute.GetCustomAttribute(property, typeof(ConfigurationItemAttribute)) is ConfigurationItemAttribute attribute)
                {
                    var provider = CreateConfigurationProvider(attribute.ProviderType);
                    var value = provider.GetSetting(attribute.SettingName);

                    if (value != null)
                    {
                        var convertedValue = ConvertToType(value.ToString(), property.PropertyType);
                        property.SetValue(this, convertedValue);
                    }
                }
            }
        }

        /// <summary>
        /// Create new instance of the provider type.
        /// Use to customize provider type instance.
        /// </summary>
        /// <param name="providerType">Type of the provider.</param>
        /// <returns>Returns instance of the provider type.</returns>
        private static IConfigurationProvider CreateConfigurationProvider(Type providerType)
        {
            Assembly providerAssembly = Assembly.LoadFrom(providerType.Assembly.Location);
            Type providerTypeInAssembly = providerAssembly.GetType(providerType.FullName);
            ConstructorInfo constructor = providerTypeInAssembly.GetConstructor(new[] { typeof(string) });

            if (providerType == typeof(FileConfigurationProvider))
            {
                var path = GetFilePath("ConsoleApp", "config.txt");
                return (IConfigurationProvider)constructor.Invoke(new object[] { path });
            }
            else if (providerType == typeof(ConfigurationManagerConfigurationProvider))
            {
                var path = GetFilePath("ConsoleApp", "appsettings.json");
                return (IConfigurationProvider)constructor.Invoke(new object[] { path });
            }

            // Add other configuration providers here as needed
            throw new NotSupportedException($"Configuration provider type '{providerType}' is not supported.");
        }

        /// <summary>
        /// Get the full file path for the given file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Returns the full file path.</returns>
        private static string GetFilePath(string folderName, string fileName)
        {
            var rootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            return Path.Combine(rootPath, folderName, fileName);
        }

        /// <summary>
        /// Convert to target type.
        /// </summary>
        /// <param name="value">Object to convert.</param>
        /// <param name="targetType">Target type.</param>
        /// <returns>Returns target type with value.</returns>
        private static object ConvertToType(object value, Type targetType)
        {
            if (targetType == typeof(TimeSpan))
            {
                return TimeSpan.Parse((string)value);
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}
