using System;
using System.Collections.Generic;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Sektionsliga.Services.Language;
using Sektionsliga.Services.Localization;
using Sektionsliga.Services.Settings;
using Sektionsliga.ViewModels.Competition;
using Sektionsliga.ViewModels.Info;
using Sektionsliga.ViewModels.Settings;

namespace Sektionsliga.ViewModels;

public class NavItem : INotifyPropertyChanged
{
    private readonly ILocalizationService _localizationService;
    private readonly string _labelKey;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Label => _localizationService[_labelKey];
    public Func<ViewModelBase> CreatePage { get; }

    public NavItem(string labelKey, Func<ViewModelBase> createPage, ILocalizationService localizationService)
    {
        _labelKey = labelKey;
        CreatePage = createPage;
        _localizationService = localizationService;
        _localizationService.LanguageChanged += (_, _) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label)));
    }
}

public partial class MainWindowViewModel : ViewModelBase
{
    public List<NavItem> CompetitionItems { get; }

    public List<NavItem> SettingsItems { get; }

    public List<NavItem> InfoItems { get; }

    [ObservableProperty]
    public partial ViewModelBase CurrentPage { get; set; }

    [ObservableProperty]
    public partial NavItem? SelectedCompetitionItem { get; set; }

    [ObservableProperty]
    public partial NavItem? SelectedSettingsItem { get; set; }

    [ObservableProperty]
    public partial NavItem? SelectedInfoItem { get; set; }

    public MainWindowViewModel(
        ISettingsService settingsService,
        ILanguageService languageService,
        ILocalizationService localizationService
    )
    {
        CompetitionItems = [new NavItem("Evaluate", () => new EvaluationViewModel(), localizationService)];
        SettingsItems =
        [
            new NavItem(
                "General",
                () => new GeneralViewModel(languageService, localizationService, settingsService),
                localizationService
            ),
            new NavItem("Database", () => new DatabaseViewModel(), localizationService),
            new NavItem("Groups", () => new GroupsViewModel(), localizationService),
        ];
        InfoItems = [new NavItem("Version", () => new VersionViewModel(), localizationService)];

        CurrentPage = new EvaluationViewModel();
        SelectedCompetitionItem = CompetitionItems[0];
    }

    partial void OnSelectedCompetitionItemChanged(NavItem? value) =>
        NavigateTo(
            value,
            () =>
            {
                SelectedSettingsItem = null;
                SelectedInfoItem = null;
            }
        );

    partial void OnSelectedSettingsItemChanged(NavItem? value) =>
        NavigateTo(
            value,
            () =>
            {
                SelectedCompetitionItem = null;
                SelectedInfoItem = null;
            }
        );

    partial void OnSelectedInfoItemChanged(NavItem? value) =>
        NavigateTo(
            value,
            () =>
            {
                SelectedCompetitionItem = null;
                SelectedSettingsItem = null;
            }
        );

    private void NavigateTo(NavItem? value, Action clearSiblings)
    {
        if (value is null)
        {
            return;
        }

        clearSiblings();
        CurrentPage = value.CreatePage();
    }
}
