using GoogleMapAutomation.Extensions;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace GoogleMapAutomation.Pages
{
    public class SearchPage : BasePage
    {
        #region Elements
        private IWebElement SearchInput => driver.FindElement(By.Id("searchboxinput"), 10);
        private IWebElement SearchButton => driver.FindElement(By.Id("searchbox-searchbutton"), 10);
        private IWebElement DirectionIcon => driver.FindElement(By.Id("hArJGc"), 10);
        private IWebElement StartPointInput => driver.FindElement(By.XPath("//div[@id='directions-searchbox-0']//input"), 10);
        private IWebElement DestinationInput => driver.FindElement(By.XPath("//div[@id='directions-searchbox-1']//input"), 10);
        private IWebElement DestinationSearchButton => driver.FindElement(By.XPath("//div[@id='directions-searchbox-1']//button[@data-tooltip='Search']"), 10);
        private IWebElement BestTravelModeButton => driver.FindElement(By.XPath("//img[@aria-label='Best travel modes']"), 10);
        private IWebElement ReverseStartAndDestinationButton => driver.FindElement(By.XPath("//button[contains(@data-tooltip,'Reverse starting point and destination')]"), 10);
        private IWebElement SorryMessage => driver.FindElement(By.XPath("//div[@aria-live='assertive' and @role='alert']//div[contains(text(),'Sorry')]"), 10);
        private IWebElement SearchPlaceName => driver.FindElement(By.XPath("//h1[@class='DUwDvf lfPIob']"), 10);
        private IWebElement SearchPleaceDescription => driver.FindElement(By.XPath("//h1[@class='DUwDvf lfPIob']"), 10);
        private IList<IWebElement> SearchResults => driver.FindElements(By.XPath("//h1[contains(@id,'section-directions-trip-title')]"), 10);
        private IList<IWebElement> CannotFindAddress => driver.FindElements(By.XPath("//div[@role='main']//div//div//div"), 10);
        #endregion

        #region Methods

        public void SearchSingleAddress(string placeName)
        {
            SearchInput.EnterText(placeName);
            SearchButton.Click();
        }

        public string GetSearchedPlaceName()
        {
            return SearchPlaceName.GetText();
        }

        public string GetSearchPlaceDescription()
        {
            return SearchPleaceDescription.GetText();

        }

        public void CLickDirectionButton()
        {
            DirectionIcon.Click();
        }

        public void EnterStartPoint(string startPoint)
        {
            StartPointInput.EnterText(startPoint);
        }

        public void EnterDestination(string destination)
        {
            DestinationInput.EnterText(destination);
        }

        public void ClickDestinationSearchButton()
        {
            DestinationSearchButton.Click();
        }

        public void ClickReverseButton()
        {
            ReverseStartAndDestinationButton.Click();
        }

        public string GetStartPoint()
        {
            string text = StartPointInput.GetAttribute("aria-label");
            return text.Remove(0, 14).Trim();
        }

        public string GetDestination()
        {
            string text = DestinationInput.GetAttribute("aria-label");
            return text.Remove(0, 11).Trim();
        }

        public string GetCannotFindMessage()
        {
            string result = string.Empty;
            foreach (var item in CannotFindAddress)
            {
                result = result + " " + item.Text;
            }
            return result;
        }

        public string GetSorryMessage()
        {
            return SorryMessage.GetText();
        }

        public List<string> GetSearchResults()
        {
            return SearchResults.Select(x => x.Text).ToList();
        }

        public bool IsBestTrvelModeEnabled()
        {
            return BestTravelModeButton.IsEnabled();
        }

        #endregion
    }
}
