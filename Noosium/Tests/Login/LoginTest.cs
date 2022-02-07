using System;
using System.Collections.Generic;
using System.Diagnostics;
using Noosium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Noosium.Tests.Login;

[Author("Admin, Manager, My")]
[Category("Login")]
[Description("A user login to System to access the functionality of the system.")]
[TestFixture, Order(0)]
public class LoginTest
{
    private IWebDriver _driver;
    private IDictionary<string, object> Vars {get; set;}
    private IJavaScriptExecutor _javaScriptExecutor;

    [SetUp]
    public void Setup()
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("start-maximized");
        chromeOptions.AddArgument("--enable-precise-memory-info");
        chromeOptions.AddArgument("--disable-popup-blocking");
        chromeOptions.AddArgument("--disable-default-apps");
        chromeOptions.AddArgument("test-type=browser");
        _driver = new ChromeDriver(chromeOptions);
        _javaScriptExecutor = (IJavaScriptExecutor) _driver;
        Vars = new Dictionary<string, object>();
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
        _driver.Navigate().GoToUrl(Crid.GetUriCrid("BaseUrl"));
        // 2 | SetWindowSize | Maximize
        _driver.Manage().Window.Maximize();
        // 3 | Click | id=username
        _driver.FindElement(By.Id("username")).Click();
        // 4 | Type | id=username
        _driver.FindElement(By.Id("username")).SendKeys(Crid.GetContextCrid("username"));
        // 5 | Click | id=password 
        _driver.FindElement(By.Id("password")).Click();
        // 6 | Type | id=password
        _driver.FindElement(By.Id("password")).SendKeys(Crid.GetContextCrid("password"));
        // 7 | Click | id=captchaCode 
        _driver.FindElement(By.Id("captchaCode")).Click();
        // 8 | Click | id=captchaCode
        _driver.FindElement(By.Id("captchaCode")).SendKeys(Crid.GetContextCrid("captcha"));
        // 9 | Click | name=button
        _driver.FindElement(By.Name("button")).Click();
        _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
    }
}