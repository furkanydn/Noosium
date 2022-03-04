namespace Noosium.Resources.Common.Private
{
    /// <summary>
    /// It's a file that stores the names that the Dom Element can access in Selenium's Web Interface.
    /// </summary>
    internal struct ElementNames
    {
        // Web Login
        public const string Username = "username";
        public const string Password = "password";
        public const string CaptchaCode = "captchaCode";
        public const string Button = "button";
        public const string FormMessages = "form-messages";
        public const string AlertStrong = "strong";
        // Mission Com
        public const string SessionUsername = "session-username";
        public const string SessionLogOut = "session-logout";
        
        public const string PrimaryMenuFirst = "primary-menu-first";
        public const string PrimaryMenuSecond = "primary-menu-second";
        public const string PrimaryMenuThird = "primary-menu-third";
        
        public const string SecondaryMenuFirst = "secondary-menu-child-first";
        public const string SecondaryMenuFirstCircular = "secondary-menu-child-first-circular";
        public const string SecondaryMenuSecond = "secondary-menu-child-second";
        public const string SecondaryMenuThird = "secondary-menu-child-third";
        public const string SecondaryMenuFour = "secondary-menu-child-four";
        
        public const string Missions = ".middle > .item:nth-child(1)";
    }
}

