using System;
using System.AddIn;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Actions;

[AddIn("Navigation")]
public class Navigation
{
    public static void Navigate(BrowserConnection connection,
        NavigationOption navigateOption = NavigationOption.Refresh)
    {
        NavigationActions.Navigate(connection?.Driver, navigateOption);
    }
}

public static class NavigationActions
{
    public static void Navigate(IWebDriver driver, NavigationOption navigateOption)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);

        switch (navigateOption)
        {
            case NavigationOption.NavigateBack:
                driver.Navigate().Back();
                break;
            case NavigationOption.NavigateForward:
                driver.Navigate().Forward();
                break;
            case NavigationOption.Refresh:
                driver.Navigate().Refresh();
                break;
            default:
                throw new ArgumentException("Invalid navigation option");
        }
    }
}