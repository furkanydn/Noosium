using Noosium.WebDriver.Mock;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Noosium.Resources.Util.ActionBuilder;

public class ActionBuilders : BaseMockDriver
{
    public static ActionBuilders GetCreateInstance()
    {
        return new ActionBuilders();
    }

    /// <summary>
    /// The user-facing API for emulating complex user gestures. Use this class rather than using the Keyboard or Mouse directly.
    /// </summary>
    /// <returns>A self reference.</returns>
    private static Actions ActionBuilder()
    {
        var actionBuilder = new Actions(Driver);
        return actionBuilder;
    }

    /// <summary>
    /// Moves the mouse to the middle of the element. The element is scrolled into view and its location is calculated using getClientRects.
    /// It's a quick and easy way to do compound actions.
    /// </summary>
    /// <param name="webElement">Element to move to..</param>
    public static void SetFocusOnIWebElement(IWebElement webElement)
    {
        ActionBuilder().MoveToElement(webElement).Build().Perform();
    }

    /// <summary>
    /// Moves the mouse to an offset from the element's in-view center point.
    /// </summary>
    /// <param name="webElement">Element to move to.</param>
    /// <returns>A self reference.</returns>
    public static void SetFocusAndClickOnIWebElement(IWebElement webElement)
    {
        ActionBuilder().MoveToElement(webElement,0,0).Click().Build().Perform();
    }

    /// <summary>
    /// Moves the mouse from its current position (or 0,0) by the given offset. If the coordinates provided are outside the viewport (the mouse will end up outside the browser window) then the viewport is scrolled to match.
    /// </summary>
    /// <param name="xCoordinate">Horizontal offset. A negative value means moving the mouse left.</param>
    /// <param name="yCoordinate">Vertical offset. A negative value means moving the mouse up.</param>
    /// <returns>A self reference</returns>
    /// <exception cref="MoveTargetOutOfBoundsException">If the provided offset is outside the document's boundaries.</exception>
    public static void MoveCursor(int xCoordinate, int yCoordinate)
    {
        ActionBuilder().MoveByOffset(xCoordinate,yCoordinate).Perform();
    }
}