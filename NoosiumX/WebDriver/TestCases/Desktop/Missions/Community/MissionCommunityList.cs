

using NoosiumX.Resources.Util.WaitAndTimeOut;
using NUnit.Framework;

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

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuSecond)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuSecond)))} Clicked.");

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuThird)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuThird)))} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuFirst)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuFirst)))} Clicked.");
        }
        public static void CheckComponent_ShouldGetMissionListDetails_WhenSecondaryMenuClicked()
        {
            new TestLog().Debug($"{GetDriverUrlWithOutSplit()} opening.");

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuSecond)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuSecond)))} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuThird)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuThird)))} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFour)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFour)))} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirst)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirst)))} Clicked.");
            
        }

        public static void CheckComponent_ShouldGetActiveMenuMissionCount_WhenSecondaryMenuFirstClicked()
        {
            Assert.That(GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirstCircular))), Is.Not.EqualTo(0));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirst)))} Clicked.");
        }
    }
}

