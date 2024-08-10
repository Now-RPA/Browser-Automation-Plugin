using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;
using System;

namespace BrowserAutomationPlugin.Actions
{
    public static class JavascriptActions
    {
        public static string Execute(IWebDriver driver, string jsCode, string jsLibrary)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            try
            {
                string fullJs = string.Join(Environment.NewLine, jsLibrary, jsCode);
                var result = ((IJavaScriptExecutor)driver).ExecuteScript(fullJs);
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing JavaScript: {ex.Message}");
            }
        }
    }
}