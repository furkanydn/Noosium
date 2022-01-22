using System;
using System.Collections.Generic;
using System.Drawing;
using Noosium.Selenium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Noosium.Selenium.Tests.Login;

[Author("Admin, Manager, My")]
[Category("Login")]
[Description("A user login to System to access the functionality of the system.")]
[TestFixture, Order(0)]
public class LoginTest
{
    private IWebDriver _driver;
    private static string _chromeDriverPath =AppContext.BaseDirectory+ @"Selenium/Libs";
    private IDictionary<string, object> vars {get; set;}
    private IJavaScriptExecutor javaScriptExecutor;
    
    [SetUp]
    public void Setup()
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("start-maximized");
        chromeOptions.AddArgument("--enable-precise-memory-info");
        chromeOptions.AddArgument("--disable-popup-blocking");
        chromeOptions.AddArgument("--disable-default-apps");
        chromeOptions.AddArgument("test-type=browser");
        _driver = new ChromeDriver(_chromeDriverPath,chromeOptions);
        javaScriptExecutor = (IJavaScriptExecutor) _driver;
        vars = new Dictionary<string, object>();
    }

    [OneTimeTearDown]
    protected void OneTimeTearDown()
    {
        _driver.Quit();
    }

    [Description("Check results on entering valid Id & Password")]
    [Test, Order(0)]
    public void CheckAbilityEnteringValidIdPassword()
    {
        // 1 | Open | /Account/Login
        _driver.Navigate().GoToUrl(Crid.ReadCrid("BaseUrl"));
        // 2 | SetWindowSize | Maximize | 
        _driver.Manage().Window.Maximize();
        // 3 | Click | id=username | 
        _driver.FindElement(By.Id("username")).Click();
        // 4 | Type | id=username | furkan.aydin
        _driver.FindElement(By.Id("username")).SendKeys("furkan.aydin");
        // 5 | Click | id=password | 
        _driver.FindElement(By.Id("password")).Click();
        // 6 | Type | id=password | Hadate25.
        _driver.FindElement(By.Id("password")).SendKeys("Hadate25.");
        // 7 | Click | id=captchaCode | 
        _driver.FindElement(By.Id("captchaCode")).Click();
        // 8 | Click | id=noosCaptchaImage
        Image captchaOcrImage = OpticChar.ElementScreenShot(_driver, _driver.FindElement(By.Id("noosCaptchaImage")));
        string ocrText = OpticChar.PerformOcr(captchaOcrImage);
        // 9 | Type | id=captchaCode
        _driver.FindElement(By.Id("captchaCode")).SendKeys(ocrText);
        // 10 | Click | name=button |
        _driver.FindElement(By.Name("button")).Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(300);
    }
}