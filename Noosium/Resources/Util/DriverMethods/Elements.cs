using System;
using System.Collections.ObjectModel;
using Noosium.WebDriver.Mock;
using OpenQA.Selenium;

namespace Noosium.Resources.Util.DriverMethods;

public class Elements : BaseMockDriver
{
    public static IWebElement GetElementById(string idElement)
    {
        var idX = Driver.FindElement(By.Id(idElement)) ??
                  throw new ArgumentNullException($"Driver.FindElement(By.Id({idElement}))");
        return idX;
    }

    public static ReadOnlyCollection<IWebElement> GetElementsById(string idElement)
    {
        var idX = Driver.FindElements(By.Id(idElement)) ??
                  throw new ArgumentNullException($"Driver.FindElement(By.Id({idElement}))");
        return idX;
    }

    public static IWebElement GetElementByName(string nameElement)
    {
        var idX = Driver.FindElement(By.Name(nameElement)) ??
                  throw new ArgumentNullException($"Driver.FindElement(By.Name({nameElement}))");
        return idX;
    }

    public static IWebElement GetElementByCss(string nameElement)
    {
        var idX = Driver.FindElement(By.CssSelector(nameElement)) ??
                  throw new ArgumentNullException($"Driver.FindElement(By.CssSelector({nameElement}))");
        return idX;
    }

    public static IWebElement GetElementByLinkText(string nameElement)
    {
        var idX =  Driver.FindElement(By.LinkText(nameElement)) ??
                   throw new ArgumentNullException($"Driver.FindElement(By.LinkText({nameElement}))");
        return idX;
    }

}