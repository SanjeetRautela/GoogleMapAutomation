using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using GoogleMapAutomation.Configurations;
using log4net;
using System;
using TechTalk.SpecFlow;

namespace GoogleMapAutomation.Helpers
{
    [Binding]
    public class GeneralHook
    {
        private static ScenarioContext? _scenarioContext;
        private static AventStack.ExtentReports.ExtentReports? _extentReports;
        private static ExtentV3HtmlReporter? _extentHtmlReporter;
        private static ExtentTest? _feature;
        private static ExtentTest? _scenario;
        private static readonly ILog Logger = Log4NetHelper.GetXmlLogger(typeof(GeneralHook));

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentHtmlReporter = new ExtentV3HtmlReporter(AppDomain.CurrentDomain.BaseDirectory + $"/{AppSettings.TestReportFolderName}/{AppSettings.OutputReportName}");
            _extentHtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            _extentHtmlReporter.Config.ReportName = AppSettings.ReportName;

            _extentReports = new AventStack.ExtentReports.ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);

            _extentReports.AddSystemInfo("Tester", Environment.UserName);
            _extentReports.AddSystemInfo("MachineName", Environment.MachineName);
            _extentReports.AddSystemInfo("Selenium Version", AppSettings.SeleniumVersion);
            _extentReports.AddSystemInfo("Platform", AppSettings.Browser.ToString());

            Logger.Info("BeforeTestRun invoked");
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            if (null != featureContext)
            {
                _feature = _extentReports?.CreateTest<Feature>(featureContext.FeatureInfo.Title,
                    featureContext.FeatureInfo.Description);
                Logger.Info("BeforeFeatureStart invoked");
            }
        }

        [BeforeScenario]
        public static void BeforeScenariostart(ScenarioContext scenarioContext)
        {
            if (null != scenarioContext)
            {
                _scenarioContext = scenarioContext;
                _scenario = _feature?.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
                Logger.Info("BeforeScenariostart invoked");
            }
        }

        [AfterStep]
        public static void AfterEachStep()
        {
            ScenarioBlock scenariodBlock = _scenarioContext.CurrentScenarioBlock;

            if (_scenarioContext.TestError != null)
            {
                switch (scenariodBlock)
                {
                    case ScenarioBlock.Given:
                        _scenario?.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message)
                            .Fail("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(GeneralHook.TakeScreenshot()).Build());
                        Logger.Info($"Failed at step =>  {_scenarioContext.StepContext.StepInfo.Text + _scenarioContext.TestError.Message}");
                        break;
                    case ScenarioBlock.When:
                        _scenario?.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message)
                            .Fail("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(GeneralHook.TakeScreenshot()).Build());
                        Logger.Info($"Failed at step =>  {_scenarioContext.StepContext.StepInfo.Text + _scenarioContext.TestError.Message}");
                        break;
                    case ScenarioBlock.Then:
                        _scenario?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message)
                            .Fail("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(GeneralHook.TakeScreenshot()).Build());
                        Logger.Info($"Failed at step =>  {_scenarioContext.StepContext.StepInfo.Text + _scenarioContext.TestError.Message}");
                        break;
                    default:
                        _scenario?.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message)
                            .Fail("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(GeneralHook.TakeScreenshot()).Build());
                        Logger.Info($"Failed at step => {_scenarioContext.StepContext.StepInfo.Text + _scenarioContext.TestError.Message}");
                        break;
                }
            }
            else if (_scenarioContext.TestError == null)
            {
                switch (scenariodBlock)
                {
                    case ScenarioBlock.Given:
                        _scenario?.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        //  .Pass("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(GeneralHook.TakeScreenshot()).Build());
                        break;
                    case ScenarioBlock.When:
                        _scenario?.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case ScenarioBlock.Then:
                        _scenario?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    default:
                        _scenario?.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);

                        break;
                }
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports?.Flush();

        }

        [AfterScenario]
        public static void AfterScenarioRun()
        {
            BrowserHelper.CloseDriver();

        }

        public static string TakeScreenshot()
        {
            return ((OpenQA.Selenium.ITakesScreenshot)BrowserHelper.Driver).GetScreenshot().AsBase64EncodedString;
        }

    }
}
