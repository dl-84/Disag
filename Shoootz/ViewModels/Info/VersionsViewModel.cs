using System.Collections.Generic;
using System.Reflection;
using Shoootz.Models;
using Shoootz.Services.License;

namespace Shoootz.ViewModels.Info;

internal partial class VersionsViewModel(IThirdPartyLicenseService thirdPartyLicenseService) : ViewModelBase
{
    public static string AppVersion => Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "n/a";

    public static string DatabaseVersion => "0.0.1";

    public List<ThirdPartyPackageModel> Packages { get; } = thirdPartyLicenseService.GetPackages();
}
