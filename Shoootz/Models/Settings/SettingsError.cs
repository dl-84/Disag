namespace Shoootz.Models.Settings;

internal record SettingsError(SettingsPropertyType PropertyType, string Message, string? Value = null);
