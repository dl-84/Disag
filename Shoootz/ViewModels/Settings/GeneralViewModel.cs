using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shoootz.Models.Settings;
using Shoootz.Models.Settings.Language;
using Shoootz.Services.App;
using Shoootz.Services.Language;
using Shoootz.Services.Localization;
using Shoootz.Services.Settings;

namespace Shoootz.ViewModels.Settings;

internal partial class GeneralViewModel : ViewModelBase
{
    private readonly ILocalizationService _localizationService;

    private readonly SettingsModel _settings;

    private readonly ISettingsService _settingsService;

    private bool _allowSave;

    public GeneralViewModel(
        ILanguageService languageService,
        ILocalizationService localizationService,
        SettingsModel settings,
        ISettingsService settingsService
    )
    {
        LanguageOptions = languageService.GetAvailableLanguages();

        _localizationService = localizationService;
        _settings = settings;
        _settingsService = settingsService;

        SelectedLanguageOption = GetLanguage(settings.CurrentLanguageCode);
        _allowSave = true;
    }

    public event Action? DeleteSettingsFolderRequested;

    public event Action? DeleteSettingsFileRequested;

    public event Action<string>? SettingsContentRequested;

    public event Action<SettingsModel>? SettingsSaved;

    public List<LanguageOptionModel> LanguageOptions { get; }

    [ObservableProperty]
    public partial LanguageOptionModel? SelectedLanguageOption { get; set; }

    public void ExecuteDeleteSettingsFile()
    {
        _settingsService.DeleteSettingsFile();
    }

    public void ExecuteDeleteSettingsFolder()
    {
        _settingsService.DeleteSettingsFolder();
    }

    [RelayCommand]
    private void DeleteSettingsFile()
    {
        DeleteSettingsFileRequested?.Invoke();
    }

    [RelayCommand]
    private void DeleteSettingsFolder()
    {
        DeleteSettingsFolderRequested?.Invoke();
    }

    private LanguageOptionModel GetLanguage(string cultureCode)
    {
        return LanguageOptions.FirstOrDefault(languageModel =>
                languageModel.CultureInfo.TwoLetterISOLanguageName == cultureCode
            ) ?? LanguageOptions[0];
    }

    partial void OnSelectedLanguageOptionChanged(LanguageOptionModel? value)
    {
        if (value is null || !_allowSave)
        {
            return;
        }

        _settings.CurrentLanguageCode = value.CultureInfo.TwoLetterISOLanguageName;
        _settingsService.Save(_settings);
        SettingsSaved?.Invoke(_settings);
        _localizationService.SetLanguage(value.CultureInfo.TwoLetterISOLanguageName);
    }

    [RelayCommand]
    private void OpenSettingsFolder()
    {
        Process.Start(new ProcessStartInfo { FileName = AppPath.AppDataBase, UseShellExecute = true });
    }

    [RelayCommand]
    private void ShowSettingsContent()
    {
        SettingsContentRequested?.Invoke(_settingsService.LoadRaw());
    }
}
