// <copyright file="FileConfigurationProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json;

namespace ConsoleApp
{ /// <summary>
  /// File configuration provider.
  /// </summary>
    public class FileConfigurationProvider : IConfigurationProvider
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileConfigurationProvider"/> class.
        /// If the path is null or empty, throw ArgumentNullException.
        /// If the given path is not exist, creates it.
        /// </summary>
        /// <param name="path">Path of file.</param>
        public FileConfigurationProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.path = path;

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "{}");
                Console.WriteLine($"File created. Path: {path}");
            }
        }

        /// <summary>
        /// Gets the value of the provided key from the configuration file.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>Returns value of the setting, if not found returns null.</returns>
        public object GetSetting(string settingName)
        {
            var file = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(file);

            if (json.TryGetValue(settingName, out var value))
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Save the provided key value pair in the configuration file.
        /// If the setting already exists, updates it else adds it.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="value">Value of the setting.</param>
        public void SaveSetting(string settingName, object value)
        {
            var file = File.ReadAllText(path);

            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(file);
            json[settingName] = value.ToString();

            File.WriteAllText(path, JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
