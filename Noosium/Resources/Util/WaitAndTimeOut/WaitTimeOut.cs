using System;
using Noosium.WebDriver.Mock;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Noosium.Resources.Util.WaitAndTimeOut;

public class WaitTimeOut : BaseMockDriver
{
    public static WaitTimeOut GetCreateInstance()
    {
        return new WaitTimeOut();
    }

    /// <summary>
    /// Specifies the amount of time the driver should wait when searching for an element if it is not immediately present.
    /// </summary>
    /// <param name="seconds">A TimeSpan structure defining the amount of time to wait.</param>
    /// <seealso cref="ITimeouts"/>
    public static void ImplicitWait(int seconds)
    {
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
    }
    
    /// <summary>
    /// Wait will ignore instances of NotFoundException that are encountered (thrown) by default in the 'until' condition, and immediately propagate all others. You can add more to the ignore list by calling ignoring(exceptions to add).
    /// </summary>
    /// <returns>The timeout in seconds</returns>
    public static WebDriverWait WebDriverWait()
    {
        var webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        return webDriverWait;
    }

    /// <summary>
    /// Repeatedly applies this instance's input value to the given function until one of the following occurs:
    /// the function returns neither null nor false,
    /// the function throws an exception that is not in the list of ignored exception types,
    /// the timeout expires.
    /// </summary>
    /// <param name="locator">The type of object on which the wait it to be applied.</param>
    /// <returns>The delegate's expected return type.</returns>
    public static void WaitForElementVisible(By locator)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        wait.Until(condition => condition.FindElement(locator));
    }
}