namespace Noosium.WebDriver.TestCases.Desktop.Auth
{
    using NUnit.Framework;
    using Resources.Common.Private;
    using Resources.Log;
    using Resources.Util;
    using System.Threading;
    using OpenQA.Selenium;
    using static Resources.Util.DriverMethods.BasicDriverInterface;

    public static class LoginTests
    {
        public static void CheckResponse_ShouldNavigateToMissionPage_WhenValidIdPasswordEntered()
        {
            NavigateToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
            new TestLog().Information($"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");
            
            ClickOnElement(By.Id(ElementNames.Username));
            new TestLog().Information($"{ElementNames.Username} Clicked.");
            SendKeys(By.Id(ElementNames.Username),JsonSoft.GetContext(ElementValues.UserName));
            new TestLog().Information($"{JsonSoft.GetContext(ElementValues.UserName)} Sent.");
            
            ClickOnElement(By.Id(ElementNames.Password));
            new TestLog().Information($"{ElementNames.Password} Clicked.");
            SendKeys(By.Id(ElementNames.Password), JsonSoft.GetContext(ElementValues.Password));
            new TestLog().Information($"{JsonSoft.GetContext(ElementValues.Password)} Sent.");
            
            ClickOnElement(By.Id(ElementNames.CaptchaCode));
            new TestLog().Information($"{ElementNames.CaptchaCode} Clicked.");
            SendKeys(By.Id(ElementNames.CaptchaCode), JsonSoft.GetContext(ElementValues.Captcha));
            new TestLog().Information($"{JsonSoft.GetContext(ElementValues.Captcha)} Sent.");
            
            ClickOnElement(By.Name(ElementNames.Button));
            new TestLog().Information($"{ElementNames.Button} Clicked.");
            
            Thread.Sleep(2000);
            Assert.That(GetDriverUrl(),Is.EqualTo(JsonSoft.GetUri("BaseUrl")+JsonSoft.GetUri("AdminMissionCom")));
        }

        public static void CheckResponse_ShouldDisplayAlertComponent_WhenInvalidIdPasswordEntered_()
        {
            NavigateToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
            new TestLog().Debug($"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");
            
            ClickOnElement(By.Id(ElementNames.Username));
            new TestLog().Debug($"{ElementNames.Username} Clicked.");
            SendKeys(By.Id(ElementNames.Username),JsonSoft.GetContext(ElementValues.InvalidUserName));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.InvalidUserName)} Sent.");
            
            ClickOnElement(By.Id(ElementNames.Password));
            new TestLog().Debug($"{ElementNames.Password} Clicked.");
            SendKeys(By.Id(ElementNames.Password), JsonSoft.GetContext(ElementValues.InvalidPassword));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.InvalidPassword)} Sent.");
            
            ClickOnElement(By.Id(ElementNames.CaptchaCode));
            new TestLog().Debug($"{ElementNames.CaptchaCode} Clicked.");
            SendKeys(By.Id(ElementNames.CaptchaCode), JsonSoft.GetContext(ElementValues.Captcha));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.Captcha)} Sent.");
            
            ClickOnElement(By.Name(ElementNames.Button));
            new TestLog().Debug($"{ElementNames.Button} Clicked.");
            
            Assert.That(GetText(By.CssSelector(ElementNames.AlertStrong)),Is.EqualTo(ElementErrorMessages.AccountDoesNotExist));
        }

        public static void CheckResponse_ShouldDisplayFormMessageError_WhenInvalidCaptcha()
        {
            NavigateToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
            new TestLog().Debug($"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");
            
            ClickOnElement(By.Id(ElementNames.Username));
            new TestLog().Debug($"{ElementNames.Username} Clicked.");
            SendKeys(By.Id(ElementNames.Username),JsonSoft.GetContext(ElementValues.UserName));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.UserName)} Sent.");
            
            ClickOnElement(By.Id(ElementNames.Password));
            new TestLog().Debug($"{ElementNames.Password} Clicked.");
            SendKeys(By.Id(ElementNames.Password), JsonSoft.GetContext(ElementValues.Password));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.Password)} Sent.");
            
            ClickOnElement(By.Id(ElementNames.CaptchaCode));
            new TestLog().Debug($"{ElementNames.CaptchaCode} Clicked.");
            SendKeys(By.Id(ElementNames.CaptchaCode), JsonSoft.GetContext(ElementValues.InvalidCaptcha));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.InvalidCaptcha)} Sent.");
            
            ClickOnElement(By.Name(ElementNames.Button));
            new TestLog().Debug($"{ElementNames.Button} Clicked.");
            
            Assert.That(GetText(By.Id(ElementNames.FormMessages)),Is.EqualTo(ElementErrorMessages.FormMessagesText));
        }
    }
}

