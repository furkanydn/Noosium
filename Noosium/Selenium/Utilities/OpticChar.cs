using System;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;
using IronOcr;
using OpenQA.Selenium;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;
using Point = SixLabors.ImageSharp.Point;
using Rectangle = SixLabors.ImageSharp.Rectangle;
using Size = SixLabors.ImageSharp.Size;

// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Noosium.Selenium.Utilities;

public static class OpticChar
{
    private static readonly string FileNameCaptcha = DateTime.Now.ToString("s") + ".png";
    [SupportedOSPlatform("windows")]
    [UnsupportedOSPlatform("MacCatalyst")]
    public static System.Drawing.Image? ElementScreenShotForWindows(IWebDriver driver, IWebElement webElement)
    {
        Screenshot screenshot = ((ITakesScreenshot) driver).GetScreenshot();
        if (System.Drawing.Image.FromStream(new MemoryStream(screenshot.AsByteArray)) is not Bitmap image) return null;
        image.Save(FileNameCaptcha, System.Drawing.Imaging.ImageFormat.Png);
        return image.Clone(new System.Drawing.Rectangle(webElement.Location, webElement.Size), image.PixelFormat);
    }

    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("macOS")]
    public static Image ElementScreenShotForOtherOs(IWebDriver driver, IWebElement webElement)
    {
        Screenshot screenshot = ((ITakesScreenshot) driver).GetScreenshot();
        using Image imageS = Image.Load(new MemoryStream(screenshot.AsByteArray));
        imageS.SaveAsPng(FileNameCaptcha,new PngEncoder());
        Rectangle rectangle = Rectangle.Empty;
        rectangle.X = webElement.Location.X;
        rectangle.Y = webElement.Location.Y;
        rectangle.Width = webElement.Size.Width;
        rectangle.Height = webElement.Size.Height;
        using Image image = Image.Load(new MemoryStream(screenshot.AsByteArray));
        using Image copy = image.Clone(x => x.Crop(rectangle));
        copy.Save(new MemoryStream(screenshot.AsByteArray),new PngEncoder());
        return null;
    }
    
    public static string PerformOcrForWindows(System.Drawing.Image image)
    {
        var ocr = new IronTesseract();
        using var input = new OcrInput(image);
        input.Deskew();  // image not straight
        input.DeNoise();
        var result = ocr.Read(input);
        return result.Text;
    }

    public static string PerformOcrForOtherOs(SixLabors.ImageSharp.Image image)
    {
        var ocr = new IronTesseract();
        using var input = new OcrInput(image);
        input.Deskew();  // image not straight
        input.DeNoise();
        var result = ocr.Read(input);
        return result.Text;
    }
}