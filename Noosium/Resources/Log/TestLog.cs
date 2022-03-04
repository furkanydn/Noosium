using System;
using NUnit.Framework;

namespace Noosium.Resources.Log
{
    internal class TestLog : ITestLog
    {
        public void Information(string message) =>
            TestContext.Progress.WriteLine("[Information]- "+$"{DateTime.UtcNow:s}- "+message);

        public void Warning(string message) => 
            TestContext.Progress.WriteLine("[Warning]-"+$"{DateTime.UtcNow:s}-"+message);

        public void Debug(string message) => 
            TestContext.Progress.WriteLine("[Debug]-"+$"{DateTime.UtcNow:s}-"+ message);

        public void Error(string message) =>
            TestContext.Progress.WriteLine("[Error]-"+$"{DateTime.UtcNow:s}-"+ message);

    }
}

