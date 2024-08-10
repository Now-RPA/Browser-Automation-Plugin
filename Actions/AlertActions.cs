using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Actions
{
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
}