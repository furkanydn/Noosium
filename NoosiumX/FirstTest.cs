using NoosiumX.Resources.NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace NoosiumX;

public class FirstTest
{
    private static readonly LogNLog NLog = new();
    private IWebDriver _driver;

    public FirstTest()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
    }
    
    [Fact]
    public void CorrectTitleDisplayed_When_NavigateToHomePage()
    {
        _driver.Navigate().GoToUrl("a");
        NLog.Error("message-template");
    }
}