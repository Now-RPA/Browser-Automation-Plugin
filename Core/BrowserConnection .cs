using System;
using System.Collections.Generic;
using System.Linq;
using BrowserAutomationPlugin.Factories;
using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;

namespace BrowserAutomationPlugin.Core;

public class BrowserConnection
{
    private readonly List<string> _arguments;

    private readonly BrowserType _browserType;
    private readonly string _driverPath;
    private readonly bool _headless;
    private readonly Dictionary<string, object> _preferences;
    private readonly string _profilePath;
    private IWebDriverFactory _factory;

    public BrowserConnection(
        BrowserType browserType = BrowserType.Chrome,
        string profilePath = null,
        bool headless = false,
        string library = "",
        string driverPath = null,
        List<string> arguments = null,
        Dictionary<string, object> preferences = null)
    {
        SeleniumManagerSetup.SetSeleniumManagerPath();
        _browserType = browserType;
        _profilePath = profilePath;
        _headless = headless;
        _driverPath = driverPath;
        _arguments = arguments;
        _preferences = preferences;
        Library = library ?? string.Empty;

        InitializeDriver();
    }

    public IWebDriver Driver { get; private set; }
    public string Library { get; }

    public void Dispose()
    {
        Driver?.Quit();
        Driver = null;
    }

    public void ReinitializeDriver()
    {
        Dispose();
        InitializeDriver();
    }

    public void OpenNewWindow()
    {
        try
        {
            WebDriverHelper.EnsureValidSessionExists(Driver);
            Driver.SwitchTo().NewWindow(WindowType.Window);
        }
        catch (WebDriverException)
        {
            // If the session doesn't exist, reinitialize the driver
            ReinitializeDriver();
            Driver.SwitchTo().NewWindow(WindowType.Window);
        }
    }

    private void InitializeDriver()
    {
        var driverArguments = PrepareDriverArguments(_headless, _profilePath, _arguments);
        var driverPreferences = PrepareDriverPreferences(_preferences);
        _factory = GetWebDriverFactory(_browserType);
        Driver = _factory.CreateDriver(driverArguments, driverPreferences, _driverPath);
        if (Driver == null)
            throw new InvalidOperationException("Unable to initialize the WebDriver.");
    }

    private static IWebDriverFactory GetWebDriverFactory(BrowserType browserType)
    {
        switch (browserType)
        {
            case BrowserType.Chrome:
                return new ChromeWebDriverFactory();
            case BrowserType.Edge:
                return new EdgeWebDriverFactory();
            default:
                throw new ArgumentException($"Unsupported browser type: {browserType}", nameof(browserType));
        }
    }

    private static Dictionary<string, object> PrepareDriverPreferences(Dictionary<string, object> preferences)
    {
        var mergedPreferences = preferences ?? new Dictionary<string, object>();

        // Default preferences
        var defaultPreferences = new Dictionary<string, object>
        {
            ["credentials_enable_service"] = false,
            ["profile.password_manager_enabled"] = false,
            ["autofill.profile_enabled"] = false,
            ["autofill.credit_card_enabled"] = false,
            ["exit_type"] = "Normal",
            ["exited_cleanly"] = true
        };

        // Add default preferences only if not already present
        foreach (var pref in defaultPreferences)
            if (!mergedPreferences.ContainsKey(pref.Key))
                mergedPreferences[pref.Key] = pref.Value;

        return mergedPreferences;
    }

    private static List<string> PrepareDriverArguments(bool headless, string profilePath,
        List<string> userArguments)
    {
        var defaultArguments = new List<string>
        {
            "--no-sandbox",
            "--disable-session-crashed-bubble",
            "--disable-infobars",
            "--disable-restore-session-state",
            "--disable-gpu",
            "--ignore-certificate-errors",
            "--disable-blink-features=AutomationControlled"
        };

        var mergedArguments = new List<string>();
        if (userArguments != null && userArguments.Any()) mergedArguments.AddRange(userArguments);
        foreach (var arg in defaultArguments)
            if (!mergedArguments.Any(a => a.StartsWith(arg)))
                mergedArguments.Add(arg);
        //remove if already present
        mergedArguments.RemoveAll(a => a.Equals("--headless"));
        mergedArguments.RemoveAll(a => a.StartsWith("--user-data-dir="));

        //add if headless mode explicitly stated
        if (headless) mergedArguments.Add("--headless");

        if (!string.IsNullOrWhiteSpace(profilePath)) mergedArguments.Add($"user-data-dir={profilePath}");

        return mergedArguments.Distinct().ToList();
    }
}

public sealed class DisposableBrowserConnection : BrowserConnection, IDisposable
{
    public DisposableBrowserConnection(
        BrowserType browserType = BrowserType.Chrome,
        string profilePath = null,
        bool headless = false,
        string library = "",
        string driverPath = null,
        List<string> arguments = null,
        Dictionary<string, object> preferences = null)
        : base(browserType, profilePath, headless, library, driverPath, arguments, preferences)
    {
    }
}