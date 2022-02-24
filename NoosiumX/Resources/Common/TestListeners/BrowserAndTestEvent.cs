using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace NoosiumX.Resources.Common.TestListeners;

public class BrowserAndTestEvent : ITestListener
{
    private By lastFind;
    private OpenQA.Selenium.WebDriver _webDriver;
    public void TestStarted(ITest test)
    {
        throw new System.NotImplementedException();
    }

    public void TestFinished(ITestResult result)
    {
        throw new System.NotImplementedException();
    }

    public void TestOutput(TestOutput output)
    {
        throw new System.NotImplementedException();
    }

    public void SendMessage(TestMessage message)
    {
        throw new System.NotImplementedException();
    }
}