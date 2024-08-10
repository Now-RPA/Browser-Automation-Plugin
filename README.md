# BrowserAutomationPlugin

BrowserAutomationPlugin is a custom user plugin for the Now RPA platform by ServiceNow. It adds powerful browser automation capabilities to your RPA projects, allowing you to interact with web applications seamlessly.

## Description

This plugin provides a wide range of actions for browser automation, including navigation, element interaction, JavaScript execution, and more. It supports both Chrome and Edge browsers and can be easily integrated into your Now RPA projects.

## Setup and Installation

Before you can use the BrowserAutomationPlugin in your Now RPA project, you need to set up the user plugin folder:

1. Locate your RPA Desktop Design Studio automation project folder.
2. Create a new folder named `UserPlugins` in this directory.
3. Inside the `UserPlugins` folder, create another folder with a name for your plugin (e.g., "BrowserAutomationPlugin").
4. Place the plugin .dll files in this folder.

To add the plugin to your project:

1. In the RPA Desktop Design Studio, open your project.
2. In the Project Explorer pane, right-click **User Plugins** and select **Add User Plugin**.
3. In the Available User Plugins dialog box, select "BrowserAutomationPlugin".
4. Click **OK**.

The plugin will now appear in the Toolbox pane, ready for use in your automation workflows.

## Element Search Techniques

The plugin supports various element search techniques:

1. **CSS Selector**: 
   Example: `#login-button` (selects element with id "login-button")
   
2. **XPath**: 
   Example: `//button[@type='submit']` (selects all submit buttons)
   
3. **Element ID**: 
   Example: `username` (selects element with id "username")
   
4. **Tag Name**: 
   Example: `input` (selects all input elements)
   
5. **JavaScript**: 
   Example: `document.querySelector('.submit-btn')` (selects first element with class "submit-btn")

## Actions

### Session

#### Start Session
Initializes a new browser session.

Inputs:
- `browserType` (optional): `BrowserType.Chrome` (default) or `BrowserType.Edge`
- `headless` (optional): `false` (default) or `true` for headless mode
- `profilePath` (optional): String path to custom user profile
- `jsLibrary` (optional): String containing custom JavaScript libraries
- `driverPath` (optional): String path to custom WebDriver
- `arguments` (optional): List of additional browser arguments
- `disposeOption` (optional): `DisposeOption.AutoDisposeOn` (default) or `DisposeOption.AutoDisposeOff`

Output: `BrowserConnection` object

#### Close Session
Closes the browser session.

Input: `BrowserConnection` object
Output: None

### Window Actions

#### GoToUrl
Navigates to a specified URL.

Inputs:
- `connection`: BrowserConnection object
- `url`: String URL to navigate to
- `windowState` (optional): WindowState enum (default: WindowMaximized)
- `width` (optional): Integer window width
- `height` (optional): Integer window height

Output: None

#### OpenNewTab
Opens a new browser tab.

Input: `connection`: BrowserConnection object
Output: None

#### OpenNewWindow
Opens a new browser window.

Input: `connection`: BrowserConnection object
Output: None

#### Close
Closes the current window or tab.

Input: `connection`: BrowserConnection object
Output: None

#### GetCurrentHandle
Gets the handle of the current window.

Input: `connection`: BrowserConnection object
Output: String window handle

#### GetCurrentTitle
Gets the title of the current window.

Input: `connection`: BrowserConnection object
Output: String window title

#### GetAllHandles
Gets handles of all open windows.

Input: `connection`: BrowserConnection object
Output: List of String window handles

#### Select
Switches to a specific window or tab.

Inputs:
- `connection`: BrowserConnection object
- `handleOrTitle`: String window handle or title
- `selectionMethod` (optional): WindowSelectionMethod enum (default: SelectWindowByTitleRegex)

Output: None

#### SetSize
Sets the size of the current window.

Inputs:
- `connection`: BrowserConnection object
- `selectMethod` (optional): WindowState enum (default: WindowMaximized)
- `width` (optional): Integer window width
- `height` (optional): Integer window height

Output: None

#### SaveScreenshot
Captures a screenshot of the current window.

Inputs:
- `connection`: BrowserConnection object
- `filePath`: String path to save the screenshot

