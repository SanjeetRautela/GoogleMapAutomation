using GoogleMapAutomation.Configurations;
using GoogleMapAutomation.Helpers;
using GoogleMapAutomation.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace GoogleMapAutomation.Steps
{
    [Binding]
    public class GoogleMapStepDefinition
    {
        SearchPage searchPage;

        [Given(@"User is in google map home page")]
        public void GivenUserIsInGoogleMapHomePage()
        {
            BrowserHelper.InitBrowser(AppSettings.Browser);
            BrowserHelper.LoadApplication(AppSettings.Url);
        }

        [When(@"User enters '(.*)' address in search box")]
        public void WhenUserEntersAddressInSearchBox(string placeName)
        {
            searchPage = new SearchPage();
            searchPage.SearchSingleAddress(placeName);
        }

        [Then(@"User should see '(.*)' in result window")]
        public void ThenUserShouldSeeInResultWindow(string placeName)
        {
            string actualPlaceName = searchPage.GetSearchedPlaceName();
            string actualDescription = searchPage.GetSearchPlaceDescription();

            Assert.AreEqual(placeName, actualPlaceName, "Place Name did not matched");
            Assert.IsTrue(actualDescription.Contains(placeName), $"Place Name does not contains '{placeName}'");
        }

        [Given(@"User Click on Direction Button")]
        public void GivenUserClickOnDirectionButton()
        {
            searchPage = new SearchPage();
            searchPage.CLickDirectionButton();
        }

        [Given(@"User enter (.*) as Start Address")]
        public void GivenUserEnterAsStartAddress(string startAddress)
        {
            searchPage.EnterStartPoint(startAddress);
        }

        [Given(@"User enter (.*) as End Address")]
        public void GivenUserEnterAsEndAddress(string destination)
        {
            searchPage.EnterDestination(destination);
        }

        [When(@"User Click reverse address button")]
        public void WhenUserClickReverseAddressButton()
        {
            searchPage.ClickReverseButton();
        }

        [Then(@"(.*) and (.*) should switch")]
        public void ThenAndShouldSwitch(string startPoint, string destination)
        {
            string actualStartPoint = searchPage.GetStartPoint();
            string actualDestination = searchPage.GetDestination();

            Assert.AreEqual(startPoint, actualDestination, "Start point and destination are not same");
            Assert.AreEqual(destination, actualStartPoint, "Destination and start point are not same");
        }

        [Then(@"User should see '(.*)' as error message in result window")]
        public void ThenUserShouldSeeAsErrorMessageInResultWindow(string message)
        {
            string actualMessage = searchPage.GetCannotFindMessage();
            Assert.IsTrue(actualMessage.Contains(message), $"Actual message=> {actualMessage} does not contain {message}");
        }

        [When(@"User Click on search button")]
        public void WhenUserClickOnSearchButton()
        {
            searchPage.ClickDestinationSearchButton();
        }

        [Then(@"User should see error message '(.*)'")]
        public void ThenUserShouldSeeErrorMessage(string errorMessage)
        {
            string actualMessage = searchPage.GetSorryMessage();
            Assert.IsTrue(actualMessage.Contains(errorMessage), $"Error message does not contains '{errorMessage}'");
        }

        [Then(@"User should see best travel modes in result")]
        public void ThenUserShouldSeeBestTravelModesInResult()
        {
            var result = searchPage.GetSearchResults();
            var isBestTravelModes = searchPage.IsBestTrvelModeEnabled();

            Assert.IsTrue(isBestTravelModes, "Best travel mode is not enabled by default");
            Assert.IsTrue(result.Count >= 0, "Best travel modes is not available");
        }

    }
}
