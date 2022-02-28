using NoosiumX.Resources.Common.Private;

namespace NoosiumX.WebDriver.Mock
{
    using Resources.Log;
    using TestCases.Desktop.Auth;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;
    using Resources.Util;
    using Resources.Util.DriverMethods;
    
    public class BaseMockDriver
    {
        protected static IWebDriver Driver { get; private set; } = default!;

        [OneTimeSetUp]
        public void GlobalTestSetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            Driver = new ChromeDriver();
            new TestLog().Debug("The tests were started by the driver.");
            CheckSession();
        }

        [OneTimeTearDown]
        public void GlobalTestTearDown()
        {
            Driver.FindElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SessionLogOut))).Click();
            Driver.Quit();
            new TestLog().Debug("The tests have been completed by the driver.");
        }

        private static void CheckSession()
        {
            if (!BasicDriverInterface.IsSessionActive())
                LoginTests.CheckResponse_ShouldNavigateToMissionPage_WhenValidIdPasswordEntered();
            else
                new TestLog().Warning(JsonSoft.GetException("AlreadyLoggedIn"));
        }
    }
}