Output: None

### Alert Actions

#### Accept
Accepts an alert.

Input: `connection`: BrowserConnection object
Output: None

#### Dismiss
Dismisses an alert.

Input: `connection`: BrowserConnection object
Output: None

#### GetText
Gets the text of an alert.

Input: `connection`: BrowserConnection object
Output: String alert text

#### SetText
Sets text in an alert prompt.

Inputs:
- `connection`: BrowserConnection object
- `text`: String text to set in the alert

Output: None

### Input Actions

#### Clear
Clears the content of an input element.

Inputs:
- `connection`: BrowserConnection object
- `selector`: String element selector
- `selectorType` (optional): SelectorType enum (default: CssSelector)
- `timeout` (optional): Integer timeout in seconds (default: 30)
- `attribute` (optional): String attribute to wait for (default: "class")
- `interactionMode` (optional): Mode enum (default: SimulateInteraction)

Output: None

#### Check
Checks a checkbox or radio button.

Inputs: (Same as Clear)

#### Uncheck
Unchecks a checkbox or radio button.

Inputs: (Same as Clear)

#### Focus
Sets focus on an element.

Inputs: (Same as Clear)

#### SelectDropdown
Selects an option from a dropdown.

Inputs:
- `connection`: BrowserConnection object
- `value`: String value to select
- (Other inputs same as Clear)

#### Submit
Submits a form.

Inputs: (Same as Clear)

#### SendKeys
Types text into an input element.

Inputs:
- `connection`: BrowserConnection object
- `keys`: String text to type
- `selector`: String element selector
- `selectorType` (optional): SelectorType enum (default: CssSelector)
- `timeout` (optional): Integer timeout in seconds (default: 30)
- `attribute` (optional): String attribute to wait for (default: "class")
- `performClick` (optional): Boolean to click before typing (default: true)

Output: None

#### SetValues
Sets values for multiple elements.

Inputs:
- `connection`: BrowserConnection object
- `selectorList`: List of String selectors
- `values`: List of String values
- `selectorType` (optional): SelectorType enum (default: CssSelector)

Output: None

#### SetValue
Sets a value for a single element.

Inputs:
- `connection`: BrowserConnection object
- `value`: String value to set
- (Other inputs same as Clear)

Output: None

### Read Actions

#### GetAllValues
Gets values from multiple elements.

Inputs:
- `connection`: BrowserConnection object
- `selectorList`: List of String selectors
- `selectorType` (optional): SelectorType enum (default: CssSelector)

Output: Dictionary<string, string> of element values

#### GetCurrentUrl
Gets the current page URL.

Input: `connection`: BrowserConnection object
Output: String URL

#### GetDetails
Gets details of an element.

Inputs:
- `connection`: BrowserConnection object
- `selector`: String element selector
- `selectorType` (optional): SelectorType enum (default: CssSelector)
- `timeout` (optional): Integer timeout in seconds (default: 30)
- `attribute` (optional): String attribute to wait for (default: "class")

Output: Dictionary<string, string> of element details

#### GetPageSource
Gets the source code of the current page.

Input: `connection`: BrowserConnection object
Output: String page source

#### GetTable
Gets data from an HTML table.

Inputs: (Same as GetDetails)
Output: DataTable containing table data

#### GetText
Gets text content of an element.

Inputs:
- `connection`: BrowserConnection object
- `selector`: String element selector
- `selectorType`: SelectorType enum
- `timeout` (optional): Integer timeout in seconds (default: 30)
- `attribute` (optional): String attribute to wait for (default: "class")
- `interactionMode` (optional): Mode enum (default: SimulateInteraction)

Output: String text content

#### GetValue
Gets the value of an input element.

Inputs: (Same as GetDetails)
Output: String element value

#### IsPageLoaded
Checks if the page is fully loaded.

Input: `connection`: BrowserConnection object
Output: Boolean indicating if page is loaded

#### IsElementDisplayed
Checks if an element is displayed.

Inputs: (Same as GetDetails)
Output: Boolean indicating if element is displayed

#### IsElementEnabled
Checks if an element is enabled.

Inputs: (Same as GetDetails)
Output: Boolean indicating if element is enabled

