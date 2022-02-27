using NoosiumX.Resources.Log;
using NUnit.Framework;
using OpenQA.Selenium.Mock;

namespace NoosiumX.WebDriver.Mock;

public class BaseMockDriver
{
    protected static readonly MockWebDriver MockDriver = new();

    [OneTimeSetUp]
    public void GlobalTestSetUp()
    {
        new TestLog().Debug("The tests were started by the driver.");
        // login
    }

    [OneTimeTearDown]
    public void GlobalTestTearDown()
    {
        //logout
        MockDriver.Quit();
        new TestLog().Debug("The tests have been completed by the driver.");
    }
}