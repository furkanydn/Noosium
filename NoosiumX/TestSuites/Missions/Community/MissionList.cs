

namespace NoosiumX.TestSuites.Missions.Community
{
    using NUnit.Framework;
    using WebDriver.Mock;
    
    [TestFixture,
     Order(1),
     Description("A user login to System to access the functionality of the system."),
     Author("Admin, Manager, My"),
     Platform(Exclude = "Win98,WinXP,Vista"),
     NonParallelizable]
    public class MissionList : BaseMockDriver
    {
        
    }
}

