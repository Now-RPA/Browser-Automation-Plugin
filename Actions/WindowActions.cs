using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BrowserAutomationPlugin.Actions
{
    public static class WindowActions
    {
        public static void GoToUrl(BrowserConnection connection, string url, WindowState windowState = WindowState.WindowMaximized, int? width = null, int? height = null)
        {
            WebDriverHelper.EnsureValidSessionExists(connection?.Driver);
            try
            {
                connection?.Driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException)
            {
                // If the session doesn't exist, reinitialize the driver -this will automatically create window
                connection?.ReinitializeDriver();
                connection?.Driver.Navigate().GoToUrl(url);
            }
            finally
            {
                SetSize(connection?.Driver, windowState, width, height);
            }

        }

        public static void OpenNewTab(BrowserConnection connection)
        {
            WebDriverHelper.EnsureValidSessionExists(connection?.Driver);
            try
            {
                connection?.Driver.SwitchTo().NewWindow(WindowType.Tab);
            }
            catch (WebDriverException)
            {
                // If the session doesn't exist, reinitialize the driver -this will automatically create window
                connection?.ReinitializeDriver();
            }
        }

        public static void OpenNewWindow(BrowserConnection connection)
        {

            WebDriverHelper.EnsureValidSessionExists(connection?.Driver);
            try
            {
                connection?.Driver.SwitchTo().NewWindow(WindowType.Window);
            }
            catch (WebDriverException)
            {
                // If the session doesn't exist, reinitialize the driver -this will automatically create window
                connection?.ReinitializeDriver();
            }
        }

        public static void CloseCurrentWindowAndSwitch(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            try
            {
                driver.Close();
                var windowHandles = driver.WindowHandles;
                if (windowHandles.Count > 0)
                {
                    driver.SwitchTo().Window(windowHandles[windowHandles.Count - 1]);
                }
            }
            catch (WebDriverException e)
            {
            }


        }

        public static string GetCurrentWindowHandle(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            return driver.CurrentWindowHandle;
        }

        public static string GetCurrentTitle(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            return driver.Title;
        }

        public static List<string> GetAllWindowHandles(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            return new List<string>(driver.WindowHandles);
        }

        public static void Select(IWebDriver driver, WindowSelectionMethod selectionMethod, string handleOrTitleRegex)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);

            switch (selectionMethod)
            {
                case WindowSelectionMethod.SelectWindowByHandle:
                    driver.SwitchTo().Window(handleOrTitleRegex);
                    break;

                case WindowSelectionMethod.SelectWindowByTitleRegex:
                    SelectWindowByTitle(driver, handleOrTitleRegex);
                    break;

                default:
                    throw new ArgumentException("Invalid selection method");
            }
        }

        private static void SelectWindowByTitle(IWebDriver driver, string titleRegex)
        {
            var originalWindow = driver.CurrentWindowHandle;
            var regex = new Regex(titleRegex);
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                var title = driver.Title;
                if (regex.IsMatch(title))
                {
                    return;
                }
            }

            driver.SwitchTo().Window(originalWindow);
            throw new Exception($"No window with title matching regex '{titleRegex}' found");
        }

        public static void SetSize(IWebDriver driver, WindowState windowState, int? width = null, int? height = null)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);

            switch (windowState)
            {
                case WindowState.WindowMaximized:
                    driver.Manage().Window.Maximize();
                    break;
                case WindowState.WindowMinimized:
                    driver.Manage().Window.Minimize();
                    break;
                case WindowState.WindowFullscreen:
                    driver.Manage().Window.FullScreen();
                    break;
                case WindowState.WindowSpecificDimension:
                    if (width.HasValue && height.HasValue)
                    {
                        driver.Manage().Window.Size = new Size(width.Value, height.Value);
                    }
                    else
                    {
                        throw new ArgumentException("Width and height must be provided for 'dimensions' option");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid window size option");
            }
        }

        public static void CaptureScreenshot(IWebDriver driver, string filePath)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            (driver as ITakesScreenshot)?.GetScreenshot()?.SaveAsFile(filePath);
        }

    }
}