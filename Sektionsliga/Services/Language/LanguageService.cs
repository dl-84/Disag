using System.Collections.Generic;
using Sektionsliga.Models;
using Sektionsliga.Services.Flag;

namespace Sektionsliga.Services.Language;

internal class LanguageService(IFlagService flagService) : ILanguageService
{
    private readonly List<string> availableLanguages = ["de", "en"];

    public List<LanguageOptionModel> GetAvailableLanguages()
    {
        List<LanguageOptionModel> result = [];

        foreach (string language in availableLanguages)
        {
            result.Add(new LanguageOptionModel(language, flagService.GetFlag(language)));
        }

        return result;
    }
}
