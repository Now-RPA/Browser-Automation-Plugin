using System.Collections.Generic;
using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowserAutomationTests;

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
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/login");
        var searchList = new List<object> { "#username", "#password" };
        var values = Read.GetAllValues(_browserConnection, searchList);
        Assert.AreEqual(2, values.Count);
    }

    [TestMethod]
    public void TestGetCurrentUrl()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        var url = Read.GetCurrentUrl(_browserConnection);
        Assert.AreEqual("https://www.example.com/", url);
    }

    [TestMethod]
    public void TestGetCurrentWindow()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        var window = Window.GetCurrentHandle(_browserConnection);
        Assert.IsNotNull(window);
    }

    [TestMethod]
    public void TestGetDetails()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        var details = Read.GetDetails(_browserConnection, "h1", SelectorType.CssSelector, 10, null);
        Assert.IsTrue(details.ContainsKey("tag"));
        Assert.AreEqual("h1", details["tag"]);
    }

    [TestMethod]
    public void TestGetPageSource()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        var source = Read.GetPageSource(_browserConnection);
        Assert.IsTrue(source.Contains("<html"));
    }

    [TestMethod]
    public void TestGetTable()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/tables");
        var table = Read.GetTable(_browserConnection, "#table1", SelectorType.CssSelector, 10, null);
        Assert.IsTrue(table.Rows.Count > 0);
    }

    [TestMethod]
    public void TestGetText()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        var text = Read.GetText(_browserConnection, "h1", SelectorType.CssSelector, 10, null);
        Assert.AreEqual("Example Domain", text);
    }

    [TestMethod]
    public void TestGetValue()
    {
        Window.GoToUrl(_browserConnection, "https://the-internet.herokuapp.com/inputs");
        Input.SetValue(_browserConnection, "42", "input[type=number]", SelectorType.CssSelector, 10, null);
        var value = Read.GetValue(_browserConnection, "input[type=number]", SelectorType.CssSelector, 10, null);
        Assert.AreEqual("42", value);
    }

    [TestMethod]
    public void TestGetWindows()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Window.OpenNewTab(_browserConnection);
        var windows = Window.GetAllHandles(_browserConnection);
        Assert.AreEqual(2, windows.Count);
    }

    [TestMethod]
    public void TestIsLoaded()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        var isLoaded = Read.IsPageLoaded(_browserConnection);
        Assert.IsTrue(isLoaded);
    }
}