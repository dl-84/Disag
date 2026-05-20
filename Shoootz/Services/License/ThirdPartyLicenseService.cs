using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Shoootz.Models;

namespace Shoootz.Services.License;

internal class ThirdPartyLicenseService : IThirdPartyLicenseService
{
    private const string LicensesFileName = "licenses.json";

    private static readonly HashSet<string> _excludedPackages =
    [
        "AvaloniaUI.DiagnosticsSupport", // Debug-only, no license in generated JSON
        "StyleCop.Analyzers", // Build-time analyzer, Apache-2.0, PrivateAssets=all
    ];

    public List<ThirdPartyPackageModel> GetPackages()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string? resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(name => name.EndsWith(LicensesFileName));

        if (resourceName is null)
        {
            return [];
        }

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        List<ThirdPartyPackageModel>? packages = JsonSerializer.Deserialize<List<ThirdPartyPackageModel>>(stream);

        return packages
                ?.Where(package => !_excludedPackages.Contains(package.PackageName))
                .OrderBy(p => p.PackageName)
                .ToList()
            ?? [];
    }
}
