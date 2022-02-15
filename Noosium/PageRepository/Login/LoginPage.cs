using Noosium.Common;
using Noosium.Utilities;

namespace Noosium.PageRepository.Login;

public class LoginPage
{
    public static void EnterValidIdPassword()
    {
        Elements.GetElementById("username").ClickItem();
        Elements.GetElementById("username").EnterText(Crid.GetContextCrid("username"));
        /*
        _username.SendKeys();
        _password.Click();
        _password.SendKeys(Crid.GetContextCrid("password"));
        _captchaCode.Click();
        _captchaCode.SendKeys(Crid.GetContextCrid("captcha"));
        _button.Click();
        */
    }
}