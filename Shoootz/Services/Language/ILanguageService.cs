using System.Collections.Generic;
using Shoootz.Models.Settings.Language;

namespace Shoootz.Services.Language;

internal interface ILanguageService
{
    List<LanguageOptionModel> GetAvailableLanguages();
}
