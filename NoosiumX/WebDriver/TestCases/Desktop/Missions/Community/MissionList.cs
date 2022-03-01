using OpenQA.Selenium;

namespace NoosiumX.WebDriver.TestCases.Desktop.Missions.Community
{
    using Resources.Common.Private;
    using Resources.Log;
    using Resources.Util;
using static NoosiumX.Resources.Util.DriverMethods.BasicDriverInterface;
    
    public class MissionList
    {
        public static void CheckComponent_ShouldGetMissionInTheSubScatter_WhenChildClicked()
        {
            new TestLog().Debug($"{GetDriverUrlWithOutSplit()} opening.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuActive)));
        }
    }
}

