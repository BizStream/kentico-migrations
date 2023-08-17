using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using AutoMapper;
using CMS.DataEngine;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace BizStream.Migrations.Repositories;
public class SettingsCategoryRepository : SqlRepository<SettingsCategoryModel>, ISettingsCategoryRepository
{
    private readonly IMapper mapper;

    public override string SqlQuery { get; } = @"SELECT DISTINCT 
	                                                 REPLACE(
		                                                REPLACE(
			                                                REPLACE(sc.CategoryName, 'CustomWebPaths', 'PathSettings'), 
		                                                'Group', ''), 
	                                                'CinBell.CustomSettings.GeneralSettings', 'altafiberSettings' ) as 'CategoryName'
	                                                ,REPLACE(CategoryDisplayName, 'Custom Web Paths', 'Path Settings') as 'CategoryDisplayName'
	                                                ,CategoryIsGroup
	                                                ,cr.ResourceID as 'CategoryResourceID'
	                                                ,1112 as CategoryParentID
                                                FROM CMS_SettingsCategory sc
                                                INNER JOIN CMS_Resource cr
	                                                ON cr.ResourceName = 'cms.customsystemmodule'
                                                WHERE CategoryName LIKE 'CinBell%'
                                                AND CategoryIsGroup = 1";

    public SettingsCategoryRepository(
        IOptions<ExportOptions> exportOptions,
        IMapper mapper )
        : base( exportOptions )
    {
        this.mapper = mapper;
    }

    public SettingsCategoryInfo Insert( SettingsCategoryModel settingsCategory )
    {
        var newSettingsCategory = new SettingsCategoryInfo();

        newSettingsCategory = mapper.Map<SettingsCategoryInfo>( settingsCategory );

        try
        {
            SettingsCategoryInfo.Provider.Set( newSettingsCategory );

            AnsiConsole.WriteLine( $"Successfully Inserted Setting Category with name: {newSettingsCategory.CategoryName}\n" );
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine( $"Failed to insert Setting Category with name: {newSettingsCategory.CategoryName} \n\t  {ex.Message} \n" );

            return SettingsCategoryInfo.Provider.Get().Where( cat => cat.CategoryName == settingsCategory.CategoryName ).FirstOrDefault()!;
        }

        return newSettingsCategory;
    }
}
