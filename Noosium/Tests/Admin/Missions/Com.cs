using Noosium.Common;
using Noosium.PageRepository.Admin.Missions;
using NUnit.Framework;

namespace Noosium.Tests.Admin.Missions;

[Author("Admin")]
[Description("It outlines the tests that must be passed in order to access the Missions screen's functionality.")]
[TestFixture, Order(1)]
[Platform(Exclude="Win98,WinXP,Vista")]
public class Com
{
    [Description("Check results on missions screen general groups")]
    [Test, Order(0)]
    public void CheckAbilityMissionScreenGroups()
    {
        ComPage.CheckMissionGroups();
    }
}