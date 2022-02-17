using Noosium.Common;
using Noosium.PageRepository.Login;
using NUnit.Framework;

namespace Noosium.Tests.Login;

[Author("Admin, Manager, My")]
[Description("A user login to System to access the functionality of the system.")]
[TestFixture, Order(0)]
[Platform(Exclude="Win98,WinXP,Vista")]
public class LoginTest
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        DriverUtilities.LaunchBrowser();
    }

    [TearDown]
    public void TearDown()
    {
        DriverUtilities.TearDown();
    }

    [Description("Check results on entering Invalid User ID & Password")]
    [Test, Order(0)]
    public void CheckAbilityEnteringInvalidIdPassword()
    {
        LoginPage.EnterInValidIdPassword();
    }
    
    [Description("Check results on entering Invalid Captcha")]
    [Test, Order(1)]
    public void CheckAbilityEnteringInvalidCaptcha()
    {
        LoginPage.EnterInValidCaptcha();
    }
    
    [Description("Check results on entering valid Username & Password")]
    [Test, Order(2), Property("Severity","Major")]
    public void CheckAbilityEnteringValidIdPassword()
    {
        LoginPage.EnterValidIdPassword();
    }
}