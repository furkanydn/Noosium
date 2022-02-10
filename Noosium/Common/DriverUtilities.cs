using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Noosium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;

namespace Noosium.Common;

public class DriverUtilities
{
    public static readonly string Browser = Crid.GetAppSettings("Environment");
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
            SetFocusOnIWebElement(webElement);
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
            TakesScreenshot(screenshotPath);
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
            TakesScreenshot(screenshotPath);
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
        string txt = Driver.FindElement(locator).Text;
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
        string txt = Driver.FindElement(locator).GetAttribute(attrName);
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
    
    #endregion

    #region Action Builder Methods

    /// <summary>
    /// The user-facing API for emulating complex user gestures. Use this class rather than using the Keyboard or Mouse directly.
    /// </summary>
    /// <returns>A self reference.</returns>
    public static Actions ActionBuilder()
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

    #endregion

    #region Wait And Timeout Methods

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

    #endregion

    #region ScreenShot Methods
    
    public static string TakesScreenshot(string screenshotLocation)
    {
        //Directory path for saving screenshots
        //dirPath = @"..\..\..\" + scFolderName;
        string scFolderName = "Screenshot " + DateTime.Now.ToString("s");
        string dirPath = screenshotLocation + scFolderName;
        if (Directory.Exists(dirPath)==false)
        {
            var directoryInfo = Directory.CreateDirectory(dirPath);
        }

        // Screenshot path for returning the complete screenshot URL with its name
        string scPath = Path.Combine(Directory.GetCurrentDirectory(), dirPath + @"\");
        string testName = TestContext.CurrentContext.Test.Name.Replace('"', '\'').Replace(";", "-").Replace("/", "_");
        int testNameLenght = TestContext.CurrentContext.Test.Name.Replace('"', '\'').Replace(";", "-").Replace("/", "_")
            .Length;
        testName = testNameLenght switch
        {
            > 50 => testName[..50] + "...",
            _ => testName
        };
        
        //Take screenshot and save it specified location

        #region Take Screenshot and Save
        //The image of the page as a Base64-encoded string.
        ITakesScreenshot takesScreenshot = Driver as ITakesScreenshot ?? throw new InvalidOperationException($"The driver type '{Driver.GetType().FullName}' does not support taking screenshots.");
        Screenshot screenshot = takesScreenshot.GetScreenshot();
        string fileP = scPath + testName + "Screenshot_" + DateTime.Now.ToString("s") + ".png";
        //screenshot.SaveAsFile(fileP,ScreenshotImageFormat.Png);
        #endregion

        #region Return Screen Shot Path
        
        string machineName = Environment.MachineName;
        string fileHostName = "\\\\" + machineName + fileP;
        var uri = new Uri(fileHostName);
        string returnPath = null!;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            returnPath = uri.AbsoluteUri.Replace("/","\\");
        Debug.Assert(returnPath != null, nameof(returnPath) + " != null");
        return returnPath;

        #endregion
    }

    #endregion

    #region Collection Related Methods

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
        ActionBuilder().ClickAndHold(source).MoveToElement(destination).Release(destination).Build().Perform();
    }

    /// <summary>
    /// Select item currently value in the List Dropdown
    /// </summary>
    /// <param name="webElements">The first matching IWebElement on the current context.</param>
    /// <param name="itemName">Element matches the criteria.</param>
    public static void SelectFromListDropDownAndClick(IList<IWebElement> webElements, string itemName)
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
    #endregion
}