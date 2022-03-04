namespace Noosium.WebDriver.TestCases.Desktop.Missions.Community
{
    using OpenQA.Selenium;
    using Resources.Common.Private;
    using Resources.Log;
    using Resources.Util;
    using NUnit.Framework;
    using static Resources.Util.DriverMethods.BasicDriverInterface;

    public static class MissionCommunityList
    {
        public static void CheckComponent_ShouldGetUserMissionList_WhenPrimaryMenuClicked()
        {
            new TestLog().Debug($"{GetDriverUrlWithOutSplit()} opening.");

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuSecond)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuSecond))).Split('\n')[0]} Clicked.");

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuThird)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuThird))).Split('\n')[0]} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuFirst)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.PrimaryMenuFirst))).Split('\n')[0]} Clicked.");
        }
        public static void CheckComponent_ShouldGetMissionListDetails_WhenSecondaryMenuClicked()
        {
            new TestLog().Debug($"{GetDriverUrlWithOutSplit()} opening.");

            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuSecond)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuSecond))).Split('\n')[0]} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuThird)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuThird))).Split('\n')[0]} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFour)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFour))).Split('\n')[0]} Clicked.");
            
            ClickOnElement(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirst)));
            new TestLog().Information($"{GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirst))).Split('\n')[0]} Clicked.");
            
        }

        public static void CheckComponent_ShouldGetActiveMenuMissionCount_WhenSecondaryMenuFirstClicked()
        {
            Assert.That(GetText(By.CssSelector(JsonSoft.GetElement(ElementNames.SecondaryMenuFirstCircular))), Is.Not.EqualTo(0));
        }
    }
}

