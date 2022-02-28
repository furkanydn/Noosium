namespace NoosiumX.TestSuites.Auth
{
    using NoosiumX.WebDriver.TestCases.Desktop.Auth;
    using NUnit.Framework;
    using Resources.Util;
    using Resources.Util.DriverMethods;
    using Resources.Log;
    using WebDriver.Mock;
    
    [TestFixture,
     Order(0),
     Description("A user login to System to access the functionality of the system."),
     Author("Admin, Manager, My"),
     Platform(Exclude = "Win98,WinXP,Vista"),
     NonParallelizable]
    public class Login : BaseMockDriver
    {
        [Test, Order(3)]
        public void CorrectTitleDisplayed_When_NavigateToHomePage()
        {
            if (!BasicDriverInterface.IsSessionActive())
                LoginTests.CheckResponse_ShouldNavigateToMissionPage_WhenValidIdPasswordEntered();
            else
            {
                Assert.Ignore();
                new TestLog().Warning(JsonSoft.GetException("AlreadyLoggedIn"));
            }
        }

        [Test, Order(2)]
        public void CorrectAlertDisplayed_When_InvalidIdPassword()
        {
            LoginTests.CheckResponse_ShouldDisplayAlertComponent_WhenInvalidIdPasswordEntered_();
        }

        [Test, Order(1)]
        public void CorrectFormMessageDisplayed_When_InvalidCaptcha()
        {
            LoginTests.CheckResponse_ShouldDisplayFormMessageError_WhenInvalidCaptcha();
        }
    }
}
