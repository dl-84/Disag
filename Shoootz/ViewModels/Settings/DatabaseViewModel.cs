using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shoootz.Models.Settings;
using Shoootz.Models.Settings.Database;
using Shoootz.Services.Settings;

namespace Shoootz.ViewModels.Settings;

internal partial class DatabaseViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;

    private readonly SettingsModel _settings;

    public DatabaseViewModel(SettingsModel settings, ISettingsService settingsService)
    {
        _settings = settings;
        _settingsService = settingsService;

        ConnectionString = settings.DbConnectionModel.ConnectionString;
        SelectedProvider = settings.DbConnectionModel.ProviderType;
    }

    public event Action<SettingsModel>? SettingsSaved;

    public IEnumerable<ProviderType> ProviderOptions { get; } = Enum.GetValues<ProviderType>();

    [ObservableProperty]
    public partial string ConnectionString { get; set; }

    [ObservableProperty]
    public partial ProviderType SelectedProvider { get; set; }

    [RelayCommand]
    private void Save()
    {
        _settings.DbConnectionModel.ConnectionString = ConnectionString;
        _settings.DbConnectionModel.ProviderType = SelectedProvider;
        _settingsService.Save(_settings);
        SettingsSaved?.Invoke(_settings);
    }
}
