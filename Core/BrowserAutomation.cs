using System;
using System.AddIn;
using System.Collections.Generic;
using BrowserAutomationPlugin.Helpers;

namespace BrowserAutomationPlugin.Core;

public enum DisposeOption
{
    AutoDisposeOn,
    AutoDisposeOff
}

[AddIn("BrowserAutomation", Description = "Browser Automation Plugin for RPA", Publisher = "Sumit Kumar",
    Version = "1.0.1.0")]
public class Session
{
    public static BrowserConnection Start(
        BrowserType browserType = BrowserType.Chrome,
        bool headless = false,
        string profilePath = "",
        string jsLibrary = "",
        string driverPath = "",
        List<string> arguments = null,
        DisposeOption disposeOption = DisposeOption.AutoDisposeOn)
    {
        switch (disposeOption)
        {
            case DisposeOption.AutoDisposeOn:
                return new DisposableBrowserConnection(browserType, profilePath, headless, jsLibrary, driverPath,
                    arguments);
            case DisposeOption.AutoDisposeOff:
                return new BrowserConnection(browserType, profilePath, headless, jsLibrary, driverPath, arguments);
            default:
                throw new ArgumentException("Invalid DisposeOption", nameof(disposeOption));
        }
    }

    public static void Close(BrowserConnection connection)
    {
        connection?.Dispose();
    }

    public static bool IsActive(BrowserConnection connection)
    {
        if (connection?.Driver is null) return false;
        try
        {
            var windowHandles = connection.Driver.WindowHandles;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

[AddIn("Wait")]
public class Wait
{
    public static bool ElementDisplayed(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector, int timeout = 30)
    {
        return WebDriverHelper.WaitForElementToAppear(connection?.Driver, selector, selectorType, timeout);
    }

    public static bool ElementHidden(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector, int timeout = 30)
    {
        return WebDriverHelper.WaitForElementToDisappear(connection?.Driver, selector, selectorType, timeout);
    }

    public static bool ElementLoaded(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        return WebDriverHelper.WaitForElementWithAttribute(connection?.Driver, selector, selectorType, attribute,
            timeout) != null;
    }

    public static bool PageLoaded(BrowserConnection connection, int timeout)
    {
        return WebDriverHelper.WaitForPageLoad(connection?.Driver, timeout);
    }
}