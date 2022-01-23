using System;
using System.IO;
using Newtonsoft.Json;

namespace Noosium.Selenium.Utilities;

[Serializable]
public class Crid
{
    private static string _basePath =AppContext.BaseDirectory+ @"Selenium/Utilities/crid.json";
    public static string? ReadCrid(string title,string key)
    {
        if (!File.Exists(_basePath)) return null;
        dynamic jsonFileList = JsonConvert.DeserializeObject(File.ReadAllText(_basePath)) ?? throw new InvalidOperationException();
        return jsonFileList[$"{title}"][$"{key}"];
    }
}