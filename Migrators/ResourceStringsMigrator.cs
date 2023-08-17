using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Attributes;
using BizStream.Migrations.Options;
using BizStream.Migrations.Repositories;
using Spectre.Console;

namespace BizStream.Migrations.Migrators;

[Migrator(MigrationOptions.ResourceStrings)]
public class ResourceStringsMigrator : IMigrator
{
    private readonly ResourceStringRepository resourceStringRepo;

    public ResourceStringsMigrator(ResourceStringRepository resourceStringRepo)
        => this.resourceStringRepo = resourceStringRepo;

    public void Migrate()
    {
        var resourceStrings = resourceStringRepo.RetrieveAll();

        AnsiConsole.WriteLine( $"Retrieved Resource String models from old site. Inserting into the new site..." );

        foreach (var m in resourceStrings)
        {
            resourceStringRepo.Insert( m );
        }
    }
}
