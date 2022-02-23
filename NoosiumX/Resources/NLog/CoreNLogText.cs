namespace NoosiumX.Resources.NLog
{
    public interface ICoreNLogText
    {
        void Information(string message);
        void Warning(string message);  
        void Debug(string message);  
        void Error(string message);
    }
}

