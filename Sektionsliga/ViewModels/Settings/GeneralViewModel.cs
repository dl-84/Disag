using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sektionsliga.Models;
using Sektionsliga.Services.Language;
using Sektionsliga.Services.Localization;
using Sektionsliga.Services.Settings;

namespace Sektionsliga.ViewModels.Settings;

internal partial class GeneralViewModel : ViewModelBase
{
    private readonly ILocalizationService _localizationService;

    private readonly ISettingsService _settingsService;

    private readonly SettingsModel? _settings;

    private readonly List<SettingsError>? _settingsErrors;

    public GeneralViewModel(
        ILanguageService languageService,
        ILocalizationService localizationService,
        ISettingsService settingsService,
        SettingsModel? settings,
        List<SettingsError>? settingsErrors
    )
    {
        LanguageOptions = languageService.GetAvailableLanguages();

        _localizationService = localizationService;
        _settingsService = settingsService;
        _settings = settings;
        _settingsErrors = settingsErrors;

        SelectedLanguageOption = GetLanguage(settings?.CurrentLanguageCode ?? "unknown");
    }

    public bool HasValidationErrors => _settingsErrors is not null && _settingsErrors.Count > 0;

    public List<LanguageOptionModel> LanguageOptions { get; }

    [ObservableProperty]
    public partial LanguageOptionModel SelectedLanguageOption { get; set; }

    [RelayCommand]
    private void DeleteSettings()
    {
        _settingsService.Delete();
    }

    private LanguageOptionModel GetLanguage(string cultureCode)
    {
        return LanguageOptions.FirstOrDefault(languageModel =>
                languageModel.CultureInfo.TwoLetterISOLanguageName == cultureCode
            ) ?? LanguageOptions[0];
    }

    partial void OnSelectedLanguageOptionChanged(LanguageOptionModel value)
    {
        _settings?.CurrentLanguageCode = value.CultureInfo.TwoLetterISOLanguageName;
        _settingsService.Save(_settings);
        _localizationService.SetLanguage(value.CultureInfo.TwoLetterISOLanguageName);
    }

    [RelayCommand]
    private void OpenSettingsFolder()
    {
        Process.Start(new ProcessStartInfo { FileName = _settingsService.FolderPath, UseShellExecute = true });
    }
}
