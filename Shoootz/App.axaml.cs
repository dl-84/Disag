using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shoootz.Data;
using Shoootz.Models.Settings;
using Shoootz.Models.Settings.Database;
using Shoootz.Services.Database;
using Shoootz.Services.Grafik;
using Shoootz.Services.Language;
using Shoootz.Services.License;
using Shoootz.Services.Localization;
using Shoootz.Services.Settings;
using Shoootz.ViewModels;
using Shoootz.Views;
using Themes.Disag;

namespace Shoootz;

/// <inheritdoc />
public class App : Application
{
    private IServiceProvider? _serviceProvider;

    /// <inheritdoc/>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Styles.Add(new Disag());
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        DbConnectionModel? dbConnection = ReadDbConnection();
        _serviceProvider = InitServiceProvider(dbConnection);

        MainWindowViewModel mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        ILocalizationService localizationService = _serviceProvider.GetRequiredService<ILocalizationService>();

        SettingsModel? settings = ReadSettings(out List<SettingsError>? settingsErrors);
        localizationService.SetLanguage(settings?.CurrentLanguageCode ?? "de");

        if (settings is not null)
        {
            mainWindowViewModel.InitSettings(settings);
            _serviceProvider.GetRequiredService<IDatabaseService>().InitializeAsync().GetAwaiter().GetResult();
        }

        if (settingsErrors is not null)
        {
            mainWindowViewModel.RedirectToSettingsError(settingsErrors);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = mainWindowViewModel };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static DbConnectionModel? ReadDbConnection()
    {
        return new SettingsService()
            .Load()
            .Match<DbConnectionModel?>(settings => settings.DbConnectionModel, _ => null);
    }

    private static ServiceProvider InitServiceProvider(DbConnectionModel? dbConnection)
    {
        ServiceCollection services = new ServiceCollection();
        InitSingletons(services, dbConnection);
        return services.BuildServiceProvider();
    }

    private static void InitSingletons(ServiceCollection services, DbConnectionModel? dbConnection)
    {
        services.AddSingleton<IGrafikService, GrafikService>();
        services.AddSingleton<ILanguageService, LanguageService>();
        services.AddSingleton<ILicenseService, LicenseService>();
        services.AddSingleton<ILocalizationService, LocalizationService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<MainWindowViewModel>();

        if (dbConnection is not null)
        {
            services.AddDbContextFactory<AppDbContext>(options =>
            {
                switch (dbConnection.ProviderType)
                {
                    case ProviderType.PostgreSql:
                        options.UseNpgsql(dbConnection.ConnectionString);
                        break;
                    default:
                        options.UseSqlite(dbConnection.ConnectionString);
                        break;
                }
            });
            services.AddSingleton<IDatabaseService, DatabaseService>();
        }
    }

    private SettingsModel? ReadSettings(out List<SettingsError>? settingsErrors)
    {
        ISettingsService settingsService = _serviceProvider!.GetRequiredService<ISettingsService>();

        List<SettingsError>? errorList = null;
        SettingsModel? settings = settingsService
            .Load()
            .Match<SettingsModel?>(
                settings => settings,
                errors =>
                {
                    errorList = errors.Value;
                    return null;
                }
            );

        settingsErrors = errorList;
        return settings;
    }
}
