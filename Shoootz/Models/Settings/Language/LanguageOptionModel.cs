using System.Globalization;
using Avalonia.Media.Imaging;

namespace Shoootz.Models.Settings.Language;

internal class LanguageOptionModel(string twoLetterIsoLanguageName, Bitmap flag)
{
    public CultureInfo CultureInfo { get; } = new(twoLetterIsoLanguageName);

    public Bitmap Flag { get; } = flag;
}
