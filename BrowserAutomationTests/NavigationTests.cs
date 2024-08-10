using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BrowserAutomationTests
{
    [TestClass]
    public class NavigationTests
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
    }
}