using GoogleMapAutomation.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;

namespace GoogleMapAutomation.Helpers
{
    public class BrowserHelper
    {
        [ThreadStatic]
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(Browser browserName)
        {
            switch (browserName)
            {
                case Browser.Chrome:
                    var options = SetChromeOptions();
                    driver = new ChromeDriver(options);
                    break;

                case Browser.Firefox:
                    driver = new FirefoxDriver();
                    break;

                case Browser.Edge:
                    var edgeOptions = SetEdgeOptions();
                    driver = new EdgeDriver(edgeOptions);
                    break;
            }
        }

        private static EdgeOptions SetEdgeOptions()
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("--start-maximized");
            return edgeOptions;
        }

        private static ChromeOptions SetChromeOptions()
        {
            var options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.None;
            options.AddArgument("start-maximized");
            if (Configurations.AppSettings.IsHeadless == true)
            {
                options.AddArgument("--headless");
            }
            return options;
        }

        private static FirefoxOptions SetFireFoxOptions()
        {
            var options = new FirefoxOptions();
            options.PageLoadStrategy = PageLoadStrategy.None;
            options.AddArgument("start-maximized");
            if (Configurations.AppSettings.IsHeadless == true)
            {
                options.AddArgument("--headless");
            }
            return options;
        }

        public static void LoadApplication(string url)
        {
            Driver.Url = url;
        }

        public static void CloseDriver()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
