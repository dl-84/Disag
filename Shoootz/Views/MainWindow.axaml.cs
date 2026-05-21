using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;

namespace Shoootz.Views;

/// <inheritdoc />
public partial class MainWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            RootBorder.CornerRadius = new CornerRadius(0);
        }
    }
}
