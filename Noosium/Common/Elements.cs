using System;
using OpenQA.Selenium;

namespace Noosium.Common;

internal static class Elements
{
    #region Driver

    /// <summary>
    /// WebDriver drives a browser natively, as a user would, either locally or on a remote machine using the Selenium server, marks a leap forward in terms of browser automation.
    /// </summary>
    private static readonly IWebDriver Driver = DriverUtilities.Driver;

    #endregion

    #region FindElementBy
    public static IWebElement GetElementById(string idElement)
    {
        var idX = Driver.FindElement(By.Id(idElement)) ??
                  throw new ArgumentNullException($"Driver.FindElement(By.Id({idElement}))");
        return idX;
    }

    public static IWebElement GetElementByName(string nameElement)
    {
        var idX = Driver.FindElement(By.Name(nameElement)) ??
                  throw new ArgumentNullException($"Driver.FindElement(By.Id({nameElement}))");
        return idX;
    }

    #endregion
    
}