#### SaveElementScreenshot
Captures a screenshot of a specific element.

Inputs:
- `connection`: BrowserConnection object
- `filePath`: String path to save the screenshot
- (Other inputs same as GetDetails)

Output: None

### JavaScript Actions

#### Execute
Executes custom JavaScript code.

Inputs:
- `connection`: BrowserConnection object
- `jsCode`: String JavaScript code to execute

Output: String result of JavaScript execution

### Frame Actions

#### Select
Switches to a specific frame.

Inputs: (Same as GetDetails)
Output: None

#### Reset
Switches back to the default content.

Input: `connection`: BrowserConnection object
Output: None

### Navigation Actions

#### Navigate
Performs browser navigation (back, forward, refresh).

Inputs:
- `connection`: BrowserConnection object
- `navigateOption` (optional): NavigationOption enum (default: Refresh)

Output: None

### Wait Actions

#### ElementDisplayed
Waits for an element to be displayed.

Inputs:
- `connection`: BrowserConnection object
- `selector`: String element selector
- `selectorType` (optional): SelectorType enum (default: CssSelector)
- `timeout` (optional): Integer timeout in seconds (default: 30)

Output: Boolean indicating if element was displayed within the timeout

#### ElementHidden
Waits for an element to be hidden.

Inputs: (Same as ElementDisplayed)
Output: Boolean indicating if element was hidden within the timeout

#### ElementLoaded
Waits for an element to be loaded in the DOM.

Inputs:
- `connection`: BrowserConnection object
- `selector`: String element selector
- `selectorType` (optional): SelectorType enum (default: CssSelector)
- `timeout` (optional): Integer timeout in seconds (default: 30)
- `attribute` (optional): String attribute to wait for (default: "class")

Output: Boolean indicating if element was loaded within the timeout

#### PageLoaded
Waits for the page to be fully loaded.

Inputs:
- `connection`: BrowserConnection object
- `timeout`: Integer timeout in seconds

Output: Boolean indicating if page was loaded within the timeout

### Mouse Actions

#### Click
Clicks on an element.

Inputs:
- `connection`: BrowserConnection object
- `selector`: String element selector
- `selectorType` (optional): SelectorType enum (default: CssSelector)
- `timeout` (optional): Integer timeout in seconds (default: 30)
- `attribute` (optional): String attribute to wait for (default: "class")
- `interactionMode` (optional): Mode enum (default: SimulateInteraction)

Output: None

#### ClickAndHold
Clicks and holds the mouse button on an element.

Inputs: (Same as Click, except no interactionMode)
Output: None

#### DoubleClick
Double-clicks on an element.

Inputs: (Same as Click)
Output: None

#### RightClick
Right-clicks on an element.

Inputs: (Same as Click)
Output: None

#### Release
Releases the mouse button.

Input: `connection`: BrowserConnection object
Output: None

#### MoveTo
Moves the mouse to an element.

Inputs: (Same as Click, except no interactionMode)
Output: None

#### DragAndDrop
Drags an element and drops it onto another element.

Inputs:
- `connection`: BrowserConnection object
- `fromSelector`: String selector for the element to drag
- `toSelector`: String selector for the drop target
- (Other inputs same as Click, except no interactionMode)

Output: None

#### ScrollTo
Scrolls to an element.

Inputs: (Same as Click)
Output: None

## Best Practices

1. Use appropriate selectors (preferably CSS or ID) for reliable element identification.
2. Set reasonable timeouts based on your application's performance.
3. Use headless mode for faster execution when GUI is not required.
4. Leverage JavaScript interaction mode for complex scenarios or when simulated interactions are unreliable.
5. Always close the browser session when finished to free up resources.

## Troubleshooting

If you encounter issues:

1. Ensure you have the latest version of Chrome or Edge installed.
2. Check that your selectors are correct and unique.
3. Increase timeouts if dealing with slow-loading pages.
4. Use `Read.GetPageSource()` to inspect the page HTML for troubleshooting.
5. Enable verbose logging in Now RPA for detailed execution information.

## Support

For additional support or to report issues, please contact your ServiceNow representative or visit the Now RPA support portal.

## License

This plugin is proprietary software. Use is subject to your ServiceNow license agreement.