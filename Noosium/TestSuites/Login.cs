namespace Noosium.TestSuites
{
    using Resources.Log;
    using WebDriver.Mock;
    using WebDriver.TestCases.Desktop.Auth;
    using NUnit.Framework;
    
    [TestFixture,
     Order(0),
     Description("A user login to System to access the functionality of the system."),
     Author("Admin, Manager, My"),
     Platform(Exclude = "Win98,WinXP,Vista"),
     NonParallelizable]
    public class Login
    {
        [OneTimeSetUp]
        public void TestSetUp()
        {
            new TestLog().Debug("The tests were started by the driver.");
            BaseMockDriver.DriverCreateByBrowser();
        }
        
        [Test, Order(3)]
        public void CorrectTitleDisplayed_When_NavigateToHomePage()
        {
            LoginTests.CheckResponse_ShouldNavigateToMissionPage_WhenValidIdPasswordEntered();
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
