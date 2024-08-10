using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;
using System;

namespace BrowserAutomationPlugin.Actions
{
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
}