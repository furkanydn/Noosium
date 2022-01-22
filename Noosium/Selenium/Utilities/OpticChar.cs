using System;
using System.Drawing;
using System.IO;
using IronOcr;
using OpenQA.Selenium;

namespace Noosium.Selenium.Utilities;

public class OpticChar
{
    public static Bitmap ElementScreenShot(IWebDriver driver, IWebElement webElement)
    {
        string fileNameCaptcha = DateTime.Now.ToString("s") + ".png";
        Screenshot screenshot = ((ITakesScreenshot) driver).GetScreenshot();
        if (OperatingSystem.IsWindows())
        {
            var image = Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as Bitmap;
            image.Save(fileNameCaptcha, System.Drawing.Imaging.ImageFormat.Png);
            return image.Clone(new Rectangle(webElement.Location, webElement.Size), image.PixelFormat);
        }

        return null;
    }

    public static string PerformOcr(Image image)
    {
        var ocr = new IronTesseract();
        using var input = new OcrInput(image);
        input.Deskew();  // image not straight
        var result = ocr.Read(input);
        return result.Text;
    }
}