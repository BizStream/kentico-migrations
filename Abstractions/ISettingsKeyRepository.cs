using BizStream.Migrations.Models;
using CMS.DataEngine;

namespace BizStream.Migrations.Abstractions;
public interface ISettingsKeyRepository
{
    public void Insert( SettingsKeyModel settingsKey, SettingsCategoryInfo parentCategory );
}
