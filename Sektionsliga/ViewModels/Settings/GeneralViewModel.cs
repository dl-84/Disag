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
    private readonly ILocalizationService localizationService;

    private readonly ISettingsService settingsService;

    public List<LanguageOptionModel> LanguageOptions { get; }

    [ObservableProperty]
    public partial LanguageOptionModel SelectedLanguageOption { get; set; }

    public GeneralViewModel(
        ILanguageService languageService,
        ILocalizationService localizationService,
        ISettingsService settingsService
    )
    {
        this.localizationService = localizationService;
        this.settingsService = settingsService;

        LanguageOptions = languageService.GetAvailableLanguages();

        AppSettingsModel settings = this.settingsService.Load();
        SelectedLanguageOption = GetLanguage(settings.CurrentLanguageCode);
    }

    partial void OnSelectedLanguageOptionChanged(LanguageOptionModel value)
    {
        settingsService.Save(new AppSettingsModel { CurrentLanguageCode = value.CultureInfo.TwoLetterISOLanguageName });

        localizationService.SetLanguage(value.CultureInfo.TwoLetterISOLanguageName);
    }

    private LanguageOptionModel GetLanguage(string cultureCode)
    {
        return LanguageOptions.FirstOrDefault(languageModel =>
                languageModel.CultureInfo.TwoLetterISOLanguageName == cultureCode
            ) ?? LanguageOptions[0];
    }
}
