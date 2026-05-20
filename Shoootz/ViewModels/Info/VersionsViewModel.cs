using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Shoootz.Models;

namespace Shoootz.ViewModels.Info;

internal partial class VersionsViewModel : ViewModelBase
{
    public string AppVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";

    public string DatabaseVersion => "0.0.1";

    public List<ThirdPartyPackageModel> Packages { get; } = LoadPackages();

    private static List<ThirdPartyPackageModel> LoadPackages()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string? resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("licenses.json"));

        if (resourceName is null)
        {
            return [];
        }

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        List<LicenseEntry>? entries = JsonSerializer.Deserialize<List<LicenseEntry>>(stream);

        return entries
                ?.OrderBy(e => e.PackageName)
                .Select(e => new ThirdPartyPackageModel(e.PackageName, e.PackageVersion, e.LicenseType, e.PackageUrl))
                .ToList()
            ?? [];
    }

    private sealed class LicenseEntry
    {
        [JsonPropertyName("PackageName")]
        public string PackageName { get; set; } = string.Empty;

        [JsonPropertyName("PackageVersion")]
        public string PackageVersion { get; set; } = string.Empty;

        [JsonPropertyName("LicenseType")]
        public string LicenseType { get; set; } = string.Empty;

        [JsonPropertyName("PackageUrl")]
        public string PackageUrl { get; set; } = string.Empty;
    }
}
