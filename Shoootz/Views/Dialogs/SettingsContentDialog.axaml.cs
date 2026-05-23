using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace Shoootz.Views.Dialogs;

/// <inheritdoc />
public partial class SettingsContentDialog : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsContentDialog"/> class.
    /// </summary>
    /// <param name="content">The raw settings JSON to display.</param>
    public SettingsContentDialog(string content)
    {
        InitializeComponent();

        RegistryOptions registryOptions = new RegistryOptions(ThemeName.LightPlus);
        JsonEditor.InstallTextMate(registryOptions).SetGrammar(registryOptions.GetScopeByLanguageId("json"));

        JsonEditor.Document.Text = content;
    }

    private void OnCloseClicked(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
