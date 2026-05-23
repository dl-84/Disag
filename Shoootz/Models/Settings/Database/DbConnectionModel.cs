using System.Text.Json.Serialization;
using Shoootz.Services.App;

namespace Shoootz.Models.Settings.Database;

internal class DbConnectionModel
{
    public string ConnectionString { get; set; } = $"Data Source={AppPath.DbFile}";

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("Provider")]
    public ProviderType ProviderType { get; set; } = ProviderType.Sqlite;
}
