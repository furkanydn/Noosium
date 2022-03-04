namespace Noosium.WebDriver.Mock
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
    using System.Collections.Generic;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Opera;
    using OpenQA.Selenium.Safari;

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

        /// <summary>
        /// Check Session method is used to return the current session status.
        /// </summary>
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
        public static void DriverCreateByBrowser()
        {
            var browser = JsonSoft.GetAppSetting("Browser");
            switch (browser.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver();
                    new TestLog().Debug("The Google Chrome Driver was installed using custom settings.");
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    Driver = new EdgeDriver();
                    new TestLog().Debug("The Edge Driver was installed using custom settings.");
                    break;
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    Driver = new FirefoxDriver();
                    new TestLog().Debug("The Firefox Driver was installed using custom settings.");
                    break;
                case "opera":
                    new DriverManager().SetUpDriver(new OperaConfig());
                    Driver = new OperaDriver();
                    new TestLog().Debug("The Opera Driver was installed using custom settings.");
                    break;
                case "safari":
                    Driver = new SafariDriver();
                    new TestLog().Debug("The Safari Driver was installed using custom settings.");
                    break;
                default:
                    new TestLog().Error(
                        "We're sorry, but the driver you requested could not be located. You can contact us to report the issue.");
                    break;
            }
        }

        /// <summary>
        /// In order to create a new session by Selenium WebDriver, the local end should provide the basic capabilities to the remote end. The remote end uses the same set of capabilities to create a session and describes the current session features.
        /// </summary>
        /// <returns>WebDriver provides capabilities.</returns>
        private static Dictionary<string, object> TestCapabilitiesOptions()
        {
            var ltOptions = new Dictionary<string, object>
            {
                {"build", JsonSoft.GetAppSetting("TestBuildName")},
                {"headless", true},
                {"selenium_version", "4.1.0"},
                {"console", "error"},
                {"network", true},
                {"geoLocation", JsonSoft.GetAppSetting("StateCode")}
            };
            return ltOptions;
        }

        #region Driver Options Managers

        /// <summary>
        /// This method is used to start the driver utilizing driver-specific starting options.
        /// </summary>
        /// <returns>Custom Chrome Browser Capabilities</returns>
        private static ChromeOptions ChromeOptionsManager()
        {
            var chromeOptions = new ChromeOptions
            {
                AcceptInsecureCertificates = true,
                PlatformName = JsonSoft.GetAppSetting("Platform")
            };
            chromeOptions.AddAdditionalOption("options", TestCapabilitiesOptions());

            return chromeOptions;
        }

        /// <summary>
        /// This method is used to start the driver utilizing driver-specific starting options.
        /// </summary>
        /// <returns>Custom Edge Browser Capabilities</returns>
        private static EdgeOptions EdgeOptionsManager()
        {
            var edgeOptions = new EdgeOptions
            {
                PlatformName = JsonSoft.GetAppSetting("Platform")
            };
            edgeOptions.AddAdditionalOption("options", TestCapabilitiesOptions());

            return edgeOptions;
        }

        /// <summary>
        /// This method is used to start the driver utilizing driver-specific starting options.
        /// </summary>
        /// <returns>Custom Firefox Browser Capabilities</returns>
        private static FirefoxOptions FirefoxOptionsManager()
        {
            var firefoxOptions = new FirefoxOptions()
            {
                PlatformName = JsonSoft.GetAppSetting("Platform")
            };
            firefoxOptions.AddAdditionalOption("options", TestCapabilitiesOptions());

            return firefoxOptions;
        }

        /// <summary>
        /// This method is used to start the driver utilizing driver-specific starting options.
        /// </summary>
        /// <returns>Custom Edge Browser Capabilities</returns>
        private static OperaOptions OperaOptionsManager()
        {
            var operaOptions = new OperaOptions()
            {
                PlatformName = JsonSoft.GetAppSetting("Platform")
            };
            operaOptions.AddAdditionalOption("options", TestCapabilitiesOptions());

            return operaOptions;
        }

        /// <summary>
        /// This method is used to start the driver utilizing driver-specific starting options.
        /// </summary>
        /// <returns>Custom Edge Browser Capabilities</returns>
        private static SafariOptions SafariOptionsManager()
        {
            var safariOptions = new SafariOptions()
            {
                PlatformName = JsonSoft.GetAppSetting("Platform")
            };
            safariOptions.AddAdditionalOption("options", TestCapabilitiesOptions());

            return safariOptions;
        }

        #endregion
    }
}
