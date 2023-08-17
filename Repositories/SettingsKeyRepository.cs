using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using AutoMapper;
using CMS.DataEngine;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace BizStream.Migrations.Repositories;
public class SettingsKeyRepository : SqlRepository<SettingsKeyModel>, ISettingsKeyRepository
{
    private readonly IMapper mapper;

    public override string SqlQuery { get; } = @"SELECT DISTINCT 
	                                                 REPLACE(sk.KeyName, 'CinBell', 'altafiber') as 'KeyName'
	                                                ,sk.KeyDisplayName
	                                                ,sk.KeyValue
                                                    ,sk.KeyType
                                                    ,sk.KeyDescription
                                                    ,sk.KeyExplanationText
	                                                ,REPLACE(
		                                                REPLACE(
			                                                REPLACE(sc.CategoryName, 'CustomWebPaths', 'PathSettings'), 
		                                                'Group', ''), 
	                                                'CinBell.CustomSettings.GeneralSettings', 'altafiberSettings' ) as 'CategoryName'
                                                FROM CMS_SettingsCategory sc
                                                INNER JOIN CMS_SettingsKey sk
                                                ON sk.KeyCategoryID = sc.CategoryID
                                                WHERE CategoryName LIKE 'CinBell%'";

    public SettingsKeyRepository(
        IOptions<ExportOptions> exportOptions,
        IMapper mapper )
        : base( exportOptions )
    {
        this.mapper = mapper;
    }

    public void Insert( SettingsKeyModel settingsKey, SettingsCategoryInfo parentCategory )
    {
        var newSetting = mapper.Map<SettingsKeyInfo>( settingsKey );

        newSetting.KeyCategoryID = parentCategory.CategoryID;

        try
        {
            newSetting.Insert();
            AnsiConsole.WriteLine( $"Successfully Inserted Setting with key: {newSetting.KeyName}\n" );
        }
        catch( Exception ex )
        {
            AnsiConsole.WriteLine( $"Failed to insert Setting with key: {newSetting.KeyName} \n\t {ex.Message} \n" );
        }
    }
}
