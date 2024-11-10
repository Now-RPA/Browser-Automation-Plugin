using System;
using System.AddIn;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Actions;

[AddIn("Javascript")]
public class Javascript
{
    public static string Execute(BrowserConnection connection, string jsCode)
    {
        return JavascriptActions.Execute(connection?.Driver, jsCode, connection?.Library ?? "");
    }
}

public static class JavascriptActions
{
    public static string Execute(IWebDriver driver, string jsCode, string jsLibrary)
    {
        WebDriverHelper.EnsureValidSessionExists(driver);
        try
        {
            var fullJs = string.Join(Environment.NewLine, jsLibrary, jsCode);
            var result = ((IJavaScriptExecutor)driver).ExecuteScript(fullJs);
            return result?.ToString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error executing JavaScript: {ex.Message}");
        }
    }
}