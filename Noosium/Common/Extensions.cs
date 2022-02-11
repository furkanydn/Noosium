using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Noosium.Common;

public static class Extensions
{
    /// <summary>
    /// Select an option by the text displayed.
    /// </summary>
    /// <param name="webElement">Select element.</param>
    /// <param name="optionValue">The text of the option to be selected. If an exact match is not found, this method will perform a substring match.</param>
    public static void GetValuesFromDropDown(this IWebElement webElement, string optionValue)
    {
        var selectElement = new SelectElement(webElement);
        selectElement.SelectByText(optionValue);
    }

    /// <summary>
    /// Gets the list of options for the select element.
    /// </summary>
    /// <param name="webElement">Select element</param>
    /// <returns><see cref="IList{T}"/><see cref="IWebElement"/></returns>
    public static List<string> GetAllOptionFromSelectDropDown(this IWebElement webElement)
    {
        var selectElement = new SelectElement(webElement);
        return selectElement.Options.Select(x => x.Text).ToList();
    }

    /// <summary>
    /// Get a list of text values of each item in webElement
    /// </summary>
    /// <param name="webElement">Collection item</param>
    /// <returns>List of text values</returns>
    public static List<string> GetTextValuesFromEachCollectionItem(this IList<IWebElement> webElement)
    {
        return webElement.Select(item => item.Text).ToList();
    }

    /// <summary>
    /// Gets the selected value from "except" Tag dropdown
    /// </summary>
    /// <param name="webElement">Select element</param>
    /// <param name="except">not including value.</param>
    /// <returns></returns>
    public static string GetSelectedValueFromSelectDropDown(this IWebElement webElement, string except)
    {
        var selectElement = new SelectElement(webElement);
        string selectedValue = selectElement.AllSelectedOptions.Single(x => x.Text != except).Text;
        return selectedValue;
    }

    /// <summary>
    /// It's used to focus on a certain IWebElement.
    /// </summary>
    /// <param name="webElement">Element that is expected to focus</param>
    public static void SetFocusToWebElement(this IWebElement webElement)
    {
        var xCoordinate = ((ILocatable) webElement).Coordinates.LocationInViewport.X;
        var yCoordinate = ((ILocatable) webElement).Coordinates.LocationInViewport.Y;
        DriverUtilities.ActionBuilder().MoveByOffset(xCoordinate,yCoordinate).MoveToElement(webElement).Build().Perform();
    }
    
    
}