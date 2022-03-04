using System;
using Noosium.Resources.Util.Screenshot;
using Noosium.WebDriver.Mock;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Noosium.Resources.Util.DriverMethods;

public class BasicMethods : BaseMockDriver
{
    private static bool _acceptNextAlert = true;
    
    
    /// <summary>
    /// Is the method used to verify a presence of a web element within the webpage. The method returns a “true” value if the specified web element is present on the web page and a “false” value if the web element is not present on the web page.
    /// </summary>
    /// <param name="webElement">Represents an HTML element. Generally, all interesting operations to do with interacting with a page will be performed through this interface.</param>
    /// <returns>Return true if the selected DOM-element is displayed.</returns>
    public static bool IsElementPresent(IWebElement webElement)
    {
        try
        {
            ActionBuilder.ActionBuilders.SetFocusOnIWebElement(webElement);
            return true;
        }
        catch
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
        TakesScreenShot.TakesScreenshot(screenshotpath);
    }
}