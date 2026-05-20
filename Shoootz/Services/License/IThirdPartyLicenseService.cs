using System.Collections.Generic;
using Shoootz.Models;

namespace Shoootz.Services.License;

internal interface IThirdPartyLicenseService
{
    List<ThirdPartyPackageModel> GetPackages();
}
