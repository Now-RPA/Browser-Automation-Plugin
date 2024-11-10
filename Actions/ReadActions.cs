using System;
using System.AddIn;
using System.Collections.Generic;
using System.Data;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Actions;

[AddIn("Read")]
public class Read
{
    public static Dictionary<string, string> GetAllValues(BrowserConnection connection, List<object> selectorList,
        SelectorType selectorType = SelectorType.CssSelector)
    {
        return ReadActions.GetAllValues(connection?.Driver, selectorList, selectorType);
    }

    public static string GetCurrentUrl(BrowserConnection connection)
    {
        return ReadActions.GetCurrentUrl(connection?.Driver);
    }

    public static Dictionary<string, string> GetDetails(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        return ReadActions.GetDetails(connection?.Driver, selector, selectorType, timeout, attribute);
    }

    public static string GetPageSource(BrowserConnection connection)
    {
        return ReadActions.GetPageSource(connection?.Driver);
    }

    public static DataTable GetTable(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        return ReadActions.GetTable(connection?.Driver, selector, selectorType, timeout, attribute);
    }

    public static string GetText(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30,
        string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
    {
        return ReadActions.GetText(connection?.Driver, selector, selectorType, timeout, attribute,
            interactionMode);
    }

    public static string GetValue(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        return ReadActions.GetValue(connection?.Driver, selector, selectorType, timeout, attribute);
    }

    public static bool IsPageLoaded(BrowserConnection connection)
    {
        return ReadActions.IsPageLoaded(connection?.Driver);
    }

    public static bool IsElementDisplayed(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        return ReadActions.IsElementDisplayed(connection?.Driver, selector, selectorType, timeout, attribute);
    }

    public static bool IsElementEnabled(BrowserConnection connection, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        return ReadActions.IsElementEnabled(connection?.Driver, selector, selectorType, timeout, attribute);
    }

    public static void SaveElementScreenshot(BrowserConnection connection, string filePath, string selector,
        SelectorType selectorType = SelectorType.CssSelector,
        int timeout = 30, string attribute = "class")
    {
        ReadActions.CaptureScreenshot(connection?.Driver, filePath, selector, selectorType, timeout, attribute);
    }
}

public static class ReadActions
{
    public static Dictionary<string, string> GetAllValues(IWebDriver driver, List<object> selectorList,
        SelectorType selectorType)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var result = new Dictionary<string, string>();
        foreach (var selector in selectorList)
        {
            var element = WebDriverHelper.GetElement(driver, selector.ToString(), selectorType);
            if (element != null) result[selector.ToString()] = element.GetAttribute("value");
        }

        return result;
    }

    public static string GetCurrentUrl(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        return driver.Url;
    }

    public static Dictionary<string, string> GetDetails(IWebDriver driver, string selector,
        SelectorType selectorType, int timeout, string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");

        return new Dictionary<string, string>
        {
            ["tag"] = element.TagName,
            ["text"] = element.Text,
            ["value"] = element.GetAttribute("value"),
            ["class"] = element.GetAttribute("class"),
            ["name"] = element.GetAttribute("name"),
            ["id"] = element.GetAttribute("id"),
            ["topX"] = element.Location.X.ToString(),
            ["topY"] = element.Location.Y.ToString(),
            ["height"] = element.Size.Height.ToString(),
            ["width"] = element.Size.Width.ToString()
        };
    }

    public static string GetPageSource(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        return driver.PageSource;
    }

    public static DataTable GetTable(IWebDriver driver, string selector, SelectorType selectorType, int timeout,
        string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
        var table = new DataTable();
        var rows = element.FindElements(By.TagName("tr"));
        if (rows.Count > 0)
        {
            var headers = rows[0].FindElements(By.TagName("th"));
            foreach (var header in headers) table.Columns.Add(header.Text);
            for (var i = 1; i < rows.Count; i++)
            {
                var cells = rows[i].FindElements(By.TagName("td"));
                var row = table.NewRow();
                for (var j = 0; j < cells.Count; j++) row[j] = cells[j].Text;
                table.Rows.Add(row);
            }
        }

        return table;
    }

    public static string GetText(IWebDriver driver, string selector, SelectorType selectorType, int timeout,
        string attribute, Mode interactionMode)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
        switch (interactionMode)
        {
            case Mode.SimulateInteraction:
                return element.Text;
            case Mode.JavascriptInteraction:
                return ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                    return arguments[0].innerText;
                                    ", element)
                    ?.ToString() ?? string.Empty;
            default:
                return string.Empty;
        }
    }

    public static string GetValue(IWebDriver driver, string selector, SelectorType selectorType, int timeout,
        string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
        return element.GetAttribute("value") ?? string.Empty;
    }

    public static bool IsPageLoaded(IWebDriver driver)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
    }

    public static bool IsElementDisplayed(IWebDriver driver, string selector, SelectorType selectorType,
        int timeout, string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        return element?.Displayed ?? false;
    }

    public static bool IsElementEnabled(IWebDriver driver, string selector, SelectorType selectorType, int timeout,
        string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
        return element.Enabled;
    }

    public static void CaptureScreenshot(IWebDriver driver, string filePath, string selector,
        SelectorType selectorType, int timeout, string attribute)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        var element =
            WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
        if (element == null)
            throw new Exception(
                $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
        var elementScreenshot = (element as ITakesScreenshot)?.GetScreenshot();
        elementScreenshot?.SaveAsFile(filePath);
    }
}