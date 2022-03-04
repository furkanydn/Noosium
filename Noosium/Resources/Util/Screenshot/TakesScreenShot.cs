using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Noosium.WebDriver.Mock;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Noosium.Resources.Util.Screenshot;

public class TakesScreenShot : BaseMockDriver
{
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
        var takesScreenshot = Driver as ITakesScreenshot ?? throw new InvalidOperationException($"The driver type '{Driver.GetType().FullName}' does not support taking screenshots.");
        var screenshot = takesScreenshot.GetScreenshot();
        var fileP = scPath + testName + "Screenshot_" + DateTime.Now.ToString("s") + ".png";
        //screenshot.SaveAsFile(fileP,ScreenshotImageFormat.Png);
        #endregion

        #region Return Screen Shot Path
        
        var machineName = Environment.MachineName;
        var fileHostName = "\\\\" + machineName + fileP;
        var uri = new Uri(fileHostName);
        string returnPath = null!;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            returnPath = uri.AbsoluteUri.Replace("/","\\");
        Debug.Assert(returnPath != null, nameof(returnPath) + " != null");
        return returnPath;

        #endregion
    }
}