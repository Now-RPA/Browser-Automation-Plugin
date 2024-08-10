using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Helpers;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.Data;

namespace BrowserAutomationPlugin.Core
{
    public enum DisposeOption
    {
        AutoDisposeOn,
        AutoDisposeOff
    }
    [AddIn("BrowserAutomation", Description = "Browser Automation Plugin for RPA", Publisher = "Sumit Kumar", Version = "1.0")]
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
                    return new DisposableBrowserConnection(browserType, profilePath, headless, jsLibrary, driverPath, arguments);
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
    }

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

    [AddIn("Window")]
    public class Window
    {
        public static void Close(BrowserConnection connection)
        {
            WindowActions.CloseCurrentWindowAndSwitch(connection?.Driver);
        }

        public static string GetCurrentHandle(BrowserConnection connection)
        {
            return WindowActions.GetCurrentWindowHandle(connection?.Driver);
        }

        public static string GetCurrentTitle(BrowserConnection connection)
        {
            return WindowActions.GetCurrentTitle(connection?.Driver);
        }

        public static List<string> GetAllHandles(BrowserConnection connection)
        {
            return WindowActions.GetAllWindowHandles(connection?.Driver);
        }

        public static void GoToUrl(BrowserConnection connection, string url, WindowState windowState = WindowState.WindowMaximized,
            int? width = null, int? height = null)
        {
            WindowActions.GoToUrl(connection?.Driver, url, windowState, width, height);
        }

        public static void OpenNewTab(BrowserConnection connection)
        {
            WindowActions.OpenNewTab(connection?.Driver);
        }

        public static void OpenNewWindow(BrowserConnection connection)
        {
            WindowActions.OpenNewWindow(connection?.Driver);
        }

        public static void Select(BrowserConnection connection, string handleOrTitle, WindowSelectionMethod selectionMethod = WindowSelectionMethod.SelectWindowByTitleRegex)
        {
            WindowActions.Select(connection?.Driver, selectionMethod, handleOrTitle);
        }

        public static void SetSize(BrowserConnection connection, WindowState selectMethod = WindowState.WindowMaximized, int? width = null, int? height = null)
        {
            WindowActions.SetSize(connection?.Driver, selectMethod, width, height);
        }

        public static void SaveScreenshot(BrowserConnection connection, string filePath)
        {
            WindowActions.CaptureScreenshot(connection?.Driver, filePath);
        }
    }

    [AddIn("Wait")]
    public class Wait
    {
        public static bool ElementDisplayed(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector, int timeout = 30)
        {
            return WebDriverHelper.WaitForElementToAppear(connection?.Driver, selector, selectorType, timeout);
        }

        public static bool ElementHidden(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector, int timeout = 30)
        {
            return WebDriverHelper.WaitForElementToDisappear(connection?.Driver, selector, selectorType, timeout);
        }

        public static bool ElementLoaded(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
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

    [AddIn("Mouse")]
    public class Mouse
    {
        public static void Click(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.Click(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void ClickAndHold(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            ElementActions.ClickAndHold(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static void DoubleClick(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.DoubleClick(connection?.Driver, selector, selectorType, timeout, attribute,
                interactionMode);
        }

        public static void RightClick(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.RightClick(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void Release(BrowserConnection connection)
        {
            ElementActions.Release(connection?.Driver);
        }

        public static void MoveTo(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            ElementActions.MoveTo(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static void DragAndDrop(BrowserConnection connection, string fromSelector, string toSelector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            ElementActions.DragAndDrop(connection?.Driver, fromSelector, toSelector, selectorType, timeout, attribute);
        }

        public static void ScrollTo(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.ScrollTo(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }
    }

    [AddIn("Input")]
    public class Input
    {
        public static void Clear(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.Clear(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void Check(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.Check(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void Uncheck(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.Uncheck(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void Focus(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.Focus(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void SelectDropdown(BrowserConnection connection, string value, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.SelectDropdown(connection?.Driver, value, selector, selectorType, timeout, attribute,
                interactionMode);
        }

        public static void Submit(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            ElementActions.Submit(connection?.Driver, selector, selectorType, timeout, attribute, interactionMode);
        }

        public static void SendKeys(BrowserConnection connection, string keys, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class", bool performClick = true)
        {
            ElementActions.SendKeys(connection?.Driver, selector, selectorType, keys, timeout, attribute,
                performClick);
        }

        public static void SetValues(BrowserConnection connection, List<object> selectorList, List<object> values,
            SelectorType selectorType = SelectorType.CssSelector)
        {
            ElementActions.SetValues(connection?.Driver, selectorList, values, selectorType);
        }

        public static void SetValue(BrowserConnection connection, string value, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            ElementActions.SetValue(connection?.Driver, value, selector, selectorType, timeout, attribute);
        }
    }

    [AddIn("Read")]
    public class Read
    {
        public static Dictionary<string, string> GetAllValues(BrowserConnection connection, List<object> selectorList, SelectorType selectorType = SelectorType.CssSelector)
        {
            return ReadActions.GetAllValues(connection?.Driver, selectorList, selectorType);
        }

        public static string GetCurrentUrl(BrowserConnection connection)
        {
            return ReadActions.GetCurrentUrl(connection?.Driver);
        }

        public static Dictionary<string, string> GetDetails(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            return ReadActions.GetDetails(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static string GetPageSource(BrowserConnection connection)
        {
            return ReadActions.GetPageSource(connection?.Driver);
        }

        public static DataTable GetTable(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            return ReadActions.GetTable(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static string GetText(BrowserConnection connection, string selector, SelectorType selectorType, int timeout = 30,
            string attribute = "class", Mode interactionMode = Mode.SimulateInteraction)
        {
            return ReadActions.GetText(connection?.Driver, selector, selectorType, timeout, attribute,
                interactionMode);
        }

        public static string GetValue(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            return ReadActions.GetValue(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static bool IsPageLoaded(BrowserConnection connection)
        {
            return ReadActions.IsPageLoaded(connection?.Driver);
        }

        public static bool IsElementDisplayed(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            return ReadActions.IsElementDisplayed(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static bool IsElementEnabled(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            return ReadActions.IsElementEnabled(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static void SaveElementScreenshot(BrowserConnection connection, string filePath, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            ReadActions.CaptureScreenshot(connection?.Driver, filePath, selector, selectorType, timeout, attribute);
        }
    }

    [AddIn("Javascript")]
    public class Javascript
    {
        public static string Execute(BrowserConnection connection, string jsCode)
        {
            return JavascriptActions.Execute(connection?.Driver, jsCode, connection?.Library ?? "");
        }
    }

    [AddIn("Frame")]
    public class Frame
    {
        public static void Select(BrowserConnection connection, string selector, SelectorType selectorType = SelectorType.CssSelector,
            int timeout = 30, string attribute = "class")
        {
            FrameActions.Select(connection?.Driver, selector, selectorType, timeout, attribute);
        }

        public static void Reset(BrowserConnection connection)
        {
            FrameActions.Reset(connection?.Driver);
        }
    }

    [AddIn("Navigation")]
    public class Navigation
    {
        public static void Navigate(BrowserConnection connection, NavigationOption navigateOption = NavigationOption.Refresh)
        {
            NavigationActions.Navigate(connection?.Driver, navigateOption);
        }
    }
}