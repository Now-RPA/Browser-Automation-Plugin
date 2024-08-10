using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;

namespace BrowserAutomationTests
{
    [TestClass]
    public class BrowserInfoTests
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
        public void TestGetAllValues()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/login", WindowState.WindowMaximized);
            List<object> searchList = new List<object> { "#username", "#password" };
            Dictionary<string, string> values = Read.GetAllValues(_browserConnection, searchList, SelectorType.CssSelector);
            Assert.AreEqual(2, values.Count);
        }

        [TestMethod]
        public void TestGetCurrentUrl()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            string url = Read.GetCurrentUrl(_browserConnection);
            Assert.AreEqual("https://www.example.com/", url);
        }

        [TestMethod]
        public void TestGetCurrentWindow()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            string window = Window.GetCurrentHandle(_browserConnection);
            Assert.IsNotNull(window);
        }

        [TestMethod]
        public void TestGetDetails()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Dictionary<string, string> details = Read.GetDetails(_browserConnection, "h1", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(details.ContainsKey("tag"));
            Assert.AreEqual("h1", details["tag"]);
        }

        [TestMethod]
        public void TestGetPageSource()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            string source = Read.GetPageSource(_browserConnection);
            Assert.IsTrue(source.Contains("<html"));
        }

        [TestMethod]
        public void TestGetTable()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/tables", WindowState.WindowMaximized);
            DataTable table = Read.GetTable(_browserConnection, "#table1", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(table.Rows.Count > 0);
        }

        [TestMethod]
        public void TestGetText()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            string text = Read.GetText(_browserConnection, "h1", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            Assert.AreEqual("Example Domain", text);
        }

        [TestMethod]
        public void TestGetValue()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/inputs", WindowState.WindowMaximized);
            Input.SetValue(_browserConnection, "42", "input[type=number]", SelectorType.CssSelector, 10, null);
            string value = Read.GetValue(_browserConnection, "input[type=number]", SelectorType.CssSelector, 10, null);
            Assert.AreEqual("42", value);
        }

        [TestMethod]
        public void TestGetWindows()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            Window.OpenNewTab(_browserConnection);
            List<string> windows = Window.GetAllHandles(_browserConnection);
            Assert.AreEqual(2, windows.Count);
        }

        [TestMethod]
        public void TestIsLoaded()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            bool isLoaded = Read.IsPageLoaded(_browserConnection);
            Assert.IsTrue(isLoaded);
        }
    }
}