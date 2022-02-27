using NoosiumX.Resources.Log;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NoosiumX.WebDriver.Mock;

public class BaseMockDriver
{
    protected static IWebDriver Driver { get; set; } = default!;

    [OneTimeSetUp]
    public void GlobalTestSetUp()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        Driver = new ChromeDriver();
        new TestLog().Debug("The tests were started by the driver.");
        // login
    }

    [OneTimeTearDown]
    public void GlobalTestTearDown()
    {
        //logout
        Driver.Quit();
        new TestLog().Debug("The tests have been completed by the driver.");
    }
}