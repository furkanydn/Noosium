using System;
using System.IO;
using System.Runtime.Versioning;
using Newtonsoft.Json;

namespace Noosium.Utilities;

[Serializable]
public class Crid
{
    private static string _basePath = AppContext.BaseDirectory+ @"Utilities/crid.json";
    private static string _settingPath = AppContext.BaseDirectory + @"appsettings.json";

    /// <summary>
    /// Deserializes the JSON to a .NET object.
    /// </summary>
    /// <param name="data">This parameter specifies the data source from which the data will be read.</param>
    /// <param name="title">The JSON header to deserialize.</param>
    /// <param name="key">The JSON content to deserialize.</param>
    /// <returns>The deserialized object from the JSON string.</returns>
    /// <exception cref="FileNotFoundException">Initializes a new instance of the FileNotFoundException class with a specified error message.</exception>
    /// <exception cref="InvalidOperationException">The exception that is thrown when a method call is invalid for the object's current state.</exception>
    [UnsupportedOSPlatform("Android23.0")]
    private static string DeserializeObject(int data,string title,string key)
    {
        dynamic jsonFileList;
        switch (data)
        {
            case 0 when !File.Exists(_basePath): case 1 when !File.Exists(_settingPath):
                throw new FileNotFoundException("File does not exist.");
            case 0: jsonFileList = JsonConvert.DeserializeObject(File.ReadAllText(_basePath)) ?? throw new InvalidOperationException();
                break;
            case 1: jsonFileList = JsonConvert.DeserializeObject(File.ReadAllText(_settingPath)) ?? throw new InvalidOperationException();
                break;
            default:
                throw new OperationCanceledException(
                    "This error occurs when someone or some thing has blocked an application from running computer");
        }
        return jsonFileList[$"{title}"][$"{key}"];
    }

    /// <summary>
    /// The method parses a string and returns the Uri value or object it defines.
    /// </summary>
    /// <param name="key">The JSON PropertyName to deserialize.</param>
    /// <returns>Returns the string values under the 'Uri' object as the header.</returns>
    public static string GetUriCrid(string key)
    {
        return DeserializeObject(0,"Uri", key);
    }
    /// <summary>
    /// The method parses a string and returns the Context value or object it defines.
    /// </summary>
    /// <param name="value">The JSON PropertyName to deserialize.</param>
    /// <returns>Returns the string values under the 'Context' object as the header.</returns>
    public static string GetContextCrid(string value)
    {
        return DeserializeObject(0,"Context", value);
    }

    /// <summary>
    /// The method parses a string and returns the AppSettings value or object it defines.
    /// </summary>
    /// <param name="key">The JSON PropertyName to deserialize.</param>
    /// <returns>Returns the string values under the 'Setting' object as title in AppSettings.</returns>
    public static string GetAppSettings(string key)
    {
        return DeserializeObject(1, "Setting", key);
    }
}