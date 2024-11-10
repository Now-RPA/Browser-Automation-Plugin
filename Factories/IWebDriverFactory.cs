using System.Collections.Generic;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Factories;

public interface IWebDriverFactory
{
    IWebDriver CreateDriver(List<string> arguments, Dictionary<string, object> preferences, string driverPath);
}