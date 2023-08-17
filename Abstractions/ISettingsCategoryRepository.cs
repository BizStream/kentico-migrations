using BizStream.Migrations.Models;
using CMS.DataEngine;

namespace BizStream.Migrations.Abstractions;
public interface ISettingsCategoryRepository
{
    public SettingsCategoryInfo Insert( SettingsCategoryModel settingsCategory );
}
