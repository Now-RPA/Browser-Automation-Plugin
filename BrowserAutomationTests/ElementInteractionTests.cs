using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace BrowserAutomationTests
{
    [TestClass]
    public class ElementInteractionTests
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
        public void TestClick()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/add_remove_elements/", WindowState.WindowMaximized);
            Mouse.Click(_browserConnection, "//button[text()='Add Element']", SelectorType.XpathSelector, 10, null, Mode.SimulateInteraction);

            bool elementExists = Wait.ElementLoaded(_browserConnection, "//button[text()='Delete']", SelectorType.XpathSelector, 10, null);
            Assert.IsTrue(elementExists);
        }

        [TestMethod]
        public void TestClearInput()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/inputs", WindowState.WindowMaximized);
            Input.SendKeys(_browserConnection, "42", "//input[@type='number']", SelectorType.XpathSelector, 10, null, true);
            string value = Read.GetValue(_browserConnection, "//input[@type='number']", SelectorType.XpathSelector, 10, null);
            Assert.AreEqual("42", value);
            Input.Clear(_browserConnection, "//input[@type='number']", SelectorType.XpathSelector, 10, null, Mode.SimulateInteraction);

            value = Read.GetValue(_browserConnection, "//input[@type='number']", SelectorType.XpathSelector, 10, null);
            Assert.AreEqual("", value);
        }

        [TestMethod]
        public void TestCheckboxOperations()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/checkboxes", WindowState.WindowMaximized);

            Input.Check(_browserConnection, "(//input[@type='checkbox'])[1]", SelectorType.XpathSelector, 10, null, Mode.SimulateInteraction);
            bool isChecked = bool.Parse(Javascript.Execute(_browserConnection, "return document.querySelector(`#checkboxes > input[type=checkbox]:nth-child(1)`).checked"));
            Assert.IsTrue(isChecked);

            Input.Uncheck(_browserConnection, "(//input[@type='checkbox'])[1]", SelectorType.XpathSelector, 10, null, Mode.SimulateInteraction);
            isChecked = bool.Parse(Javascript.Execute(_browserConnection, "return document.querySelector(`#checkboxes > input[type=checkbox]:nth-child(1)`).checked"));
            Assert.IsFalse(isChecked);
        }

        [TestMethod]
        public void TestClickAndHoldRelease()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/drag_and_drop", WindowState.WindowMaximized);
            Mouse.ClickAndHold(_browserConnection, "#column-a", SelectorType.CssSelector, 10, null);
            Thread.Sleep(1000);
            Mouse.Release(_browserConnection);
        }

        [TestMethod]
        public void TestDoubleClick()
        {
            Window.GoToUrl(_browserConnection, "https://testpages.herokuapp.com/styled/events/javascript-events.html", WindowState.WindowMaximized);
            Mouse.DoubleClick(_browserConnection, "#ondoubleclick", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            bool isVisible = Read.IsElementDisplayed(_browserConnection, "#ondoubleclickstatus", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(isVisible);
        }

        [TestMethod]
        public void TestFocus()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/inputs", WindowState.WindowMaximized);
            Input.Focus(_browserConnection, "//input[@type='number']", SelectorType.XpathSelector, 10, null, Mode.SimulateInteraction);
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void TestRightClick()
        {
            Window.GoToUrl(_browserConnection, "https://testpages.herokuapp.com/styled/events/javascript-events.html", WindowState.WindowMaximized);
            Mouse.RightClick(_browserConnection, "#oncontextmenu", SelectorType.CssSelector, 10, null, Mode.SimulateInteraction);
            bool isVisible = Read.IsElementDisplayed(_browserConnection, "#oncontextmenustatus", SelectorType.CssSelector, 10, null);
            Thread.Sleep(2000);
            Assert.IsTrue(isVisible);
        }

        [TestMethod]
        public void TestSelect()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/dropdown", WindowState.WindowMaximized);
            Input.SelectDropdown(_browserConnection, "Option 2", "dropdown", SelectorType.IdSelector, 10, null, Mode.SimulateInteraction);
            string selectedValue = Read.GetValue(_browserConnection, "dropdown", SelectorType.IdSelector, 10, null);
            Assert.AreEqual("2", selectedValue);

            Input.SelectDropdown(_browserConnection, "Option 1", "dropdown", SelectorType.IdSelector, 10, null, Mode.JavascriptInteraction);
            selectedValue = Read.GetValue(_browserConnection, "dropdown", SelectorType.IdSelector, 10, null);
            Assert.AreEqual("1", selectedValue);
        }

        [TestMethod]
        public void TestSubmit()
        {
            Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/login", WindowState.WindowMaximized);
            Input.SetValue(_browserConnection, "tomsmith", "#username", SelectorType.CssSelector, 10, null);
            Input.SetValue(_browserConnection, "SuperSecretPassword!", "password", SelectorType.IdSelector, 10, null);
            Input.Submit(_browserConnection, "//button[@type='submit']", SelectorType.XpathSelector, 10, null, Mode.SimulateInteraction);
            bool loggedIn = Wait.ElementLoaded(_browserConnection, ".flash.success", SelectorType.CssSelector, 10, null);
            Assert.IsTrue(loggedIn);
        }
    }
}