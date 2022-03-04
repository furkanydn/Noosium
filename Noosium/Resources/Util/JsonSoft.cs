namespace Noosium.Resources.Util
{
    using System;
    using System.IO;
    using System.Runtime.Versioning;
    using Newtonsoft.Json;
    
    [Serializable]
    public class JsonSoft
    {
        private static string _appSettings = AppContext.BaseDirectory + @"Resources/appsettings.json";
        private static string _contextJson = AppContext.BaseDirectory + @"Resources/Util/Configuration/context.json";
        private static string _cridJson = AppContext.BaseDirectory + @"Resources/Util/Configuration/crid.json";
        private static string _elementJson = AppContext.BaseDirectory + @"Resources/Util/Configuration/element.json";

        /// <summary>
        /// Deserializes the JSON to a .NET object.
        /// </summary>
        /// <param name="data">This parameter specifies the data source from which the data will be read.</param>
        /// <param name="header">The JSON header to deserialize.</param>
        /// <param name="title">The JSON title to deserialize.</param>
        /// <param name="key">The JSON content to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        /// <exception cref="FileNotFoundException">Initializes a new instance of the FileNotFoundException class with a specified error message.</exception>
        /// <exception cref="InvalidOperationException">The exception that is thrown when a method call is invalid for the object's current state.</exception>
        [UnsupportedOSPlatform("Android23.0")]
        private static string DeserializeObjectWithFile(int data, string header,string title, string key)
        {
            dynamic jsonFile;
            switch (data)
            {
                case 0 when !File.Exists(_appSettings):
                case 1 when !File.Exists(_contextJson):
                case 2 when !File.Exists(_cridJson):
                case 3 when !File.Exists(_elementJson):
                    throw new FileNotFoundException("File with the specified pathname does not exist.");
                case 0:
                    jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(_appSettings)) ??
                               throw new InvalidOperationException();
                    break;
                case 1:
                    jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(_contextJson)) ??
                               throw new InvalidOperationException();
                    break;
                case 2:
                    jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(_cridJson)) ??
                               throw new InvalidOperationException();
                    break;
                case 3:
                    jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(_elementJson)) ??
                               throw new InvalidOperationException();
                    break;
                default:
                    throw new OperationCanceledException(
                        "This error occurs when someone or some thing has blocked an application from running computer");
            }

            return jsonFile[$"{header}"][$"{title}"][$"{key}"];
        }

        /// <summary>
        /// The method parses a string and returns the AppSettings value or object it defines.
        /// </summary>
        /// <param name="key">The JSON PropertyName to deserialize.</param>
        /// <returns>Returns the string values under the 'Setting' object as title in AppSettings.</returns>
        public static string GetAppSetting(string key) => DeserializeObjectWithFile(0, "Setting", "Default", key);

        /// <summary>
        /// The method parses a string and returns the Context value or object it defines.
        /// </summary>
        /// <param name="key">The JSON PropertyName to deserialize.</param>
        /// <returns>Returns the string values under the 'Context' object as the header.</returns>
        public static string GetContext(string key) => DeserializeObjectWithFile(1, "Context", "Values", key);

        /// <summary>
        /// The method parses a string and returns the Exception value or object it defines.
        /// </summary>
        /// <param name="key">The JSON Exceptions to deserialize.</param>
        /// <returns>Returns the string values under the 'System' object as the header.</returns>
        public static string GetException(string key) => DeserializeObjectWithFile(2, "System", "Exception", key);
        
        /// <summary>
        /// The method parses a string and returns the Uri value or object it defines.
        /// </summary>
        /// <param name="key">The JSON Uri to deserialize.</param>
        /// <returns>Returns the string values under the 'System' object as the header.</returns>
        public static string GetUri(string key) => DeserializeObjectWithFile(2, "System", "Uri", key);

        /// <summary>
        /// The method parses a string and returns the Web Elements value or object it defines.
        /// </summary>
        /// <param name="key">The JSON Web Element to deserialize.</param>
        /// <returns>Returns the string values under the 'Web Element' object as the header.</returns>
        public static string GetElement(string key) => DeserializeObjectWithFile(3, "Elements", "Web", key);
    }
}

