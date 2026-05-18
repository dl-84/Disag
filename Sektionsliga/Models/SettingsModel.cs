using System.Globalization;

namespace Sektionsliga.Models;

internal class SettingsModel
{
    public string CurrentLanguageCode { get; set; } = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
}
