using Noosium.Common;
using Noosium.Common.Private.Elements;
using static Noosium.Common.Elements;

namespace Noosium.PageRepository.Admin.Missions;

public static class ComPage
{
    public static void CheckMissionGroups()
    {
        GetElementByLinkText(MissionsCom.Missions).ClickItem();
    }
}