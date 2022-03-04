namespace Noosium.TestSuites
{
    using WebDriver.Mock;
    using WebDriver.TestCases.Desktop.Missions.Community;
    using NUnit.Framework;
    
    [TestFixture,
     Order(1),
     Description("A user login to System to access the functionality of the system."),
     Author("Admin, Manager, My"),
     Platform(Exclude = "Win98,WinXP,Vista"),
     NonParallelizable]
    public class MissionList : BaseMockDriver
    {
        [Test, Order(0)]
        public void CorrectMissionDisplayed_When_SwitchTheMenus()
        {
            MissionCommunityList.CheckComponent_ShouldGetUserMissionList_WhenPrimaryMenuClicked();
            
            MissionCommunityList.CheckComponent_ShouldGetMissionListDetails_WhenSecondaryMenuClicked();

            MissionCommunityList.CheckComponent_ShouldGetActiveMenuMissionCount_WhenSecondaryMenuFirstClicked();
        }
    }
}

