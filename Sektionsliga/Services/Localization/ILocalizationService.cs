using System;

namespace Sektionsliga.Services.Localization;

internal interface ILocalizationService
{
    event EventHandler? LanguageChanged;

    string this[string key] { get; }

    void SetLanguage(string cultureCode);
}
