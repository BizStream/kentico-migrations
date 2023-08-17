using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Attributes;
using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using BizStream.Migrations.Repositories;
using CMS.DataEngine;
using Spectre.Console;

namespace BizStream.Migrations.Migrators;

[Migrator(MigrationOptions.Settings)]
public class SettingsMigrator : IMigrator
{
    private readonly SettingsCategoryRepository settingsCategoryRepo;
    private readonly SettingsKeyRepository settingsKeyRepo;

    public SettingsMigrator(
        SettingsCategoryRepository settingsCategoryRepo,
        SettingsKeyRepository settingsKeyRepo )
    {
        this.settingsCategoryRepo = settingsCategoryRepo;
        this.settingsKeyRepo = settingsKeyRepo;
    }

    public void Migrate()
    {
        var settingsCategories = settingsCategoryRepo.RetrieveAll();
        var settingsKeys = settingsKeyRepo.RetrieveAll();

        AnsiConsole.WriteLine( $"Retrieved Settings from old site. Inserting into the new site..." );

        InsertSettings( settingsCategories, settingsKeys );
    }

    private void InsertSettings( IEnumerable<SettingsCategoryModel> settingsCategories, IEnumerable<SettingsKeyModel> settingsKeys )
    {
        foreach (var m in settingsCategories)
        {
            var settingsCategory = settingsCategoryRepo.Insert( m );
            InsertSettingsKeys( settingsKeys, settingsCategory );
        }
    }

    private void InsertSettingsKeys( IEnumerable<SettingsKeyModel> models, SettingsCategoryInfo category )
    {
        foreach (var m in models.Where( k => k.CategoryName == category.CategoryName ))
        {
            settingsKeyRepo.Insert( m, category );
        }
    }
}
