using System;
using Avalonia.Controls;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using Shoootz.ViewModels.Error;
using TextMateSharp.Grammars;

namespace Shoootz.Views.Error;

/// <inheritdoc />
public partial class SettingsErrorView : UserControl
{
    private TextEditor? _editor;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsErrorView"/> class.
    /// </summary>
    public SettingsErrorView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
        SaveButton.Click += OnSaveButtonClick;
    }

    private static TextEditor BuildEditor(string content)
    {
        RegistryOptions registryOptions = new RegistryOptions(ThemeName.LightPlus);
        TextEditor editor = new TextEditor
        {
            FontFamily = new Avalonia.Media.FontFamily("Consolas,Courier New,monospace"),
            FontSize = 15,
            ShowLineNumbers = true,
            WordWrap = false,
        };
        editor.InstallTextMate(registryOptions).SetGrammar(registryOptions.GetScopeByLanguageId("json"));
        editor.Document.Text = content;
        return editor;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not SettingsErrorViewModel viewModel)
        {
            return;
        }

        _editor = BuildEditor(viewModel.RawContent);
        EditorContainer.Content = _editor;
    }

    private void OnSaveButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is SettingsErrorViewModel viewModel && _editor is not null)
        {
            viewModel.SaveContent(_editor.Document.Text);
        }
    }
}
