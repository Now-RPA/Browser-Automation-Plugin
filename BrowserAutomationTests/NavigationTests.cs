using System.Collections.Generic;
using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowserAutomationTests;

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
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);
        var initialUrl = Read.GetCurrentUrl(_browserConnection);

        Window.GoToUrl(_browserConnection, "https://www.google.com");
        Wait.PageLoaded(_browserConnection, 10);

        Navigation.Navigate(_browserConnection, NavigationOption.NavigateBack);
        Wait.PageLoaded(_browserConnection, 10);

        Assert.AreEqual(initialUrl, Read.GetCurrentUrl(_browserConnection));
    }

    [TestMethod]
    public void TestOpenNewTab()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);

        var initialWindows = Window.GetAllHandles(_browserConnection);
        Window.OpenNewTab(_browserConnection);
        Wait.PageLoaded(_browserConnection, 10);

        var finalWindows = Window.GetAllHandles(_browserConnection);
        Assert.AreEqual(initialWindows.Count + 1, finalWindows.Count);
    }

    [TestMethod]
    public void TestOpenNewWindow()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);

        var initialWindows = Window.GetAllHandles(_browserConnection);
        Window.OpenNewWindow(_browserConnection);
        Wait.PageLoaded(_browserConnection, 10);

        var finalWindows = Window.GetAllHandles(_browserConnection);
        Assert.AreEqual(initialWindows.Count + 1, finalWindows.Count);
    }

    [TestMethod]
    public void TestCloseWindow()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Window.OpenNewTab(_browserConnection);
        var initialWindows = Window.GetAllHandles(_browserConnection);
        Window.Close(_browserConnection);
        var finalWindows = Window.GetAllHandles(_browserConnection);
        Assert.AreEqual(initialWindows.Count - 1, finalWindows.Count);
    }
}