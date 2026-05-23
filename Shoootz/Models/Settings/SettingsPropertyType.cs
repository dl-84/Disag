namespace Shoootz.Models.Settings;

internal enum SettingsPropertyType
{
    CurrentLanguageCode,
    DatabaseConnectionString,
    DatabaseProvider,
    ExceptionOnReadContent,
    JsonExceptionOnValidate,
}

internal static class StringExtensions
{
    extension(string? value)
    {
        public SettingsPropertyType? ToSettingsProperty() =>
            value switch
            {
                "ConnectionString" => SettingsPropertyType.DatabaseConnectionString,
                "CurrentLanguageCode" => SettingsPropertyType.CurrentLanguageCode,
                "Provider" => SettingsPropertyType.DatabaseProvider,
                _ => null,
            };
    }
}
