namespace Noosium.Tests
{
    using Common;
    using NUnit.Framework;
    
    public class TestRootSuite
    {
        [OneTimeSetUp]
        public void CreateDriver()
        {
            DriverUtilities.LaunchBrowser();
        }

        [OneTimeTearDown]
        public void CloseDriver()
        {
            DriverUtilities.TearDown();
        }
    }
}

