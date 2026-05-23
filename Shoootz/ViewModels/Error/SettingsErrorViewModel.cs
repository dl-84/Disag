using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using Shoootz.Models.Settings;
using Shoootz.Services.Settings;

namespace Shoootz.ViewModels.Error;

internal partial class SettingsErrorViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;

    public SettingsErrorViewModel(List<SettingsError> settingsErrors, ISettingsService settingsService)
    {
        ErrorMessages = settingsErrors.Select(settingsError => settingsError.Message).ToList();
        RawContent = settingsService.LoadRaw();
        _settingsService = settingsService;
    }

    public IReadOnlyList<string> ErrorMessages { get; }

    public string RawContent { get; }

    public void SaveContent(string content)
    {
        _settingsService.SaveRaw(content);
        Restart();
    }

    private static void Restart()
    {
        Process.Start(new ProcessStartInfo { FileName = Environment.ProcessPath!, UseShellExecute = true });
        Environment.Exit(0);
    }

    [RelayCommand]
    private void RestoreDefaults()
    {
        _settingsService.DeleteSettingsFile();
        Restart();
    }
}
