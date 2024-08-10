using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrowserAutomationPlugin.Helpers
{
    public static class SendKeysProcessor
    {
        private static readonly Dictionary<string, Action<OpenQA.Selenium.Interactions.Actions>> SpecialKeys = InitializeSpecialKeys();

        public static void ProcessInputString(string input, OpenQA.Selenium.Interactions.Actions action)
        {
            var segments = ParseInputString(input);
            foreach (var segment in segments)
            {
                if (SpecialKeys.TryGetValue(segment.ToUpper(), out var specialAction))
                {
                    specialAction(action);
                }
                else
                {
                    action.SendKeys(segment);
                }
            }
        }

        private static List<string> ParseInputString(string input)
        {
            var segments = new List<string>();
            var currentSegment = new StringBuilder();
            int i = 0;

            while (i < input.Length)
            {
                char c = input[i];

                if (c == '[')
                {
                    int endIndex = input.IndexOf(']', i + 1);
                    if (endIndex != -1)
                    {
                        string potentialSpecialKey = input.Substring(i, endIndex - i + 1);
                        if (SpecialKeys.ContainsKey(potentialSpecialKey.ToUpper()))
                        {
                            if (currentSegment.Length > 0)
                            {
                                segments.Add(currentSegment.ToString());
                                currentSegment.Clear();
                            }
                            segments.Add(potentialSpecialKey);
                            i = endIndex + 1;
                            continue;
                        }
                    }
                }

                currentSegment.Append(c);
                i++;
            }

            if (currentSegment.Length > 0)
            {
                segments.Add(currentSegment.ToString());
            }

            return segments;
        }

        private static Dictionary<string, Action<OpenQA.Selenium.Interactions.Actions>> InitializeSpecialKeys()
        {
            return new Dictionary<string, Action<OpenQA.Selenium.Interactions.Actions>>(StringComparer.OrdinalIgnoreCase)
            {
                ["[SHIFT DOWN]"] = a => a.KeyDown(Keys.Shift),
                ["[SHIFT UP]"] = a => a.KeyUp(Keys.Shift),
                ["[CTRL DOWN]"] = a => a.KeyDown(Keys.Control),
                ["[CTRL UP]"] = a => a.KeyUp(Keys.Control),
                ["[ALT DOWN]"] = a => a.KeyDown(Keys.Alt),
                ["[ALT UP]"] = a => a.KeyUp(Keys.Alt),
                ["[ALT-GR DOWN]"] = a => a.KeyDown(Keys.Control).KeyDown(Keys.Alt),
                ["[ALT-GR UP]"] = a => a.KeyUp(Keys.Alt).KeyUp(Keys.Control),
                ["[ENTER]"] = a => a.SendKeys(Keys.Enter),
                ["[RETURN]"] = a => a.SendKeys(Keys.Enter),
                ["[NUM-ENTER]"] = a => a.SendKeys(Keys.Enter),
                ["[BACKSPACE]"] = a => a.SendKeys(Keys.Backspace),
                ["[TAB]"] = a => a.SendKeys(Keys.Tab),
                ["[ESCAPE]"] = a => a.SendKeys(Keys.Escape),
                ["[ESC]"] = a => a.SendKeys(Keys.Escape),
                ["[PAGE UP]"] = a => a.SendKeys(Keys.PageUp),
                ["[PAGE DOWN]"] = a => a.SendKeys(Keys.PageDown),
                ["[HOME]"] = a => a.SendKeys(Keys.Home),
                ["[LEFT ARROW]"] = a => a.SendKeys(Keys.Left),
                ["[UP ARROW]"] = a => a.SendKeys(Keys.Up),
                ["[RIGHT ARROW]"] = a => a.SendKeys(Keys.Right),
                ["[DOWN ARROW]"] = a => a.SendKeys(Keys.Down),
                ["[DELETE]"] = a => a.SendKeys(Keys.Delete),
                ["[INSERT]"] = a => a.SendKeys(Keys.Insert),
                ["[PAUSE]"] = a => a.SendKeys(Keys.Pause),
                ["[END]"] = a => a.SendKeys(Keys.End),
                ["[F1]"] = a => a.SendKeys(Keys.F1),
                ["[F2]"] = a => a.SendKeys(Keys.F2),
                ["[F3]"] = a => a.SendKeys(Keys.F3),
                ["[F4]"] = a => a.SendKeys(Keys.F4),
                ["[F5]"] = a => a.SendKeys(Keys.F5),
                ["[F6]"] = a => a.SendKeys(Keys.F6),
                ["[F7]"] = a => a.SendKeys(Keys.F7),
                ["[F8]"] = a => a.SendKeys(Keys.F8),
                ["[F9]"] = a => a.SendKeys(Keys.F9),
                ["[F10]"] = a => a.SendKeys(Keys.F10),
                ["[F11]"] = a => a.SendKeys(Keys.F11),
                ["[F12]"] = a => a.SendKeys(Keys.F12)
            };
        }
    }
}