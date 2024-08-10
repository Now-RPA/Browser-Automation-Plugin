using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;

namespace BrowserAutomationPlugin.Actions
{
    public static class ReadActions
    {
        public static Dictionary<string, string> GetAllValues(IWebDriver driver, List<object> selectorList, SelectorType selectorType)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var result = new Dictionary<string, string>();
            foreach (var selector in selectorList)
            {
                var element = WebDriverHelper.GetElement(driver, selector.ToString(), selectorType);
                if (element != null)
                {
                    result[selector.ToString()] = element.GetAttribute("value");
                }
            }
            return result;
        }

        public static string GetCurrentUrl(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            return driver.Url;
        }

        public static Dictionary<string, string> GetDetails(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                return new Dictionary<string, string>();
            }
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

        public static DataTable GetTable(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                return new DataTable();
            }
            var table = new DataTable();
            var rows = element.FindElements(By.TagName("tr"));
            if (rows.Count > 0)
            {
                var headers = rows[0].FindElements(By.TagName("th"));
                foreach (var header in headers)
                {
                    table.Columns.Add(header.Text);
                }
                for (int i = 1; i < rows.Count; i++)
                {
                    var cells = rows[i].FindElements(By.TagName("td"));
                    var row = table.NewRow();
                    for (int j = 0; j < cells.Count; j++)
                    {
                        row[j] = cells[j].Text;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static string GetText(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                return string.Empty;
            }
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

        public static string GetValue(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            return element?.GetAttribute("value") ?? string.Empty;
        }

        public static bool IsPageLoaded(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
        }

        public static bool IsElementDisplayed(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            return element?.Displayed ?? false;
        }

        public static bool IsElementEnabled(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            return element?.Enabled ?? false;
        }
        public static void CaptureScreenshot(IWebDriver driver, string filePath, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }
            var elementScreenshot = (element as ITakesScreenshot)?.GetScreenshot();
            elementScreenshot?.SaveAsFile(filePath);
        }
    }
}