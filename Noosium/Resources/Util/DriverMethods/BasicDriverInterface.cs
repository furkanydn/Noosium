using System;
using System.Linq.Expressions;
using Noosium.Resources.Common.Private;
using Noosium.WebDriver.Mock;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Noosium.Resources.Util.DriverMethods;

internal class BasicDriverInterface : BaseMockDriver
{
    public static BasicDriverInterface GetCreateInstance()
    {
        return new BasicDriverInterface();
    }

    /// <summary>
    /// It's the mechanism for determining the session status based on the session's username.
    /// </summary>
    /// <returns>If you're logged in, it returns true; otherwise, it returns false.</returns>
    public static bool IsSessionActive() => Driver.FindElements(By.CssSelector(JsonSoft.GetElement(ElementNames.SessionUsername))).Count>0;

    /// <summary>
    /// You can sift through both windows or tabs that WebDriver can see and switch to the other tab if you only have two tabs or windows open and know which one you started with.
    /// </summary>
    /// <param name="preExpression">Provides the base class from which the classes that represent expression tree nodes are derived.</param>
    /// <exception cref="ArgumentException">The exception that is thrown when one of the arguments provided to a method is not valid.</exception>
    public static void SwitchToWindow(Expression<Func<IWebDriver, bool>> preExpression)
    {
        var pre = preExpression.Compile();
        foreach (var handle in Driver.WindowHandles)
        {
            Driver.SwitchTo().Window(handle);
            if (!pre(Driver)) continue;
            return;
        }

        throw new ArgumentException($"Unable to find window with condition: '{preExpression.Body}'");
    }
    
    /// <summary>
    /// Load a new web page in the current browser window. This is done using an HTTP GET operation, and the method will block until the load is complete.
    /// </summary>
    /// <param name="url">The URL to load. It is best to use a fully qualified URL</param>
    public static void NavigateToUrl(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    /// <summary>
    /// Clicks at a set of coordinates using the primary mouse button.
    /// </summary>
    /// <param name="locator">An ICoordinates describing where to click.</param>
    public static void ClickOnElement(By locator)
    {
        WaitAndTimeOut.WaitTimeOut.WaitForElementVisible(locator);
        Driver.FindElement(locator).Click();
    }
    
    /// <summary>
    /// This method is used to select a dropdown from a list based on its identifier.
    /// </summary>
    /// <param name="webElement">Provided locator value</param>
    /// <returns>The element to be wrapped</returns>
    public static SelectElement DropdownMenu(IWebElement webElement)
    {
        var selectElement = new SelectElement(webElement);
        return selectElement;
    }
    
    /// <summary>
    /// This method is used to select a dropdown from a list based on its identifier.
    /// </summary>
    /// <param name="dropDownLocator">Provided locator value</param>
    /// <returns>The element to be wrapped</returns>
    public static SelectElement DropdownMenu(By dropDownLocator)
    {
        var selectElement = new SelectElement(Driver.FindElement(dropDownLocator));
        return selectElement;
    }
    
    /// <summary>
    /// The text value of a specific element is obtained using this method.
    /// </summary>
    /// <param name="locator">Target element</param>
    /// <returns>Gets the innerText of this element, without any leading or trailing whitespace, and with other whitespace collapsed.</returns>
    public static string GetText(By locator)
    {
        var txt = Driver.FindElement(locator).Text;
        return txt;
    }
    
    /// <summary>
    /// Gets the value of the specified attribute for this element.
    /// </summary>
    /// <param name="locator">Target element.</param>
    /// <param name="attrName">The name of the attribute.</param>
    /// <returns>The attribute's current value. Returns a null if the value is not set.</returns>
    public static string GetAttributeValue(By locator, string attrName)
    {
        var txt = Driver.FindElement(locator).GetAttribute(attrName);
        return txt;
    }
    
    /// <summary>
    /// Typing text into the element is simulated.
    /// </summary>
    /// <param name="locator">Target element.</param>
    /// <param name="requiredText">The text to type into the element.</param>
    public static void SendKeys(By locator, string requiredText)
    {
        Driver.FindElement(locator).Clear();
        Driver.FindElement(locator).SendKeys(requiredText);
    }
    
    /// <summary>
    /// Gets the URL the browser is currently displaying.
    /// </summary>
    public static string GetDriverUrl()
    {
        return Driver.Url.Split('?')[0];
    }
    
    /// <summary>
    /// Gets the URL the browser is currently displaying with out split.
    /// </summary>
    public static string GetDriverUrlWithOutSplit()
    {
        return Driver.Url;
    }
}