using Noosium.PageRepository.Login;
using NUnit.Framework;

namespace Noosium.Tests.Cases
{
    [TestFixture, 
     Description("A user login to System to access the functionality of the system."),
     Author("Admin, Manager, My"),
     Platform(Exclude = "Win98,WinXP,Vista")]
    public class AccountLogin : TestRootSuite
    {
        [Test, 
         Category("Regression Testing"), 
         Description("Check results on entering Invalid User ID & Password"),
         Order(1)]
        public static void CheckAbilityEnteringInvalidIdPassword()
        {
            LoginPage.EnterInValidIdPassword();
        }

        [Test,
         Category("Regression Testing"), 
         Description("Check results on entering Invalid Captcha"), 
         Order(1)]
        public static void CheckAbilityEnteringInvalidCaptcha()
        {
            LoginPage.EnterInValidCaptcha();
        }

        [Test, 
         Category("Sanity Testing"), 
         Description("Check results on entering valid Username & Password"),
         Property("Severity", "Major"), 
         Order(2)]
        public static void CheckAbilityEnteringValidIdPassword()
        {
            LoginPage.EnterValidIdPassword();
        }
    }
}

