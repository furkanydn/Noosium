using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace NoosiumX.Resources.Common.Template.Core;

public class NoosiumWebDriver : EventFiringWebDriver
{
    private OpenQA.Selenium.WebDriver _webDriver;
    private bool _isMobile;
    
    public NoosiumWebDriver(OpenQA.Selenium.WebDriver parentDriver, bool isMobile) : base(parentDriver)
    {
        _isMobile = isMobile;
        _webDriver = parentDriver;
    }
    
    //public NoosiumWebDriver(OpenQA.Selenium.WebDriver parentDriver, NetworkTrafficInterceptor)
}