using OpenQA.Selenium;
using System;

namespace GoogleMapAutomation.Extensions
{
    public static class ElementExtension
    {
        public static void EnterText(this IWebElement element, string text, bool isClear = true)
        {
            if (isClear)
            {
                element.Clear();
            }
            //Log.Info("Clear action is performed on an xyz element");
            element.SendKeys(text);
        }
        public static bool IsEnabled(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Enabled;
            }
            catch (Exception)
            {
                result = false;
            }
        
            return result;
        }
        public static string GetText(this IWebElement element)
        {
            return element.Text;
        }

    }
}
