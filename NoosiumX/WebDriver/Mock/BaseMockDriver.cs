using System.Collections.Generic;
using OpenQA.Selenium.DevTools;

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
    using Resources.Common.Private;
    
    public class BaseMockDriver
    {
        protected static IWebDriver Driver { get; private set; } = default!;

        [OneTimeSetUp]
        public void GlobalTestSetUp()
        {
            new TestLog().Debug("The tests were started by the driver.");
            DriverCreateByBrowser();
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
        
        /// <summary>
        /// This method is used to launch the browser (driver) based on the browser configured in DriverOptionsManager.
        /// </summary>
        private static void DriverCreateByBrowser()
        {
            var browser = JsonSoft.GetAppSetting("Browser");
            switch (browser.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver(DriverOptionsManager());
                    new TestLog().Debug("The Google Chrome Driver was installed using custom settings.");
                    break;
                case "edge":
                    break;
            }
        }

        /// <summary>
        /// This method is used to start the driver utilizing driver-specific starting options.
        /// </summary>
        /// <returns>Custom Capabilities</returns>
        private static ChromeOptions DriverOptionsManager()
        {
            
            var ltOptions = new Dictionary<string, object>
            {
                {"build", JsonSoft.GetAppSetting("TestBuildName")},
                {"headless",true},
                {"selenium_version","4.1.0"},
                {"console", "error"},
                {"network", true},
                {"geoLocation", JsonSoft.GetAppSetting("StateCode")}
            };

            var chromeOptions = new ChromeOptions
            {
                AcceptInsecureCertificates = true,
                PlatformName = JsonSoft.GetAppSetting("Platform")
            };
            chromeOptions.AddAdditionalOption("options",ltOptions);

            return chromeOptions;
        }
    }
}
