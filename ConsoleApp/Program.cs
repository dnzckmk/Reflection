// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var component = new ConfigurationComponent();

        // Load the settings
        component.LoadSettings();

        // Set values
        component.Description = "Setting 1";
        component.Count = 5;
        component.Amount = 10;
        component.Duration = TimeSpan.FromMinutes(5);

        // Save the settings
        component.SaveSettings();

        // Reset the values
        component.Description = null;
        component.Count = 0;
        component.Amount = 0;
        component.Duration = TimeSpan.Zero;

        // Load the settings
        component.LoadSettings();

        // Display the loaded values
        Console.WriteLine($"Description: {component.Description}");
        Console.WriteLine($"Count: {component.Count}");
        Console.WriteLine($"Amount: {component.Amount}");
        Console.WriteLine($"Duration: {component.Duration}");

        Console.ReadLine();
    }
}