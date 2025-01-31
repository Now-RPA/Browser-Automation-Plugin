using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BrowserAutomationPlugin.Helpers;

public enum WindowState
{
    WindowMaximized,
    WindowMinimized,
    WindowFullscreen,
    WindowSpecificDimension
}

public enum BrowserType
{
    Chrome,
    Edge
}

public enum NavigationOption
{
    NavigateBack,
    NavigateForward,
    Refresh
}

public enum WindowSelectionMethod
{
    SelectWindowByHandle,
    SelectWindowByTitleRegex
}

public enum SelectorType
{
    CssSelector,
    XpathSelector,
    TagNameSelector,
    IdSelector,
    JavaScriptElementSelector
}

public enum Mode
{
    SimulateInteraction,
    JavascriptInteraction
}

public static class WebDriverHelper
{
    public static void EnsureValidSessionExists(IWebDriver driver)
    {
        if (driver == null)
            throw new InvalidOperationException("Valid browser session not found");
    }

    public static IWebElement WaitForElementWithAttribute(IWebDriver driver, string search,
        SelectorType selectorType,
        string attribute, int timeoutInSeconds)
    {
        attribute = attribute ?? "class";
        var wait = new DefaultWait<IWebDriver>(driver)
        {
            Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
            PollingInterval = TimeSpan.FromMilliseconds(100)
        };
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException),
            typeof(WebDriverTimeoutException));
        try
        {
            return wait.Until(webDriver =>
            {
                var element = GetElement(webDriver, search, selectorType);
                if (element != null &&
                    element.GetAttribute(attribute) != null)
                    return element;
                return null;
            });
        }
        catch (WebDriverTimeoutException)
        {
            // Timeout occurred, return null instead of throwing
            return null;
        }
    }

    public static bool WaitForElementToAppear(IWebDriver driver, string search, SelectorType selectorType,
        int timeoutInSeconds)
    {
        var wait = new DefaultWait<IWebDriver>(driver)
        {
            Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
            PollingInterval = TimeSpan.FromMilliseconds(100)
        };
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
            typeof(StaleElementReferenceException),
            typeof(ElementNotVisibleException),
            typeof(ElementNotInteractableException));
        try
        {
            return wait.Until(webDriver =>
            {
                var element = GetElement(webDriver, search, selectorType);
                return element != null && element.Displayed;
            });
        }
        catch (WebDriverTimeoutException)
        {
            // Timeout occurred, element did not appear
            return false;
        }
    }

    public static bool WaitForElementToDisappear(IWebDriver driver, string search, SelectorType selectorType,
        int timeoutInSeconds)
    {
        var wait = new DefaultWait<IWebDriver>(driver)
        {
            Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
            PollingInterval = TimeSpan.FromMilliseconds(100)
        };
        try
        {
            return wait.Until(webDriver =>
            {
                try
                {
                    var element = GetElement(webDriver, search, selectorType);
                    return element == null || !element.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return true; // Element is no longer attached to the DOM, consider it disappeared
                }
            });
        }
        catch (WebDriverTimeoutException)
        {
            // Timeout occurred, element did not disappear
            return false;
        }
    }

    public static IWebElement GetElement(IWebDriver driver, string search, SelectorType selectorType)
    {
        try
        {
            switch (selectorType)
            {
                case SelectorType.IdSelector:
                    return driver.FindElement(By.Id(search));
                case SelectorType.CssSelector:
                    return driver.FindElement(By.CssSelector(search));
                case SelectorType.XpathSelector:
                    return driver.FindElement(By.XPath(search));
                case SelectorType.TagNameSelector:
                    return driver.FindElement(By.TagName(search));
                case SelectorType.JavaScriptElementSelector:
                    return (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript(search);
                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    internal static bool WaitForPageLoad(IWebDriver driver, int timeout)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
        catch (Exception)
        {
            return false;
        }
    }
}