using NoosiumX.Resources.Common.Private;
using NoosiumX.Resources.Log;
using NoosiumX.Resources.Util;
using NoosiumX.Resources.Util.DriverMethods;

namespace NoosiumX.WebDriver.TestCases.Desktop.Auth
{
    
    public class LoginTests
    {
        public static void CheckResponse_WhenValidIdPasswordEntered()
        {
            BasicDriverInterface.NavigateToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
            new TestLog().Debug($"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");

            Elements.GetElementById(ElementNames.Username).Click();
            new TestLog().Debug($"{ElementNames.Username} Clicked.");
            Elements.GetElementById(ElementNames.Username).SendKeys(JsonSoft.GetContext(ElementValues.UserName));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.UserName)} Sent.");
            
            Elements.GetElementById(ElementNames.Password).Click();
            new TestLog().Debug($"{ElementNames.Password} Clicked.");
            Elements.GetElementById(ElementNames.Password).SendKeys(JsonSoft.GetContext(ElementValues.Password));
            new TestLog().Debug($"{JsonSoft.GetContext(ElementValues.Password)} Sent.");
        }
    }
}

