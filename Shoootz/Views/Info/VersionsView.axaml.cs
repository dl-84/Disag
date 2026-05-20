using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Shoootz.Views.Info;

/// <inheritdoc />
public partial class VersionsView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VersionsView"/> class.
    /// </summary>
    public VersionsView()
    {
        InitializeComponent();
    }

    private void OnLinkClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string url })
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
    }
}
