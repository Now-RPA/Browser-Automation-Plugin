using System;
using System.AddIn;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Actions;

[AddIn("Frame")]
public class Frame
{
    public static void Select(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        FrameActions.Select(connection?.Driver, selector, selectorType, timeout, attribute);
    }

    public static void Reset(BrowserConnection connection)
    {
        FrameActions.Reset(connection?.Driver);
    }
}

public static class FrameActions
{
    public static void Select(IWebDriver driver, string selector, SelectorType selectorType, int timeout,
        string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Frame Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
        driver.SwitchTo().Frame(element);
    }

    public static void Reset(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        driver.SwitchTo().DefaultContent();
    }
}