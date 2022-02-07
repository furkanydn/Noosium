using System;
using System.IO;
using System.Runtime.Versioning;
using Newtonsoft.Json;

namespace Noosium.Utilities;

[Serializable]
public class Crid
{
    private static string _basePath = AppContext.BaseDirectory+ @"Utilities/crid.json";

    /// <summary>
    /// Deserializes the JSON to a .NET object.
    /// </summary>
    /// <param name="title">The JSON header to deserialize.</param>
    /// <param name="key">The JSON content to deserialize.</param>
    /// <returns>The deserialized object from the JSON string.</returns>
    /// <exception cref="FileNotFoundException">Initializes a new instance of the FileNotFoundException class with a specified error message.</exception>
    /// <exception cref="InvalidOperationException">The exception that is thrown when a method call is invalid for the object's current state.</exception>
    [UnsupportedOSPlatform("Android26.0")]
    private static string DeserializeObject(string title,string key)
    {
        if (!File.Exists(_basePath)) throw new FileNotFoundException("File does not exist.");
        dynamic jsonFileList = JsonConvert.DeserializeObject(File.ReadAllText(_basePath)) ?? throw new InvalidOperationException();
        return jsonFileList[$"{title}"][$"{key}"];
    }

    /// <summary>
    /// The method parses a string and returns the Uri value or object it defines.
    /// </summary>
    /// <param name="key">The JSON PropertyName to deserialize.</param>
    /// <returns>Returns the string values under the 'Uri' object as the header.</returns>
    public static string GetUriCrid(string key)
    {
        return DeserializeObject("Uri", key);
    }
    /// <summary>
    /// The method parses a string and returns the Context value or object it defines.
    /// </summary>
    /// <param name="value">The JSON PropertyName to deserialize.</param>
    /// <returns>Returns the string values under the 'Context' object as the header.</returns>
    public static string GetContextCrid(string value)
    {
        return DeserializeObject("Context", value);
    }
}