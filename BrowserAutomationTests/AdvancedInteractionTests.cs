using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace BrowserAutomationTests
{
    [TestClass]
    public class AdvancedInteractionTests
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
        public void TestDragAndDrop()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/drag_and_drop", WindowState.WindowMaximized);
            Mouse.DragAndDrop(_browserConnection, "#column-a", "#column-b", SelectorType.CssSelector, 10, "class");
            string textAfterDrag = Read.GetText(_browserConnection, "#column-a", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            Assert.AreEqual("B", textAfterDrag);
            Thread.Sleep(1000);
            Mouse.DragAndDrop(_browserConnection, "#column-b", "#column-a", SelectorType.CssSelector, 10, null);
            textAfterDrag = Read.GetText(_browserConnection, "#column-a", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            Assert.AreEqual("A", textAfterDrag);
        }

        [TestMethod]
        public void TestExecuteJs()
        {
            Window.GoToUrl(_browserConnection, "https://www.example.com", WindowState.WindowMaximized);
            string result = Javascript.Execute(_browserConnection, "return document.title;");
            Assert.AreEqual("Example Domain", result);
        }

        [TestMethod]
        public void TestMoveTo()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/hovers", WindowState.WindowMaximized);
            Mouse.MoveTo(_browserConnection, ".figure", SelectorType.CssSelector, 10, null);
            bool isVisible = Read.IsElementDisplayed(_browserConnection, ".figure>.figcaption>h5", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(isVisible);
        }

        [TestMethod]
        public void TestScrollTo()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/infinite_scroll", WindowState.WindowMaximized);

            for (int i = 1; i < 10; i++)
            {
                string xpath = $"(//div[@class='jscroll-added'])[{i}]";

                // Use ScrollTo instead of MoveTo
                Mouse.ScrollTo(_browserConnection, xpath, SelectorType.XpathSelector, 10, null, Mode.JavascriptInteraction);

                Thread.Sleep(1000);  // Short pause between scrolls

                // Verify the element is visible after scrolling
                bool isVisible = Read.IsElementDisplayed(_browserConnection, xpath, SelectorType.XpathSelector, 10, null);
                Assert.IsTrue(isVisible, $"Element at index {i} is not visible after scrolling");
            }
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/infinite_scroll", WindowState.WindowMaximized);
        }

        [TestMethod]
        public void TestSelectWindow()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/windows", WindowState.WindowMaximized);
            Mouse.Click(_browserConnection, ".example a", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            Thread.Sleep(2000);
            Window.Select(_browserConnection, "New Window", WindowSelectionMethod.SelectWindowByTitleRegex);
            string newWindowText = Read.GetText(_browserConnection, "h3", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            Thread.Sleep(2000);
            Assert.AreEqual("New Window", newWindowText);
        }

        [TestMethod]
        public void TestSendKeys()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/key_presses", WindowState.WindowMaximized);
            Input.SendKeys(_browserConnection, "Hello World!", "#target", SelectorType.CssSelector, 10, null, true);
            string result = Read.GetValue(_browserConnection, "#target", SelectorType.CssSelector, 10, null);
            Thread.Sleep(2000);
            Assert.AreEqual("Hello World!", result);
        }

        [TestMethod]
        public void TestSpecialSendKeys()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/key_presses", WindowState.WindowMaximized);
            Input.SendKeys(_browserConnection, "[Hello][shift down] World 123![[][shift up][[]", "#target", SelectorType.CssSelector, 10, null, true);
            string result = Read.GetValue(_browserConnection, "#target", SelectorType.CssSelector, 10, null);
            Thread.Sleep(2000);
            Assert.AreEqual("[Hello] WORLD !@#!{{}[[]", result);
        }

        [TestMethod]
        public void TestSetAllValues()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/login", WindowState.WindowMaximized);
            List<object> searchList = new List<object> { "#username", "#password" };
            List<object> valueList = new List<object> { "tomsmith", "SuperSecretPassword!" };
            Input.SetValues(_browserConnection, searchList, valueList, SelectorType.CssSelector);
            string username = Read.GetValue(_browserConnection, "#username", SelectorType.CssSelector, 10, null);
            string password = Read.GetValue(_browserConnection, "#password", SelectorType.CssSelector, 10, null);
            Thread.Sleep(2000);
            Assert.AreEqual("tomsmith", username);
            Assert.AreEqual("SuperSecretPassword!", password);
        }

        [TestMethod]
        public void TestSetValue()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/inputs", WindowState.WindowMaximized);
            Input.SetValue(_browserConnection, "42", "input[type=number]", SelectorType.CssSelector, 10, null);
            string value = Read.GetValue(_browserConnection, "input[type=number]", SelectorType.CssSelector, 10, null);
            Assert.AreEqual("42", value);
        }



        [TestMethod]
        public void TestToDefault()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/iframe", WindowState.WindowMaximized);
            Frame.Select(_browserConnection, "#mce_0_ifr", SelectorType.CssSelector, 10, null);
            Frame.Reset(_browserConnection);
            bool isVisible = Read.IsElementDisplayed(_browserConnection, "h3", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(isVisible);
        }
        [TestMethod]
        public void TestElementEnabled()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/inputs", WindowState.WindowMaximized);
            bool ieEnabled = Read.IsElementEnabled(_browserConnection, "input[type=number]", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(ieEnabled);
        }
    }
}