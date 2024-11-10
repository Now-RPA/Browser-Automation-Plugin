using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace BrowserAutomationPlugin.Factories;

public class EdgeWebDriverFactory : IWebDriverFactory
{
    public IWebDriver CreateDriver(List<string> arguments, Dictionary<string, object> preferences,
        string driverPath)
    {
        var edgeDriverService = CreateDriverService(driverPath);
        var options = new EdgeOptions();
        ConfigureOptions(options, arguments, preferences);
        return new EdgeDriver(edgeDriverService, options);
    }

    private EdgeDriverService CreateDriverService(string driverPath)
    {
        EdgeDriverService service;
        if (!string.IsNullOrWhiteSpace(driverPath))
            service = EdgeDriverService.CreateDefaultService(driverPath);
        else
            service = EdgeDriverService.CreateDefaultService();
        service.HideCommandPromptWindow = true;
        return service;
    }

    private void ConfigureOptions(EdgeOptions options, List<string> arguments,
        Dictionary<string, object> preferences)
    {
        options.AddArguments(arguments);
        foreach (var pref in preferences) options.AddUserProfilePreference(pref.Key, pref.Value);
        options.PageLoadStrategy = PageLoadStrategy.Normal;
        options.AddAdditionalOption("useAutomationExtension", false);
        options.AddExcludedArgument("enable-automation");
    }
}