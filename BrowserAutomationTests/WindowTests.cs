using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace BrowserAutomationTests
{
    [TestClass]
    public class WindowTests
    {
        private static BrowserConnection _browserConnection;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _browserConnection = Session.Start(BrowserType.Chrome, false, null, null, null, new List<string>());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Session.Close(_browserConnection);
        }

        [TestMethod]
        public void TestGoToUrl()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            Assert.AreEqual("https://www.example.com/", Read.GetCurrentUrl(_browserConnection));
        }

        [TestMethod]
        public void TestNavigation()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            string initialUrl = Read.GetCurrentUrl(_browserConnection);

            Window.GoToUrl(_browserConnection, "https://www.google.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);

            Navigation.Navigate(_browserConnection, NavigationOption.NavigateBack);
            Wait.PageLoaded(_browserConnection, 10);

            Assert.AreEqual(initialUrl, Read.GetCurrentUrl(_browserConnection));
        }

        [TestMethod]
        public void TestOpenNewTab()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            List<string> initialWindows = Window.GetAll(_browserConnection);
            Window.OpenNewTab(_browserConnection);
            Wait.PageLoaded(_browserConnection, 10);
            List<string> finalWindows = Window.GetAll(_browserConnection);
            Assert.AreEqual(initialWindows.Count + 1, finalWindows.Count);
        }

        [TestMethod]
        public void TestOpenNewWindow()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            List<string> initialWindows = Window.GetAll(_browserConnection);
            Window.OpenNewWindow(_browserConnection);
            Wait.PageLoaded(_browserConnection, 10);
            List<string> finalWindows = Window.GetAll(_browserConnection);
            Assert.AreEqual(initialWindows.Count + 1, finalWindows.Count);
        }

        [TestMethod]
        public void TestCloseWindow()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Window.OpenNewTab(_browserConnection);
            List<string> initialWindows = Window.GetAll(_browserConnection);
            Window.Close(_browserConnection);
            List<string> finalWindows = Window.GetAll(_browserConnection);
            Assert.AreEqual(initialWindows.Count - 1, finalWindows.Count);
        }

        [TestMethod]
        public void TestGetCurrentTitle()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            string title = Window.GetCurrentTitle(_browserConnection);
            Assert.AreEqual("Example Domain", title);
        }

        [TestMethod]
        public void TestSetWindowSize()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Window.SetSize(_browserConnection, WindowState.WindowSpecificDimension, 1024, 768);

            int width = int.Parse(Javascript.Execute(_browserConnection, "return window.outerWidth;"));
            int height = int.Parse(Javascript.Execute(_browserConnection, "return window.outerHeight;"));

            int allowedError = 4; // Allow 5 pixels of error

            Assert.IsTrue(Math.Abs(768 - height) <= allowedError,
                $"Height {height} is not within {allowedError} pixels of expected 768");
            Assert.IsTrue(Math.Abs(1024 - width) <= allowedError,
                $"Width {width} is not within {allowedError} pixels of expected 1024");
        }

        [TestMethod]
        public void TestSelectWindowByTitle()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            Window.OpenNewTab(_browserConnection);
            Window.GoToUrl(_browserConnection, "https://www.google.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);

            Window.Select(_browserConnection, "Example Domain", WindowSelectionMethod.SelectWindowByTitleRegex);
            Wait.PageLoaded(_browserConnection, 10);

            string currentUrl = Read.GetCurrentUrl(_browserConnection);
            Assert.AreEqual("https://www.example.com/", currentUrl);
        }

        [TestMethod]
        public void TestGetCurrentHandle()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);
            string handle = Window.GetCurrentHandle(_browserConnection);
            Assert.IsNotNull(handle);
            Assert.IsTrue(handle.Length > 0);
        }
        [TestMethod]
        public void TestCaptureFullPageScreenshot()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);

            string screenshotPath = Path.Combine(Path.GetTempPath(), "full_page_screenshot.png");
            Window.SaveScreenshot(_browserConnection, screenshotPath);

            Assert.IsTrue(File.Exists(screenshotPath), "Screenshot file was not created.");
            FileInfo fileInfo = new FileInfo(screenshotPath);
            Assert.IsTrue(fileInfo.Length > 0, "Screenshot file is empty.");
            File.Delete(screenshotPath);
        }

        [TestMethod]
        public void TestCaptureElementScreenshot()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Wait.PageLoaded(_browserConnection, 10);

            string screenshotPath = Path.Combine(Path.GetTempPath(), "element_screenshot.png");
            Read.SaveElementScreenshot(_browserConnection, screenshotPath, "h1", SelectorType.CssSelector, 10, "class");

            Assert.IsTrue(File.Exists(screenshotPath), "Element screenshot file was not created.");
            FileInfo fileInfo = new FileInfo(screenshotPath);
            Assert.IsTrue(fileInfo.Length > 0, "Element screenshot file is empty.");
            File.Delete(screenshotPath);
        }
    }
}