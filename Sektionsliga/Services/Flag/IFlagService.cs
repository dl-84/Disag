using Avalonia.Media.Imaging;

namespace Sektionsliga.Services.Flag;

internal interface IFlagService
{
    Bitmap GetFlag(string twoLetterIsoLanguageName);
}
