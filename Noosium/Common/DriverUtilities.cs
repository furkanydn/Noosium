using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Noosium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace Noosium.Common;

public class DriverUtilities
{
    public static string Browser = Crid.GetAppSettings("Environment");

    private static bool _acceptNextAlert = true;
    public static IWebDriver Driver;

    /// <summary>
    /// Returns a DateTime representing the current date and time. The resolution of the returned value depends on the system timer.
    /// <example>1999-10-31T02:00:00</example>
    /// <returns>"yyyy-MM-dd'T'HH:mm:ss"</returns>
    /// </summary>
    public static string DateTimeStamp => DateTime.Now.ToString("s");

    #region Driver Setup and Teardown Methods

    /// <summary>
    /// This method is used to launch the browser (driver) based on the browser configured in AppSettings
    /// </summary>
    public static void LaunchBrowser()
    {
        switch (Browser.ToLower())
        {
            case "chrome":
                Driver = new ChromeDriver();
                TestContext.WriteLine("The driver will start Chrome without any input from the user.");
                break;
            case "edge":
                Driver = new EdgeDriver();
                TestContext.WriteLine("The driver will start Edge without any input from the user.");
                break;
            case "firefox":
                Driver = new FirefoxDriver();
                TestContext.WriteLine("The driver will start Firefox without any input from the user.");
                break;
            case "opera":
                Driver = new OperaDriver();
                TestContext.WriteLine("The driver will start Opera without any input from the user.");
                break;
            case "safari":
                Driver = new SafariDriver();
                TestContext.WriteLine("The driver will start Safari without any input from the user.");
                break;
            /*
             For use PhantomJS browser install Selenium.WebDriver.PhantomJS.Xplatform
             case "phantom":
                PhantomJSOptions PJS = new PhantomJSOptions();
                Driver = new PhantomJSDriver();
                */
        }
        Driver.Manage().Window.Maximize();
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
    }
    
    /// <summary>
    /// Returns Assembly directory path
    /// </summary>
    /// <exception cref="InvalidOperationException">The exception that is thrown when a method call is invalid for the object's current state.</exception>
    private static string AssemblyDirectory
    {
        get
        {
            var @base = Assembly.GetExecutingAssembly().Location;
            var uriBuilder = new UriBuilder(@base ?? throw new InvalidOperationException());
            var path = Uri.UnescapeDataString(uriBuilder.Path);
            return Path.GetDirectoryName(path) ?? throw new InvalidOperationException();
        }
    }
    
    /// <summary>
    /// This method is used to close the browser(driver)
    /// </summary>
    public static void TearDown()
    {
        try
        {
            Driver.Quit();
            foreach (var process in Process.GetProcessesByName("chrome"))
            {
                process.Kill();
                Thread.Sleep(3000);
            }
        }
        catch (Exception e)
        {
            TestContext.WriteLine(e);
            Assert.Fail("Could not kill all process");
        }
    }

    #region JavaScriptFunctions

    /// <summary>
    /// Indicates that a driver can execute JavaScript, providing access to the mechanism to do so. Because of cross domain policies browsers enforce your script execution may fail unexpectedly and without adequate error messaging. This is particularly pertinent when creating your own XHR request or when trying to access another frame. Most times when troubleshooting failure it's best to view the browser's console after executing the WebDriver request.
    /// </summary>
    /// <returns>One of Boolean, Long, Double, String, List, Map or WebElement. Or null.</returns>
    /// <exception cref="NotSupportedException">The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality.</exception>
    private static IJavaScriptExecutor JavaScriptExecutor()
    {
        var javaScriptExecutor = Driver as IJavaScriptExecutor ?? throw new NotSupportedException("Underlying driver instance does not support executing JavaScript");
        return javaScriptExecutor;
    }

    /// <summary>
    /// Bind an event handler to the "click" JavaScript event, or trigger that event on an element.
    /// </summary>
    /// <param name="value">An value containing data that will be passed to the event handler.</param>
    public static void JavaScriptClick(string value)
    {
        JavaScriptExecutor().ExecuteScript("$('" + value + "').trigger('click')");
    }

    /// <summary>
    /// Window.scrollTo() scrolls to a particular set of coordinates in the document.
    /// document.body.scrollHeight till end of the page
    /// </summary>
    public static void JavaScriptTillEnd()
    {
        JavaScriptExecutor().ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");
    }
    
    /// <summary>
    /// Window.scrollTo() scrolls to a particular set of coordinates in the document.
    /// Minus(-) document.body.scrollHeight to top of the page
    /// </summary>
    public static void JavaScriptToTop()
    {
        JavaScriptExecutor().ExecuteScript("window.scrollTo(0,-document.body.scrollHeight)");
    }

    /// <summary>
    /// The JavaScriptTillPoint method scrolls the window to a particular place in the page.
    /// </summary>
    /// <param name="xAxis">is the pixel along the horizontal axis of the document that you want displayed in the upper left.</param>
    /// <param name="yAxis">is the pixel along the vertical axis of the document that you want displayed in the upper left.</param>
    public static void JavaScriptTillPoint(string xAxis, string yAxis)
    {
        JavaScriptExecutor().ExecuteScript("scroll(" + xAxis + "," + yAxis + ");");
    }
    #endregion

