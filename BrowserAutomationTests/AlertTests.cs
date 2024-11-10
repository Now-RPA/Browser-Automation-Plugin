using System.Collections.Generic;
using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowserAutomationTests;

[TestClass]
public class AlertTests
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
    public void TestAlertAccept()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/javascript_alerts");
        Mouse.Click(_browserConnection, "//button[text()='Click for JS Alert']", SelectorType.XpathSelector, 10,
            null);
        Alerts.Accept(_browserConnection);
        var result = Read.GetText(_browserConnection, "result", SelectorType.IdSelector, 10, null);
        Assert.AreEqual("You successfully clicked an alert", result);
    }

    [TestMethod]
    public void TestAlertDismiss()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/javascript_alerts");
        Mouse.Click(_browserConnection, "//button[text()='Click for JS Confirm']", SelectorType.XpathSelector, 10,
            null);
        Alerts.Dismiss(_browserConnection);
        var result = Read.GetText(_browserConnection, "result", SelectorType.IdSelector, 10, null);
        Assert.AreEqual("You clicked: Cancel", result);
    }

    [TestMethod]
    public void TestAlertGetText()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/javascript_alerts");
        Mouse.Click(_browserConnection, "//button[@onclick='jsAlert()']", SelectorType.XpathSelector, 10, null);
        var alertText = Alerts.GetText(_browserConnection);
        Alerts.Accept(_browserConnection);
        Assert.AreEqual("I am a JS Alert", alertText);
    }

    [TestMethod]
    public void TestAlertSetText()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/javascript_alerts");
        Mouse.Click(_browserConnection, "button[onclick='jsPrompt()']", SelectorType.CssSelector, 10, null);
        Alerts.SetText(_browserConnection, "Hello, World!");
        Alerts.Accept(_browserConnection);
        var result = Read.GetText(_browserConnection, "#result", SelectorType.CssSelector, 10, null);
        Assert.AreEqual("You entered: Hello, World!", result);
    }
}