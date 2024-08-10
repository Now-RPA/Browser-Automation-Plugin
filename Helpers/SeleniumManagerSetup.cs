using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BrowserAutomationPlugin.Helpers
{
    public static class SeleniumManagerSetup
    {
        public static void SetSeleniumManagerPath()
        {
            string seleniumManagerPath = FindSeleniumManagerPath();
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
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Try to find Selenium Manager in the assembly location
            string path = SearchForSeleniumManager(Path.GetDirectoryName(assemblyLocation));

            // If not found, try the base directory
            if (string.IsNullOrEmpty(path))
            {
                path = SearchForSeleniumManager(baseDirectory);
            }

            return path;
        }

        private static string SearchForSeleniumManager(string startPath)
        {
            string[] searchPaths = { startPath, Path.Combine(startPath, "selenium-manager") };

            foreach (string searchPath in searchPaths)
            {
                if (Directory.Exists(searchPath))
                {
                    string[] files = Directory.GetFiles(searchPath, "*manager*.exe", SearchOption.AllDirectories);
                    string managerPath = files.FirstOrDefault(f => f.Contains("selenium-manager"));

                    if (!string.IsNullOrWhiteSpace(managerPath))
                    {
                        return managerPath;
                    }
                }
            }

            return null;
        }
    }
}