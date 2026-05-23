using System;
using System.IO;

namespace Shoootz.Services.App;

internal static class AppPath
{
    private const string DatabaseFileName = "database.db";

    private const string SettingsFileName = "settings.json";

    private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    private static readonly string appName = AppDomain.CurrentDomain.FriendlyName;

    public static string AppDataBase => Path.Combine(appDataPath, appName);

    public static string DbFile => Path.Combine(AppDataBase, DatabaseFileName);

    public static string SettingsFile => Path.Combine(AppDataBase, SettingsFileName);
}
