using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Sektionsliga.Models;
using Sektionsliga.Services.Language;
using Sektionsliga.Services.Localization;
using Sektionsliga.Services.Settings;

namespace Sektionsliga.ViewModels.Settings;

public partial class GeneralViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;
    private readonly ILocalizationService _localizationService;

    public List<LanguageOptionModel> LanguageOptions { get; }

    [ObservableProperty]
    public partial LanguageOptionModel SelectedLanguageOption { get; set; }

    public string Title => _localizationService["GeneralTitle"];
    public string DisplayLanguageLabel => _localizationService["DisplayLanguageLabel"];

    public GeneralViewModel(
        ISettingsService settingsService,
        ILanguageService languageService,
        ILocalizationService localizationService
    )
    {
        _settingsService = settingsService;
        _localizationService = localizationService;
        _localizationService.LanguageChanged += (_, _) => OnPropertyChanged(string.Empty);

        LanguageOptions = languageService.GetAvailableLanguages();

        AppSettingsModel settings = _settingsService.Load();
        SelectedLanguageOption = GetLanguage(settings.CurrentLanguageCode);
    }

    partial void OnSelectedLanguageOptionChanged(LanguageOptionModel value)
    {
        _settingsService.Save(
            new AppSettingsModel { CurrentLanguageCode = value.CultureInfo.TwoLetterISOLanguageName }
        );
        _localizationService.SetLanguage(value.CultureInfo.TwoLetterISOLanguageName);
    }

    private LanguageOptionModel GetLanguage(string cultureCode)
    {
        return LanguageOptions.FirstOrDefault(o => o.CultureInfo.TwoLetterISOLanguageName == cultureCode)
            ?? LanguageOptions[0];
    }
}
