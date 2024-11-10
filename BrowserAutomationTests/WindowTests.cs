using System;
using System.Collections.Generic;
using System.IO;
using BrowserAutomationPlugin.Actions;
using BrowserAutomationPlugin.Core;
using BrowserAutomationPlugin.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrowserAutomationTests;

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
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);
        Assert.AreEqual("https://www.example.com/", Read.GetCurrentUrl(_browserConnection));
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
    public void TestCloseWindow()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Window.OpenNewTab(_browserConnection);
        var initialWindows = Window.GetAllHandles(_browserConnection);
        Window.Close(_browserConnection);
        var finalWindows = Window.GetAllHandles(_browserConnection);
        Assert.AreEqual(initialWindows.Count - 1, finalWindows.Count);
        Window.Close(_browserConnection);
        //even if window does not exist, close should not throw error since it is already closed
        Window.Close(_browserConnection);
        Window.OpenNewWindow(_browserConnection);
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        finalWindows = Window.GetAllHandles(_browserConnection);
        Assert.AreEqual(1, finalWindows.Count);
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
    public void TestGetCurrentTitle()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);
        var title = Window.GetCurrentTitle(_browserConnection);
        Assert.AreEqual("Example Domain", title);
        Window.Close(_browserConnection);
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);
        title = Window.GetCurrentTitle(_browserConnection);
        Assert.AreEqual("Example Domain", title);
        Window.Close(_browserConnection);
    }

    [TestMethod]
    public void TestSetWindowSize()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Window.SetSize(_browserConnection, WindowState.WindowSpecificDimension, 1024, 768);

        var width = int.Parse(Javascript.Execute(_browserConnection, "return window.outerWidth;"));
        var height = int.Parse(Javascript.Execute(_browserConnection, "return window.outerHeight;"));

        var allowedError = 4; // Allow 5 pixels of error

        Assert.IsTrue(Math.Abs(768 - height) <= allowedError,
            $"Height {height} is not within {allowedError} pixels of expected 768");
        Assert.IsTrue(Math.Abs(1024 - width) <= allowedError,
            $"Width {width} is not within {allowedError} pixels of expected 1024");
    }

    [TestMethod]
    public void TestSelectWindowByTitle()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);
        Window.OpenNewTab(_browserConnection);
        Window.GoToUrl(_browserConnection, "https://www.google.com");
        Wait.PageLoaded(_browserConnection, 10);

        Window.SelectByTitle(_browserConnection, "Example Domain");
        Wait.PageLoaded(_browserConnection, 10);

        var currentUrl = Read.GetCurrentUrl(_browserConnection);
        Assert.AreEqual("https://www.example.com/", currentUrl);
    }

    [TestMethod]
    public void TestGetCurrentHandle()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);
        var handle = Window.GetCurrentHandle(_browserConnection);
        Assert.IsNotNull(handle);
        Assert.IsTrue(handle.Length > 0);
    }

    [TestMethod]
    public void TestCaptureFullPageScreenshot()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);

        var screenshotPath = Path.Combine(Path.GetTempPath(), "full_page_screenshot.png");
        Window.SaveScreenshot(_browserConnection, screenshotPath);

        Assert.IsTrue(File.Exists(screenshotPath), "Screenshot file was not created.");
        var fileInfo = new FileInfo(screenshotPath);
        Assert.IsTrue(fileInfo.Length > 0, "Screenshot file is empty.");
        File.Delete(screenshotPath);
    }

    [TestMethod]
    public void TestCaptureElementScreenshot()
    {
        Window.GoToUrl(_browserConnection, "https://www.example.com");
        Wait.PageLoaded(_browserConnection, 10);

        var screenshotPath = Path.Combine(Path.GetTempPath(), "element_screenshot.png");
        Read.SaveElementScreenshot(_browserConnection, screenshotPath, "h1", SelectorType.CssSelector, 10);

        Assert.IsTrue(File.Exists(screenshotPath), "Element screenshot file was not created.");
        var fileInfo = new FileInfo(screenshotPath);
        Assert.IsTrue(fileInfo.Length > 0, "Element screenshot file is empty.");
        File.Delete(screenshotPath);
    }
}