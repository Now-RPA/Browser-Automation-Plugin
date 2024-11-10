using System.AddIn;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Actions;

[AddIn("Alerts")]
public class Alerts
{
    public static void Accept(BrowserConnection connection)
    {
        AlertActions.Accept(connection?.Driver);
    }

    public static void Dismiss(BrowserConnection connection)
    {
        AlertActions.Dismiss(connection?.Driver);
    }

    public static string GetText(BrowserConnection connection)
    {
        return AlertActions.GetText(connection?.Driver);
    }

    public static void SetText(BrowserConnection connection, string text)
    {
        AlertActions.SetText(connection?.Driver, text);
    }
}

public static class AlertActions
{
    public static void Accept(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        try
        {
            driver.SwitchTo().Alert().Accept();
        }
        catch (NoAlertPresentException)
        {
            // Alert not present, do nothing
        }
    }

    public static void Dismiss(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        try
        {
            driver.SwitchTo().Alert().Dismiss();
        }
        catch (NoAlertPresentException)
        {
            // Alert not present, do nothing
        }
    }

    public static string GetText(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        try
        {
            return driver.SwitchTo().Alert().Text;
        }
        catch (NoAlertPresentException)
        {
            return string.Empty;
        }
    }

    public static void SetText(IWebDriver driver, string text)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        try
        {
            driver.SwitchTo().Alert().SendKeys(text);
        }
        catch (NoAlertPresentException)
        {
            // Alert not present, do nothing
        }
    }
}