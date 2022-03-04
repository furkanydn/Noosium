using System;
using Noosium.WebDriver.Mock;
using OpenQA.Selenium;

namespace Noosium.Resources.Util.JSFunctions;

public class JavaScriptFunctions : BaseMockDriver
{
    /// <summary>
    /// Indicates that a driver can execute JavaScript, providing access to the mechanism to do so. Because of cross domain policies browsers enforce your script execution may fail unexpectedly and without adequate error messaging. This is particularly pertinent when creating your own XHR request or when trying to access another frame. Most times when troubleshooting failure it's best to view the browser's console after executing the WebDriver request.
    /// </summary>
    /// <returns>One of Boolean, Long, Double, String, List, Map or WebElement. Or null.</returns>
    /// <exception cref="NotSupportedException">The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
    private static IJavaScriptExecutor GetJavaScriptExecutor()
    {
        var javaScriptExecutor = Driver as IJavaScriptExecutor ??
                    throw new NotSupportedException("Underlying driver instance does not support executing JavaScript");
        return javaScriptExecutor;
    }

    /// <summary>
    /// Bind an event handler to the "click" JavaScript event, or trigger that event on an element.
    /// </summary>
    /// <param name="value">An value containing data that will be passed to the event handler.</param>
    public static void JavaScriptClick(string value)
    {
        GetJavaScriptExecutor().ExecuteScript("$('" + value + "').trigger('click')");
    }

    /// <summary>
    /// Window.scrollTo() scrolls to a particular set of coordinates in the document.
    /// document.body.scrollHeight till end of the page
    /// </summary>
    public static void JavaScriptTillEnd()
    {
        GetJavaScriptExecutor().ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
    }
    
    /// <summary>
    /// Window.scrollTo() scrolls to a particular set of coordinates in the document.
    /// Minus(-) document.body.scrollHeight to top of the page
    /// </summary>
    public static void JavaScriptToTop()
    {
        GetJavaScriptExecutor().ExecuteScript("window.scrollTo(0,-document.body.scrollHeight)");
    }

    /// <summary>
    /// The JavaScriptTillPoint method scrolls the window to a particular place in the page.
    /// </summary>
    /// <param name="xAxis">is the pixel along the horizontal axis of the document that you want displayed in the upper left.</param>
    /// <param name="yAxis">is the pixel along the vertical axis of the document that you want displayed in the upper left.</param>
    public static void JavaScriptTillPoint(string xAxis, string yAxis)
    {
        GetJavaScriptExecutor().ExecuteScript("scroll(" + xAxis + "," + yAxis + ");");
    }
}