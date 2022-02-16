using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

    /// <summary>
    /// It's used to focus on a certain IWebElement and then Click on it
    /// </summary>
    /// <param name="webElement">Element that is expected to focus and click</param>
    public static void SetFocusToWebElementAndClick(this IWebElement webElement)
    {
        SetFocusToWebElement(webElement);
        webElement.Click();
    }

    /// <summary>
    /// Clicks this element ignoring stale element exception
    /// </summary>
    /// <param name="webElement">Element expected click</param>
    /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
    public static void ClickItem(this IWebElement webElement)
    {
        int tryW=0;
        while (tryW < 3)
        {
            tryW += 1;
            try
            {
                webElement.Click();
                TestContext.WriteLine( webElement + " Clicked.");
                break;
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Gets the innerText of this element, without any leading or trailing whitespace, and with other whitespace collapsed ignoring stale element exception and
    /// Often you get a collection of elements but want to work with a specific element, which means you need to iterate over the collection and identify the one you want.
    /// </summary>
    /// <param name="webElement">Gets the innerText of this element</param>
    /// <exception cref="StaleElementReferenceException">Thrown when the target element is no longer valid in the document DOM.</exception>
    /// <returns>Get return text from webElement</returns>
    public static string GetText(this IWebElement webElement)
    {
        string elementText="innerText";
        int tries = 0;
        while (tries<3)
        {
            tries += 1;
            try
            {
                elementText = webElement.Text;
                break;
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        return elementText;
    }

    /// <summary>
    /// Returns the value of required attribute of the element ignoring stale element and element not visible exception.
    /// </summary>
    /// <param name="webElement">The name of the attribute.</param>
    /// <returns>The attribute's current value. Returns a null if the value is not set.</returns>
    public static string GetAttributeValue(this IWebElement webElement)
    {
        string elementText = "innerText";
        int tries = 0;
        while (tries < 3)
        {
            tries += 1;
            try
            {
                elementText = webElement.GetAttribute("value");
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        return elementText;
    }

    /// <summary>
    /// Simulates typing text into the element ignoring Stale element exception
    /// </summary>
    /// <param name="webElement">Target element</param>
    /// <param name="textToBeEntered">The text to type into the element.</param>
    public static void EnterText(this IWebElement webElement, string textToBeEntered)
    {
        int tries = 0;
        while (tries<3)
        {
            tries += 1;
            try
            {
                webElement.SendKeys(textToBeEntered);
                TestContext.WriteLine( webElement +"SendKeys " + textToBeEntered);
                break;
            }
            catch (StaleElementReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
    /// <summary>
    /// Enter text in phases; selenium frequently fails to enter long text in a text box; this solution fixes that problem.
    /// </summary>
    /// <param name="webElement">Target element</param>
    /// <param name="textToBeEntered">Typing text into the element.</param>
    public static void EnterTextInSteps(this IWebElement webElement, string textToBeEntered)
    {
        try
        {
            foreach (var chara in textToBeEntered)
            {
                webElement.SendKeys(chara.ToString());
                Thread.Sleep(1500);
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    /// <summary>
    /// Executes JavaScript in the context scrolls the element to a particular place of the currently selected frame or window
    /// </summary>
    /// <param name="webDriver">Interface through which the user controls</param>
    /// <param name="webElement">The arguments to the HTML element</param>
    public static void ScrollToElement(this IWebDriver webDriver, IWebElement webElement)
    {
        var xPosition = webElement.Location.X;
        var yPosition = webElement.Location.Y;
        ((IJavaScriptExecutor) webDriver).ExecuteScript($"window.scroll({xPosition}, {yPosition});");
        Thread.Sleep(2000);
    }
    
    /// <summary>
    /// Moves the mouse to the middle of the element.
    /// </summary>
    /// <param name="webElement">The arguments to the HTML element</param>
    public static void ScrollToElement(this IWebElement webElement)
    {
        var actions = new Actions(DriverUtilities.Driver);
        actions.MoveToElement(webElement).Perform();
    }

    /// <summary>
    /// Moves the mouse to the middle of the element and click on it.
    /// </summary>
    /// <param name="webElement">The arguments to the HTML element</param>
    public static void ScrollToElementAndClick(this IWebElement webElement)
    {
        try
        {
            ScrollToElement(webElement);
            webElement.Click();
        }
        catch (Exception e)
        {
            TestContext.WriteLine(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }
}