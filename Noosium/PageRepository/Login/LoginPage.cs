using Noosium.Common;
using Noosium.Common.Private.Elements;
using Noosium.Common.Private.Objects;
using NUnit.Framework;
using static Noosium.Common.Elements;
using static Noosium.Utilities.Crid;

namespace Noosium.PageRepository.Login;

public static class LoginPage
{
    public static void EnterValidIdPassword()
    {
        DriverUtilities.NavigateToUrl(GetUriCrid(CridUris.BaseUrl));
        GetElementById(WebLogin.UserName).ClickItem();
        GetElementById(WebLogin.UserName).EnterText(GetContextCrid(CridContexts.UserName));
        GetElementById(WebLogin.Password).ClickItem();
        GetElementById(WebLogin.Password).EnterText(GetContextCrid(CridContexts.Password));
        GetElementById(WebLogin.CaptchaCode).ClickItem();
        GetElementById(WebLogin.CaptchaCode).EnterText(GetContextCrid(CridContexts.Captcha));
        GetElementByName(WebLogin.Button).ClickItem();
    }
}