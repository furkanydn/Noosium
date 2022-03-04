using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Noosium.WebDriver.Mock;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Noosium.Resources.Util.DriverMethods;

public class CollectionMethods : BaseMockDriver
{
    /// <summary>
    /// Finds all IWebElements within the current context using the given mechanism.
    /// </summary>
    /// <param name="locator">The locating mechanism to use.</param>
    /// <returns>A <see cref="IReadOnlyCollection{T}"/> of all <see cref="IWebElement"/> matching the current criteria, or an empty list if nothing matches.</returns>
    public static List<IWebElement> GetCollection(By locator)
    {
        List<IWebElement> webElements = Driver.FindElements(locator).ToList();
        return webElements;
    }

    /// <summary>
    /// Clicks (without releasing) in the middle of the given element.
    /// </summary>
    /// <param name="source">Element to move to and click.</param>
    /// <param name="destination">Element to (without releasing) at the destination location.</param>
    public static void DragAndDrop(IWebElement source, IWebElement destination)
    {
        var actions = new Actions(Driver);
        actions.DragAndDrop(source,destination).Build().Perform();
    }

    /// <summary>
    /// Select item currently value in the List Dropdown
    /// </summary>
    /// <param name="webElements">The first matching IWebElement on the current context.</param>
    /// <param name="itemName">Element matches the criteria.</param>
    public static void SelectFromListDropDownAndClick(IEnumerable<IWebElement> webElements, string itemName)
    {
        if (itemName.Length < 1) return;
        foreach (var item in webElements)
        {
            if (item.Text != itemName) continue;
            Thread.Sleep(1500);
            item.Click();
            break;
        }
    }
}