namespace Noosium.Common.Private.Objects;

internal struct CridContexts
{
    public const string UserName = "username";
    public const string InvalidUserName = "iusername";
    public const string Password = "password";
    public const string InvalidPassword = "ipassword";
    public const string Captcha = "captcha";
    public const string InvalidCaptcha = "icaptcha";
    
}

internal struct CridErrorMessages
{
    public const string FormMessagesText = "Lütfen resimdeki karakterleri doğru giriniz.";
    public const string AccountDoesNotExist = "Account does not exist or password is incorrect.";
}

internal struct CridUris
{
    public const string BaseUrl = "BaseUrl";
    public const string AdminMissionsCom = "Admin/Missions/COM";
}

internal struct CridAppSetting
{
    public const string TestName = "testName";
    public const string Browser = "browser";
    public const string Platform = "platform";
    public const string StateCode = "stateCode";
}