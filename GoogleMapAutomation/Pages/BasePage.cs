using GoogleMapAutomation.Helpers;
using OpenQA.Selenium;

namespace GoogleMapAutomation.Pages
{
    public class BasePage
    {
        public IWebDriver driver;
        public BasePage()
        {
            this.driver = BrowserHelper.Driver;

        }
    }
}
