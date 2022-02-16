using Noosium.Common;
using Noosium.PageRepository.Login;
using NUnit.Framework;

namespace Noosium.Tests.Login;

[Author("Admin, Manager, My")]
[Category("Login")]
[Description("A user login to System to access the functionality of the system.")]
[TestFixture, Order(0)]
public class LoginTest
{
    [SetUp]
    public void Setup()
    {
        DriverUtilities.LaunchBrowser();
    }

    [OneTimeTearDown]
    protected void OneTimeTearDown()
    {
        DriverUtilities.TearDown();
    }

    [Description("Check results on entering valid Id & Password")]
    [Test, Order(0)]
    public void CheckAbilityEnteringValidIdPassword()
    {
        LoginPage.EnterValidIdPassword();
    }
}