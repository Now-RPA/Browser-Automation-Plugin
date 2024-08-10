using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace BrowserAutomationPlugin.Factories
{
    public class ChromeWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver(List<string> arguments, Dictionary<string, object> preferences,
            string driverPath)
        {
            var chromeDriverService = CreateDriverService(driverPath);
            var options = new ChromeOptions();
            ConfigureOptions(options, arguments, preferences);
            return new ChromeDriver(chromeDriverService, options);
        }

        private ChromeDriverService CreateDriverService(string driverPath)
        {
            ChromeDriverService service;
            if (!string.IsNullOrWhiteSpace(driverPath))
            {
                service = ChromeDriverService.CreateDefaultService(driverPath);
            }
            else
            {
                service = ChromeDriverService.CreateDefaultService();
            }
            service.HideCommandPromptWindow = true;
            return service;
        }

        private void ConfigureOptions(ChromeOptions options, List<string> arguments,
            Dictionary<string, object> preferences)
        {
            options.AddArguments(arguments);
            foreach (var pref in preferences)
            {
                options.AddUserProfilePreference(pref.Key, pref.Value);
            }
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddExcludedArgument("enable-automation");
        }
    }
}