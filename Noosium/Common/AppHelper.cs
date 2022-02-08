using System.Configuration;

namespace Noosium.Common;

public static class AppHelper
{
    public static string AppBaseUrl => ConfigurationManager.AppSettings["Environment"] ?? string.Empty;
}