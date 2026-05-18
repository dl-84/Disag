using System.Globalization;
using Avalonia.Media.Imaging;

namespace Sektionsliga.Models;

internal class LanguageOptionModel(string twoLetterIsoLanguageName, Bitmap flag)
{
    public CultureInfo CultureInfo { get; } = new(twoLetterIsoLanguageName);

    public Bitmap Flag { get; } = flag;
}
