using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Noosium.Selenium.Utilities;

public static class Crid
{
    private static string _basePath = Directory.GetCurrentDirectory() + "Noosium/Selenium/Utilities/crid.json";
    private static List<KeyValuePair<string, string>> _config;

    private static IEnumerable<KeyValuePair<string, string>> Config
    {
        get
        {
            if (_config==null)
            {
                Assert.Fail("The necessary information for the application to work could not be located in the file.");
            }

            return _config ?? throw new InvalidOperationException();
        }
    }

    public static string ReadCrid(string key)
    {
        return Config.Where(j => 
                string.Equals(j.Key, $"Uri:{key}", StringComparison.CurrentCultureIgnoreCase))
            .Select(j => 
                j.Value).FirstOrDefault() ?? throw new InvalidOperationException();
    }
}