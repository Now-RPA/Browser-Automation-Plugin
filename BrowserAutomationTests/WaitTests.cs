using System.Collections.Generic;
using System.Threading;
using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowserAutomationTests;

[TestClass]
public class WaitTests
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
    public void TestWaitLoadedElement()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/dynamic_loading/2");
        Mouse.Click(_browserConnection, "#start button", SelectorType.CssSelector, 20, null);
        var isLoaded = Wait.ElementLoaded(_browserConnection, "#finish", SelectorType.CssSelector, 25, null);
        Thread.Sleep(1000);
        Assert.IsTrue(isLoaded);
    }

    [TestMethod]
    public void TestWaitElementAppear()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/dynamic_loading/1");
        Mouse.Click(_browserConnection, "#start button", SelectorType.CssSelector, 20, null);
        var isLoaded = Wait.ElementDisplayed(_browserConnection, "#finish", SelectorType.CssSelector, 25);
        Thread.Sleep(1000);
        Assert.IsTrue(isLoaded);
    }

    [TestMethod]
    public void TestWaitElementDisAppear()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/dynamic_loading/1");

        var isUnLoaded = Wait.ElementHidden(_browserConnection, "#start", SelectorType.CssSelector, 5);
        Thread.Sleep(1000);
        Assert.IsFalse(isUnLoaded);

        Mouse.Click(_browserConnection, "#start button", SelectorType.CssSelector, 20, null);

        isUnLoaded = Wait.ElementHidden(_browserConnection, "#start", SelectorType.CssSelector, 5);
        Thread.Sleep(1000);
        Assert.IsTrue(isUnLoaded);
    }

    [TestMethod]
    public void TestWaitLoadedPage()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com");
        var isLoaded = Wait.PageLoaded(_browserConnection, 10);
        Thread.Sleep(1000);
        Assert.IsTrue(isLoaded);
    }
}