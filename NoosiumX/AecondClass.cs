using NoosiumX.WebDriver.Mock;
using NoosiumX.WebDriver.TestCases.Desktop.Auth;
using NUnit.Framework;

namespace NoosiumX;

[TestFixture, 
     Description("A user login to System to access the functionality of the system."),
     Author("Admin, Manager, My"),
     Platform(Exclude = "Win98,WinXP,Vista")]
[NonParallelizable, Order(2)]
public class AecondClass : BaseMockDriver
{
     [Test, Order(1)]
     public void CorrectTitleDisplayed_When_NavigateToHomePage()
     {
          /*BasicDriverInterface.NavigateToUrl(JsonSoft.GetUri(ElementSetting.BaseUrl));
          new TestLog().Information(
              $"{JsonSoft.GetUri(ElementSetting.BaseUrl)} opening in other tab /window as per requirement.");*/
          LoginTests.CheckResponse_WhenValidIdPasswordEntered();
     }    
}