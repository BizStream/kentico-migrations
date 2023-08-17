using AutoMapper;

using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Models;
using BizStream.Migrations.Options;

using CMS.Localization;

using Microsoft.Extensions.Options;

using Spectre.Console;

namespace BizStream.Migrations.Repositories;
public class ResourceStringRepository : SqlRepository<ResourceStringModel>, IResourceStringRepository
{
    private readonly IMapper mapper;

    public override string SqlQuery { get; } = @"
                                                SELECT
                                                     rs.StringKey
	                                                ,rs.StringIsCustom
	                                                ,rt.TranslationText
	                                                ,c.CultureCode
                                                FROM CMS_ResourceString rs
                                                INNER JOIN CMS_ResourceTranslation rt
                                                ON rs.StringID = rt.TranslationStringID
                                                INNER JOIN CMS_Culture c
                                                ON rt.TranslationCultureID = c.CultureID";

    public ResourceStringRepository(
        IOptions<ExportOptions> exportOptions,
        IMapper mapper )
        : base( exportOptions )
    {
        this.mapper = mapper;
    }

    public void Insert( ResourceStringModel resourceString )
    {
        var newResString = new ResourceStringInfo();

        newResString = mapper.Map<ResourceStringInfo>( resourceString );

        newResString.Insert();

        AnsiConsole.WriteLine( $"Successfully Inserted Resource String with key: {newResString.StringKey}" );
    }
}
