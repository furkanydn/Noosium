using NoosiumX.Resources.Common.Private;
using NoosiumX.Resources.Log;
using NoosiumX.Resources.Util;
using NoosiumX.Resources.Util.DriverMethods;
using NoosiumX.WebDriver.Mock;
using NUnit.Framework;

namespace NoosiumX;

[TestFixture, 
 Description("A user login to System to access the functionality of the system."),
 Author("Admin, Manager, My"),
 Platform(Exclude = "Win98,WinXP,Vista")]
public class FirstTest : BaseMockDriver
{
    [Test, Order(1)]
    public void CorrectTitleDisplayed_When_NavigateToHomePage()
    {
        BasicDriverInterface.NavigateToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
        new TestLog().Information(
            $"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");
        
    }
}