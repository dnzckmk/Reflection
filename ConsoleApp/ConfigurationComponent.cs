// <copyright file="ConfigurationComponent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ConfigurationManagerConfigurationProviderPlugin;
using FileConfigurationProviderPlugin;

namespace ConsoleApp
{
    /// <summary>
    /// Basic Configuration Component.
    /// </summary>
    public class ConfigurationComponent : ConfigurationComponentBase
    {
        /// <summary>
        /// Gets or sets string field for configuration.
        /// </summary>
        [ConfigurationItem("Description", typeof(FileConfigurationProvider))]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets integer field for configuration.
        /// </summary>
        [ConfigurationItem("Count", typeof(ConfigurationManagerConfigurationProvider))]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets float field for configuration.
        /// </summary>
        [ConfigurationItem("Amount", typeof(FileConfigurationProvider))]
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets timeSpan field for configuration.
        /// </summary>
        [ConfigurationItem("Duration", typeof(ConfigurationManagerConfigurationProvider))]
        public TimeSpan Duration { get; set; }
    }
}
