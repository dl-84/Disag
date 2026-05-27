using System.Globalization;
using System.Text.Json.Serialization;
using Shoootz.Models.Settings.Database;
using Shoootz.Models.Settings.Udp;

namespace Shoootz.Models.Settings;

internal class SettingsModel
{
    public string CurrentLanguageCode { get; set; } = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

    [JsonPropertyName("Database")]
    public DbConnectionModel DbConnectionModel { get; init; } = new();

    [JsonPropertyName("Udp")]
    public UdpConnectionModel UdpConnectionModel { get; init; } = new();
}
