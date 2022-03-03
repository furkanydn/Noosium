

using NoosiumX.Resources.Util.WaitAndTimeOut;

namespace NoosiumX.WebDriver.TestCases.Desktop.Missions.Community
{
    using OpenQA.Selenium;
    using Resources.Common.Private;
    using Resources.Log;
    using Resources.Util;
    using static Resources.Util.DriverMethods.BasicDriverInterface;

    public static class MissionCommunityList
    {
        public static void CheckComponent_ShouldGetUserMissionList_WhenPrimaryMenuClicked()
        {
            new TestLog().Debug($"{GetDriverUrlWithOutSplit()} opening.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuActive)));
            new TestLog().Information($"{ElementNames.PrimaryMenuActive} Clicked.");
            //todo-sub-scatter control
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuSecond)));
            new TestLog().Information($"{ElementNames.PrimaryMenuSecond} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuThird)));
            new TestLog().Information($"{ElementNames.PrimaryMenuThird} Clicked.");
        }
        public static void CheckComponent_ShouldGetMissionInTheSubScatter_WhenChildClicked()
        {
            new TestLog().Debug($"{GetDriverUrlWithOutSplit()} opening.");

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuActive)));
        }
    }
}

