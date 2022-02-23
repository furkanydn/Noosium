namespace NoosiumX.Resources.Common.Private
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
        public const string Missions = ".middle > .item:nth-child(1)";
    }
}

