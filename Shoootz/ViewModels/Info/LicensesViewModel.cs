using System;
using System.Collections.Generic;
using Controls.LicenseTable;
using Shoootz.Services.License;
using Shoootz.Services.Localization;

namespace Shoootz.ViewModels.Info;

internal partial class LicensesViewModel : ViewModelBase
{
    private readonly ILocalizationService _localizationService;

    public LicensesViewModel(ILicenseService licenseService, ILocalizationService localizationService)
    {
        _localizationService = localizationService;
        Packages = licenseService.GetThirdPartyPackages();
        localizationService.LanguageChanged += OnLanguageChanged;
    }

    public IReadOnlyList<string> ColumnHeaders =>
        [
            _localizationService["Name"],
            _localizationService["Version"],
            _localizationService["License"],
            _localizationService["Link"],
        ];

    public List<PackageModel> Packages { get; }

    private void OnLanguageChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(ColumnHeaders));
    }
}
