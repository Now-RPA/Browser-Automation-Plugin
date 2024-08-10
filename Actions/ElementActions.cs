﻿using BrowserAutomationPlugin.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace BrowserAutomationPlugin.Actions
{
    public static class ElementActions
    {
        public static void Click(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    element.Click();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                    break;
            }
        }

        public static void SendKeys(IWebDriver driver, string selector, SelectorType selectorType, string keys, int timeout, string attribute, bool performClick)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }
            keys = keys.Replace("\r\n", "[ENTER]");
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            SendKeysProcessor.ProcessInputString(keys, actions);
            if (performClick)
            {
                element.Click();
            }
            actions.Perform();
        }

        public static void Clear(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    element.Clear();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                var input = arguments[0];
                                input.value = '';
                                var event = new Event('change', { bubbles: true, cancelable: true, view: window });
                                input.dispatchEvent(event);
                            ", element);
                    break;
            }
        }

        public static void Check(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            if (!element.Selected)
            {
                switch (interactionMode)
                {
                    case Mode.SimulateInteraction:
                        element.Click();
                        break;
                    case Mode.JavascriptInteraction:
                        ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                var input = arguments[0];
                                input.checked = true;
                                var event = new Event('change', { bubbles: true, cancelable: true, view: window });
                                input.dispatchEvent(event);
                            ", element);
                        break;
                }
            }
        }

        public static void Uncheck(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            if (element.Selected)
            {
                switch (interactionMode)
                {
                    case Mode.SimulateInteraction:
                        element.Click();
                        break;
                    case Mode.JavascriptInteraction:
                        ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                var input = arguments[0]
                                input.checked = false;
                                var event = new Event('change', { bubbles: true, cancelable: true, view: window });
                                input.dispatchEvent(event);
                            ", element);

                        break;
                }
            }
        }

        public static void Focus(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    new OpenQA.Selenium.Interactions.Actions(driver).MoveToElement(element).Perform();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                window.focus();
                                arguments[0].focus();
                                ", element);
                    break;
            }
        }

        public static void SelectDropdown(IWebDriver driver, string value, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    new SelectElement(element).SelectByText(value);
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                var dropdown = arguments[0];
                                var optionText = arguments[1];
                                var option = Array.from(dropdown.options).find(option => option.text === optionText);
                                if (option) {
                                    option.selected = true;
                                    dropdown.dispatchEvent(new Event('change', { bubbles: true, cancelable: true }));
                                }
                                var event = new Event('change', { bubbles: true, cancelable: true, view: window });
                                dropdown.dispatchEvent(event);
                            ", element, value);
                    break;
            }
        }

        public static void Submit(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    element.Submit();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].submit();", element);
                    break;
            }
        }

        public static void DragAndDrop(IWebDriver driver, string fromSelector, string toSelector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var sourceElement = WebDriverHelper.WaitForElementWithAttribute(driver, fromSelector, selectorType, attribute, timeout);
            var targetElement = WebDriverHelper.WaitForElementWithAttribute(driver, toSelector, selectorType, attribute, timeout);

            if (sourceElement == null)
            {
                throw new Exception("From Element did not load within timeout, ensure selector method and selector are correct");
            }
            if (targetElement == null)
            {
                throw new Exception("To Element did not load within timeout, ensure selector method and selector are correct");
            }

            new OpenQA.Selenium.Interactions.Actions(driver).DragAndDrop(sourceElement, targetElement).Perform();
        }

        public static void DoubleClick(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    new OpenQA.Selenium.Interactions.Actions(driver).DoubleClick(element).Perform();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                var event = new MouseEvent('dblclick',
                                { bubbles: true, cancelable: true, view: window });
                                arguments[0].dispatchEvent(event);
                                ", element);
                    break;
            }
        }

        public static void RightClick(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    new OpenQA.Selenium.Interactions.Actions(driver).ContextClick(element).Perform();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                var event = new MouseEvent('contextmenu',
                                { bubbles: true, cancelable: true, view: window });
                                arguments[0].dispatchEvent(event);
                                ", element);
                    break;
            }
        }
        public static void ClickAndHold(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }
            new OpenQA.Selenium.Interactions.Actions(driver).ClickAndHold(element).Perform();
        }

        public static void ScrollTo(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute, Mode interactionMode)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            switch (interactionMode)
            {
                case Mode.SimulateInteraction:
                    new OpenQA.Selenium.Interactions.Actions(driver).ScrollToElement(element).Perform();
                    break;
                case Mode.JavascriptInteraction:
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"
                                arguments[0].scrollIntoView({behavior: 'smooth', block: 'center', inline: 'nearest'});
                                ", element);
                    break;
            }
        }
        public static void MoveTo(IWebDriver driver, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception($"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }
            new OpenQA.Selenium.Interactions.Actions(driver).MoveToElement(element).Perform();
        }

        public static void Release(IWebDriver driver)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            new OpenQA.Selenium.Interactions.Actions(driver).Release().Perform();
        }

        public static void SetValues(IWebDriver driver, List<object> selectorList, List<object> values, SelectorType selectorType)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            for (int i = 0; i < selectorList.Count && i < values.Count; i++)
            {
                var element = WebDriverHelper.GetElement(driver, selectorList[i].ToString(), selectorType);
                if (element == null)
                {
                    throw new Exception($"Element not found, ensure selector method and selector are correct: Search by {selectorType}, selector: {selectorList[i]}");
                }

                ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            arguments[0].value = arguments[1];
                            arguments[0].dispatchEvent(new Event('change',{ bubbles: true, cancelable: true, view: window }));
                        ", element, values[i].ToString());
            }
        }

        public static void SetValue(IWebDriver driver, string value, string selector, SelectorType selectorType, int timeout, string attribute)
        {
            WebDriverHelper.EnsureValidSessionExists(driver);
            var element = WebDriverHelper.WaitForElementWithAttribute(driver, selector, selectorType, attribute, timeout);
            if (element == null)
            {
                throw new Exception(
                    $"Element not found within timeout, ensure selector type and selector are correct: Search by {selectorType}, selector: {selector}, attribute: {attribute}");
            }

            ((IJavaScriptExecutor)driver).ExecuteScript(@"
                            arguments[0].value = arguments[1];
                            arguments[0].dispatchEvent(new Event('change',{ bubbles: true, cancelable: true, view: window }));
                        ", element, value);

        }
    }
}