using System;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

namespace Shoootz.Extensions;

/// <inheritdoc />
public class LocalizationExtension(string key) : MarkupExtension
{
    public bool ToUpper { get; set; }

    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new Binding(nameof(LocalizationSource.Value))
        {
            Source = new LocalizationSource(key, ToUpper),
            Mode = BindingMode.OneWay,
        };
    }
}
