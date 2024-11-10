using System.Collections.Generic;
using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowserAutomationTests;

[TestClass]
public class BrowserSetupTests
{
    private static BrowserConnection _browserConnection;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        _browserConnection = Session.Start(
            BrowserType.Chrome,
            false,
            null,
            null,
            null,
            new List<string>()
        );
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        if (_browserConnection != null) Session.Close(_browserConnection);
    }

    [TestMethod]
    public void TestStartSession()
    {
        Assert.IsNotNull(_browserConnection);
    }

    [TestMethod]
    public void TestOpenBrowser()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Assert.AreEqual("https://www.example.com/", Read.GetCurrentUrl(_browserConnection));
        Window.GoToUrl(_browserConnection, "https://www.google.com", WindowState.WindowSpecificDimension, 1024,
            768);
        Assert.AreEqual("https://www.google.com/", Read.GetCurrentUrl(_browserConnection));
    }
}