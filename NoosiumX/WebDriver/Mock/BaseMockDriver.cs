namespace NoosiumX.WebDriver.Mock
{
    using Resources.Log;
    using NoosiumX.WebDriver.TestCases.Desktop.Auth;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;
    
    public class BaseMockDriver
    {
        protected static IWebDriver Driver { get; set; } = default!;

        [OneTimeSetUp]
        public void GlobalTestSetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            Driver = new ChromeDriver();
            new TestLog().Debug("The tests were started by the driver.");
            //LoginTests.CheckResponse_ShouldNavigateToMissionPage_WhenValidIdPasswordEntered();
        }

        [OneTimeTearDown]
        public void GlobalTestTearDown()
        {
            //logout
            Driver.Quit();
            new TestLog().Debug("The tests have been completed by the driver.");
        }
    }
}
