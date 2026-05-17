using System;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace Sektionsliga.Extensions;

public class LocalizationExtension(string key) : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new Binding(nameof(LocalizationSource.Value))
        {
            Source = new LocalizationSource(key),
            Mode = BindingMode.OneWay,
        };
    }
}
