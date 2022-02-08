using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;
using Noosium.Utilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace Noosium.Common;

public class DriverUtilities
{
    //public static string Browser = ConfigurationManager.AppSettings["browser"]?.ToLower() ?? string.Empty;
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

    #endregion
}