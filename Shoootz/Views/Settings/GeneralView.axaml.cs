using System.Threading.Tasks;
using Avalonia.Controls;
using Shoootz.ViewModels;
using Shoootz.ViewModels.Settings;
using Shoootz.Views.Dialogs;

namespace Shoootz.Views.Settings;

/// <inheritdoc />
public partial class GeneralView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeneralView"/> class.
    /// </summary>
    public GeneralView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, System.EventArgs e)
    {
        if (DataContext is GeneralViewModel viewModel)
        {
            viewModel.SettingsContentRequested += OnSettingsContentRequested;
        }
    }

    private void OnSettingsContentRequested(string content)
    {
        _ = OpenSettingsContentDialogAsync(content);
    }

    private async Task OpenSettingsContentDialogAsync(string content)
    {
        if (TopLevel.GetTopLevel(this) is not Window window)
        {
            return;
        }

        MainWindowViewModel? vm = window.DataContext as MainWindowViewModel;

        try
        {
            if (vm is not null)
            {
                vm.IsDialogOpen = true;
            }

            await new SettingsContentDialog(content).ShowDialog(window);
        }
        finally
        {
            if (vm is not null)
            {
                vm.IsDialogOpen = false;
            }
        }
    }
}
