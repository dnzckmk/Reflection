// <copyright file="ConfigurationItemAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp
{
    /// <summary>
    /// Allows loading or saving a setting value based on the ConfigurationProvider type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigurationItemAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationItemAttribute"/> class.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="providerType">Configuration provider type.</param>
        public ConfigurationItemAttribute(string settingName, Type providerType)
        {
            SettingName = settingName;
            ProviderType = providerType;
        }

        /// <summary>
        /// Gets name of the setting.
        /// </summary>
        public string SettingName { get; }

        /// <summary>
        /// Gets type of configuration provider.
        /// </summary>
        public Type ProviderType { get; }
    }
}
