using Noosium.Selenium.Utilities;

namespace Noosium.Selenium.Login;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

[Author("Admin, Manager, My")]
[Category("Login")]
[Description("A user login to System to access the functionality of the system.")]
[TestFixture, Order(0)]
public class LoginTest
{
    private IWebDriver _driver;

    public LoginTest(IWebDriver driver, IJavaScriptExecutor javaScriptExecutor, IDictionary<string, object> vars)
    {
        _driver = driver;
        _javaScriptExecutor = javaScriptExecutor;
        this.Vars = vars;
    }
    private IDictionary<string, object> Vars { get; set; }
    private IJavaScriptExecutor _javaScriptExecutor;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
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
    public void CheckAbilityEnteringInvalidIdPassword()
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
        // 8 | ImplicitWait | for=captchaCode 
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        // 9 | Click | name=button | 
        _driver.FindElement(By.Name("button")).Click();
    }
}