using System.Collections.Generic;
using Result;
using Sektionsliga.Models;

namespace Sektionsliga.Services.Settings;

internal interface ISettingsService
{
    string FolderPath { get; }

    void Delete();

    Result<SettingsModel, List<SettingsError>> Load();

    void Save(SettingsModel? settings);
}
