namespace Noosium.Resources.Log;

public interface ITestLog
{
    void Information(string message);
    void Warning(string message);
    void Debug(string message);
    void Error(string message);
}