    /// <summary>
    /// Is the method used to verify a presence of a web element within the webpage. The method returns a “true” value if the specified web element is present on the web page and a “false” value if the web element is not present on the web page.
    /// </summary>
    /// <param name="webElement">Represents an HTML element. Generally, all interesting operations to do with interacting with a page will be performed through this interface.</param>
    /// <returns>Return true if the selected DOM-element is displayed.</returns>
    public static bool IsElementPresent(IWebElement webElement)
    {
        try
        {
            //todo SetFocusOnIWebElement(element);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    /// <summary>
    /// Switches to the currently active modal dialog for this particular driver instance.
    /// </summary>
    /// <returns>A handle to the dialog.</returns>
    public static bool IsAlertPresent()
    {
        try
        {
            Driver.SwitchTo().Alert();
            return true;
        }
        catch (NoAlertPresentException)
        {
            return false;
        }
    }

    /// <summary>
    /// This method is used to turn off the User's JavaScript alerts Warning/popup and get messages from the popup.
    /// </summary>
    /// <returns>Gets the text of the alert.</returns>
    public static string CloseAlertAndGetItsText()
    {
        try
        {
            var alert = Driver.SwitchTo().Alert();
            var alertText = alert.Text;
            if (_acceptNextAlert)
                alert.Accept();
            else
                alert.Dismiss();

            return alertText;
        }
        finally
        {
            _acceptNextAlert = true;
        }
    }

    /// <summary>
    /// Accepts the alert.
    /// </summary>
    public static void AcceptAlertAndCloseAlert()
    {
        try
        {
            var alert = Driver.SwitchTo().Alert();
            if (_acceptNextAlert)
                alert.Accept();
            else
                alert.Dismiss();
        }
        finally
        {
            _acceptNextAlert = true;
        }
    }

    /// <summary>
    /// Handle the Exception instance that caused the current exception.
    /// </summary>
    /// <param name="exception">An object that describes the error that caused the current exception. The InnerException property returns the same value as was passed into the Exception(String, Exception) constructor, or null if the inner exception value was not supplied to the constructor.</param>
    /// <param name="screenshotPath">Screenshot store it in the specified location.</param>
    public static void HandleNotNullInnerException(Exception exception, string screenshotPath)
    {
        string innerException = Crid.GetExceptionMessage("InnerException");
        string exceptionMessage = Crid.GetExceptionMessage("ExceptionInnerMessage");
        if (exception.InnerException != null && (exception.InnerException.ToString().Contains(innerException) || exception.Message.Contains(exceptionMessage)))
        {
            Driver.Quit();
            Thread.Sleep(3000);
            LaunchBrowser();
        }
        else
        {
            //todo TakesScreenshot(screenshotPath)
            TestContext.WriteLine("Another Exception");
        }
    }

    /// <summary>
    /// The InnerException property, which holds a reference to the inner exception, is set upon initialization of the exception object.
    /// </summary>
    /// <param name="exception">An object that describes the error that caused the current exception. The InnerException property returns the same value as was passed into the Exception(String, Exception) constructor, or null if the inner exception value was not supplied to the constructor.</param>
    /// <param name="screenshotPath">Screenshot store it in the specified location.</param>
    public static void HandleNullInnerException(Exception exception, string screenshotPath)
    {
        string expectedMessage = Crid.GetExceptionMessage("ExpectedExceptionMessage");
        if (exception.Message.Contains(expectedMessage))
        {
            Driver.Quit();
            Thread.Sleep(3000);
            LaunchBrowser();
        }
        else
        {
            //todo TakesScreenshot(screenshotPath)
            TestContext.WriteLine("Another Exception");
        }
    }
    
    /// <summary>
    /// This function is used to handle exceptions during test execution, and it's very handy for restarting the driver if it's stopped or not responding due to any unanticipated issues.
    /// </summary>
    /// <param name="exception">An object that describes the error that caused the current exception.</param>
    /// <param name="screenshotpath">Screenshot store it in the specified location.</param>
    public static void ManageFailure(Exception exception, string screenshotpath=@"Screenshot")
    {
        TestContext.WriteLine(exception.HResult);
        TestContext.WriteLine("The Message is :" + exception.Message);
        TestContext.WriteLine("The Source is :" + exception.Source);
        TestContext.WriteLine("The InnerException is :" + exception.InnerException);
        TestContext.WriteLine("The StackTrace is :" + exception.StackTrace);
        TestContext.WriteLine("TargetSite is :" + exception.TargetSite);
        TestContext.WriteLine("Data is :" + exception.Data);
        if (exception.InnerException == null)
            HandleNullInnerException(exception, screenshotpath);
        else
            HandleNotNullInnerException(exception, screenshotpath);
    }

    #endregion

    #region Basic Driver Interface Methods

    

    #endregion
}