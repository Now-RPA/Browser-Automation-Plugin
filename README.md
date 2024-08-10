# Browser Automation Plugin
BrowserAutomationPlugin is a custom user plugin for the Now RPA platform by ServiceNow. It adds powerful browser automation capabilities to your RPA projects, allowing you to interact with web applications seamlessly.

## Description
This plugin provides a wide range of actions for browser automation, including navigation, element interaction, JavaScript execution, and more. It supports both Chrome and Edge browsers and can be easily integrated into your Now RPA projects.


https://github.com/user-attachments/assets/01a4ff5b-445e-46c8-b84c-d23718131846


## Setup and Installation
Before you can use the BrowserAutomationPlugin in your Now RPA project, you need to set up the user plugin folder:
1. Locate your RPA Desktop Design Studio automation project folder.
2. Create a new folder named `UserPlugins` in this directory.
3. Inside the `UserPlugins` folder, create another folder with a name for your plugin (e.g., "BrowserAutomationPlugin").
4. Place the plugin .dll files and selenium manager in this folder.

To add the plugin to your project:
1. In the RPA Desktop Design Studio, open your project.
2. In the Project Explorer pane, right-click **User Plugins** and select **Add User Plugin**.
3. In the Available User Plugins dialog box, select `BrowserAutomationPlugin`.
 </br>![image](https://github.com/user-attachments/assets/7eede72a-b51a-4ef5-875f-b9f004ca0a6f)
5. Click **OK**.

The plugin will now appear in the Toolbox pane, ready for use in your automation workflows.
![toolbox](https://github.com/user-attachments/assets/1b99c9be-bc4a-4bb2-91fa-1667a6c29e31)

## Best Practices
1. Use appropriate selectors (preferably CSS or ID) for reliable element identification.
2. Set reasonable timeouts based on your application's performance.
3. Use headless mode for faster execution when GUI is not required.
4. Leverage JavaScript interaction mode for complex scenarios or when simulated interactions are unreliable.
5. Always close the browser session when finished to free up resources.

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
   
# Browser Actions Library

## Important: Session Management

Before using any other actions, it's crucial to start a session. The `BrowserConnection` object returned by `Start Session` is required for all other actions.
### Start Session
Initializes a new browser session.
</br>![image](https://github.com/user-attachments/assets/465c0981-b88d-4f92-9ca8-f1ba69f7beac)

- Inputs:
  - `browserType` (optional): `BrowserType.Chrome` (default) or `BrowserType.Edge`
  - `headless` (optional): `false` (default) or `true` for headless mode
  - `profilePath` (optional): String path to custom user profile
  - `jsLibrary` (optional): String containing custom JavaScript libraries
  - `driverPath` (optional): String path to custom WebDriver
  - `arguments` (optional): List of additional browser arguments
  - `disposeOption` (optional): `DisposeOption.AutoDisposeOn` (default) or `DisposeOption.AutoDisposeOff`
- Output: `BrowserConnection` object

### Close Session
Closes the browser session.
- Input: `BrowserConnection` object
- Output: None

**Best Practice**: Always close your session explicitly using `Close Session` or use `AutoDisposeOn` to ensure proper resource management.

## Table of Contents
- [Window Actions](#window-actions)
- [Alert Actions](#alert-actions)
- [Input Actions](#input-actions)
- [Read Actions](#read-actions)
- [JavaScript Actions](#javascript-actions)
- [Frame Actions](#frame-actions)
- [Navigation Actions](#navigation-actions)
- [Wait Actions](#wait-actions)
- [Mouse Actions](#mouse-actions)

## Window Actions

<details>
<summary>Manage browser windows and tabs</summary>

### GoToUrl
- Input:
  - `connection`: BrowserConnection object
  - `url`: String URL to navigate to
  - `windowState` (optional): WindowState enum (default: WindowMaximized)
  - `width` (optional): Integer window width
  - `height` (optional): Integer window height
- Output: None

### OpenNewTab
- Input: `connection`: BrowserConnection object
- Output: None

### OpenNewWindow
- Input: `connection`: BrowserConnection object
- Output: None

### Close
- Input: `connection`: BrowserConnection object
- Output: None

### GetCurrentHandle
- Input: `connection`: BrowserConnection object
- Output: String window handle

### GetCurrentTitle
- Input: `connection`: BrowserConnection object
- Output: String window title

### GetAllHandles
- Input: `connection`: BrowserConnection object
- Output: List of String window handles

### Select
- Input:
  - `connection`: BrowserConnection object
  - `handleOrTitle`: String window handle or title
  - `selectionMethod` (optional): WindowSelectionMethod enum (default: SelectWindowByTitleRegex)
- Output: None

### SetSize
- Input:
  - `connection`: BrowserConnection object
  - `selectMethod` (optional): WindowState enum (default: WindowMaximized)
  - `width` (optional): Integer window width
  - `height` (optional): Integer window height
- Output: None

### SaveScreenshot
- Input:
  - `connection`: BrowserConnection object
  - `filePath`: String path to save the screenshot
- Output: None

</details>

## Alert Actions

<details>
<summary>Handle browser alerts and prompts</summary>

### Accept
- Input: `connection`: BrowserConnection object
- Output: None

### Dismiss
- Input: `connection`: BrowserConnection object
- Output: None

### GetText
- Input: `connection`: BrowserConnection object
- Output: String alert text

### SetText
- Input:
  - `connection`: BrowserConnection object
  - `text`: String text to set in the alert
- Output: None

</details>

## Input Actions

<details>
<summary>Interact with form inputs and controls</summary>

### Clear
- Input:
  - `connection`: BrowserConnection object
  - `selector`: String element selector
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
  - `timeout` (optional): Integer timeout in seconds (default: 30)
  - `attribute` (optional): String attribute to wait for (default: "class")
  - `interactionMode` (optional): Mode enum (default: SimulateInteraction)
- Output: None

### Check
- Input: (Same as Clear)
- Output: None

### Uncheck
- Input: (Same as Clear)
- Output: None

### Focus
- Input: (Same as Clear)
- Output: None

### SelectDropdown
- Input:
  - `connection`: BrowserConnection object
  - `value`: String value to select
  - (Other inputs same as Clear)
- Output: None

### Submit
- Input: (Same as Clear)
- Output: None

### SendKeys
- Input:
  - `connection`: BrowserConnection object
  - `keys`: String text to type
  - `selector`: String element selector
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
  - `timeout` (optional): Integer timeout in seconds (default: 30)
  - `attribute` (optional): String attribute to wait for (default: "class")
  - `performClick` (optional): Boolean to click before typing (default: true)
- Output: None

### SetValues
- Input:
  - `connection`: BrowserConnection object
  - `selectorList`: List of String selectors
  - `values`: List of String values
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
- Output: None

### SetValue
- Input:
  - `connection`: BrowserConnection object
  - `value`: String value to set
  - (Other inputs same as Clear)
- Output: None

</details>

## Read Actions

<details>
<summary>Retrieve information from web pages</summary>

### GetAllValues
- Input:
  - `connection`: BrowserConnection object
  - `selectorList`: List of String selectors
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
- Output: Dictionary<string, string> of element values

### GetCurrentUrl
- Input: `connection`: BrowserConnection object
- Output: String URL

### GetDetails
- Input:
  - `connection`: BrowserConnection object
  - `selector`: String element selector
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
  - `timeout` (optional): Integer timeout in seconds (default: 30)
  - `attribute` (optional): String attribute to wait for (default: "class")
- Output: Dictionary<string, string> of element details

### GetPageSource
- Input: `connection`: BrowserConnection object
- Output: String page source

### GetTable
- Input: (Same as GetDetails)
- Output: DataTable containing table data

### GetText
- Input:
  - `connection`: BrowserConnection object
  - `selector`: String element selector
  - `selectorType`: SelectorType enum
  - `timeout` (optional): Integer timeout in seconds (default: 30)
  - `attribute` (optional): String attribute to wait for (default: "class")
  - `interactionMode` (optional): Mode enum (default: SimulateInteraction)
- Output: String text content

### GetValue
- Input: (Same as GetDetails)
- Output: String element value

### IsPageLoaded
- Input: `connection`: BrowserConnection object
- Output: Boolean indicating if page is loaded

### IsElementDisplayed
- Input: (Same as GetDetails)
- Output: Boolean indicating if element is displayed

### IsElementEnabled
- Input: (Same as GetDetails)
- Output: Boolean indicating if element is enabled

### SaveElementScreenshot
- Input:
  - `connection`: BrowserConnection object
  - `filePath`: String path to save the screenshot
  - (Other inputs same as GetDetails)
- Output: None

</details>

## JavaScript Actions

<details>
<summary>Execute custom JavaScript code</summary>

### Execute
- Input:
  - `connection`: BrowserConnection object
  - `jsCode`: String JavaScript code to execute
- Output: String result of JavaScript execution

</details>

## Frame Actions

<details>
<summary>Navigate between frames on a page</summary>

### Select
- Input: (Same as GetDetails)
- Output: None

### Reset
- Input: `connection`: BrowserConnection object
- Output: None

</details>

## Navigation Actions

<details>
<summary>Control browser navigation</summary>

### Navigate
- Input:
  - `connection`: BrowserConnection object
  - `navigateOption` (optional): NavigationOption enum (default: Refresh)
- Output: None

</details>

## Wait Actions

<details>
<summary>Wait for specific conditions on the page</summary>

### ElementDisplayed
- Input:
  - `connection`: BrowserConnection object
  - `selector`: String element selector
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
  - `timeout` (optional): Integer timeout in seconds (default: 30)
- Output: Boolean indicating if element was displayed within the timeout

### ElementHidden
- Input: (Same as ElementDisplayed)
- Output: Boolean indicating if element was hidden within the timeout

### ElementLoaded
- Input:
  - `connection`: BrowserConnection object
  - `selector`: String element selector
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
  - `timeout` (optional): Integer timeout in seconds (default: 30)
  - `attribute` (optional): String attribute to wait for (default: "class")
- Output: Boolean indicating if element was loaded within the timeout

### PageLoaded
- Input:
  - `connection`: BrowserConnection object
  - `timeout`: Integer timeout in seconds
- Output: Boolean indicating if page was loaded within the timeout

</details>

## Mouse Actions

<details>
<summary>Perform mouse interactions on page elements</summary>

### Click
- Input:
  - `connection`: BrowserConnection object
  - `selector`: String element selector
  - `selectorType` (optional): SelectorType enum (default: CssSelector)
  - `timeout` (optional): Integer timeout in seconds (default: 30)
  - `attribute` (optional): String attribute to wait for (default: "class")
  - `interactionMode` (optional): Mode enum (default: SimulateInteraction)
- Output: None

### ClickAndHold
- Input: (Same as Click, except no interactionMode)
- Output: None

### DoubleClick
- Input: (Same as Click)
- Output: None

### RightClick
- Input: (Same as Click)
- Output: None

### Release
- Input: `connection`: BrowserConnection object
- Output: None

### MoveTo
- Input: (Same as Click, except no interactionMode)
- Output: None

### DragAndDrop
- Input:
  - `connection`: BrowserConnection object
  - `fromSelector`: String selector for the element to drag
  - `toSelector`: String selector for the drop target
  - (Other inputs same as Click, except no interactionMode)
- Output: None

### ScrollTo
- Input: (Same as Click)
- Output: None

</details>
