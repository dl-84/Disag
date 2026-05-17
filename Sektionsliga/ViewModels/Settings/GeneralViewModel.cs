using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Sektionsliga.Models;
using Sektionsliga.Services.Settings;
using Sektionsliga.ViewModels.Settings.Dtos;

namespace Sektionsliga.ViewModels.Settings;

public partial class GeneralViewModel : ViewModelBase
{
    public static List<LanguageOptionDto> LanguageOptions => [new LanguageOptionDto("de"), new LanguageOptionDto("en")];

    [ObservableProperty]
    public partial LanguageOptionDto SelectedLanguageOption { get; set; }

    private readonly ISettingsService _settingsService;
    private bool _initialized;

    public GeneralViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        AppSettingsDto settings = _settingsService.Load();
        SelectedLanguageOption =
            LanguageOptions.FirstOrDefault(o => o.CultureInfo.TwoLetterISOLanguageName == settings.LanguageCultureCode)
            ?? LanguageOptions[0];
        _initialized = true;
    }

    partial void OnSelectedLanguageOptionChanged(LanguageOptionDto value)
    {
        if (!_initialized)
        {
            return;
        }

        _settingsService.Save(new AppSettingsDto { LanguageCultureCode = value.CultureInfo.TwoLetterISOLanguageName });
    }
}
