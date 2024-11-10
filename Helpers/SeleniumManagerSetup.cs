using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BrowserAutomationPlugin.Helpers;

public static class SeleniumManagerSetup
{
    public static void SetSeleniumManagerPath()
    {
        var seleniumManagerPath = FindSeleniumManagerPath();
        if (!string.IsNullOrWhiteSpace(seleniumManagerPath))
        {
            Environment.SetEnvironmentVariable("SE_MANAGER_PATH", seleniumManagerPath);
            Console.WriteLine($"Selenium Manager path set to: {seleniumManagerPath}");
        }
        else
        {
            Console.WriteLine("Selenium Manager executable not found.");
        }
    }

    private static string FindSeleniumManagerPath()
    {
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Try to find Selenium Manager in the assembly location
        var path = SearchForSeleniumManager(Path.GetDirectoryName(assemblyLocation));

        // If not found, try the base directory
        if (string.IsNullOrEmpty(path)) path = SearchForSeleniumManager(baseDirectory);

        return path;
    }

    private static string SearchForSeleniumManager(string startPath)
    {
        string[] searchPaths = [startPath, Path.Combine(startPath, "selenium-manager")];

        foreach (var searchPath in searchPaths)
            if (Directory.Exists(searchPath))
            {
                var files = Directory.GetFiles(searchPath, "*manager*.exe", SearchOption.AllDirectories);
                var managerPath = files.FirstOrDefault(f => f.Contains("selenium-manager"));

                if (!string.IsNullOrWhiteSpace(managerPath)) return managerPath;
            }

        return null;
    }
}