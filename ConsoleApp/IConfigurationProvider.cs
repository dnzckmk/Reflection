// <copyright file="IConfigurationProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ConsoleApp
{
    /// <summary>
    /// Interface for the Configuration Providers.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Get setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>Returns setting value.</returns>
        object GetSetting(string settingName);

        /// <summary>
        /// Save setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">Value of the setting.</param>
        void SaveSetting(string settingName, object value);
    }
}
