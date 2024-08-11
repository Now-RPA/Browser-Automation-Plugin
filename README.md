# Browser Automation Plugin
BrowserAutomationPlugin is a custom user plugin for the Now RPA platform by ServiceNow. It adds powerful browser automation capabilities to your RPA projects, allowing you to interact with web applications seamlessly.

## Description
This plugin provides a wide range of actions for browser automation, including navigation, element interaction, JavaScript execution, and more. It supports both Chrome and Edge browsers and can be easily integrated into your Now RPA projects.


https://github.com/user-attachments/assets/1aae221c-d578-48fa-b196-a93d3901312a



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
   
## Important: Session Management

Before using any other actions, it's crucial to start a session. The `BrowserConnection` object returned by `Start Session` is required for all other actions.
### Start Session
Initializes a new browser session.
</br>![image](https://github.com/user-attachments/assets/465c0981-b88d-4f92-9ca8-f1ba69f7beac)

- Inputs:
  - `browserType`: `BrowserType.Chrome` (default) or `BrowserType.Edge`
  - `headless` (optional): `false` (default) or `true` for headless mode
  - `profilePath` (optional): String path to custom user profile
  - `jsLibrary` (optional): String containing custom JavaScript libraries
  - `driverPath` (optional): String path to custom WebDriver
  - `arguments` (optional): List of additional browser arguments
  - `disposeOption` : `DisposeOption.AutoDisposeOn` (default) or `DisposeOption.AutoDisposeOff`
- Output: `BrowserConnection` object

### Close Session
Closes the browser session.
- Input: `BrowserConnection` object
- Output: None

**Best Practice**: Always close your session explicitly using `Close Session` or use `AutoDisposeOn` to ensure proper resource management.
