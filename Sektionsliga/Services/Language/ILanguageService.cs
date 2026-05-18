using System.Collections.Generic;
using Sektionsliga.Models;

namespace Sektionsliga.Services.Language;

internal interface ILanguageService
{
    List<LanguageOptionModel> GetAvailableLanguages();
}
