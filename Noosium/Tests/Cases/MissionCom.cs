namespace Noosium.Tests.Cases
{
    using PageRepository.Admin.Missions;
    using NUnit.Framework;
    
    [TestFixture,
     Description("It outlines the tests that must be passed in order to access the Missions screen's functionality."),
     Author("Admin"),
     Platform(Exclude = "Win98,WinXP,Vista")]
    internal class AdminMissionComCases : TestRootSuite
    {
        [Test, Description("Check results on missions screen general groups"), Order(1)]
        public void CheckAbilityMissionScreenGroups()
        {
            ComPage.CheckMissionGroups();
        }
    }
}

