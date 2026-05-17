using System;

namespace Sektionsliga.Services.Localization;

public interface ILocalizationService
{
    string this[string key] { get; }

    void SetLanguage(string cultureCode);

    event EventHandler? LanguageChanged;
}
