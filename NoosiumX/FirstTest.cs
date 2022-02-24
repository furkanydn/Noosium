using NoosiumX.Resources.Common.Private;
using NoosiumX.Resources.Log;
using NoosiumX.Resources.Util;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace NoosiumX;

[TestFixture]
public class FirstTest
{
    [Test]
    public void CorrectTitleDisplayed_When_NavigateToHomePage()
    {
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
        using var driver = new ChromeDriver();
        driver.Navigate().GoToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
        new TestLog().Information(
            $"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");
        Assert.AreEqual(driver.Title,"Noos Identity");
    }
}