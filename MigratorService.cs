using System.Reflection;
using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Attributes;
using BizStream.Migrations.Helpers;
using CMS.DataEngine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

namespace BizStream.Migrations;
public class MigratorService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Background Service that handles both exporting/importing during migrations. Modify the `ExecuteAsync`
    /// (or underlying private functions) to change migration behavior.
    /// </summary>
    /// <param name="serviceProvider">The service provider, used to explicity retrieve the requested migrator from the service collection</param>
    public MigratorService( IServiceProvider serviceProvider )
        => this.serviceProvider = serviceProvider;


    /// <summary>
    /// Migrates models from migration source to destination.
    /// </summary>
    public void Migrate()
    {
        var selection = TerminalHelper.PromptMigrationCategorySelection();
        var migrators = selection.Select( GetRequestedMigrator )
            .Where(migrator => migrator is not null)
            .ToList();

        if (migrators.Count == 0)
        {
            AnsiConsole.WriteLine( $"No valid selections for migration." );
            return;
        }

        migrators.ForEach( m => m?.Migrate() );

        AnsiConsole.WriteLine( $"\n\n********* Import to new site completed. *********\n\n" );
    }

    private IMigrator? GetRequestedMigrator(string name)
    {
        var requestedMigratorType = GetType().Assembly.GetTypes()
            .Where( t => typeof( IMigrator ).IsAssignableFrom( t ) )
            .FirstOrDefault( t => t.GetCustomAttribute<MigratorAttribute>()?.MigratorName == name );

        if (requestedMigratorType == null )
        {
            return null;
        }

        return serviceProvider.GetRequiredService( requestedMigratorType ) as IMigrator;
    }

    protected override Task ExecuteAsync( CancellationToken stoppingToken )
    {
        CMSApplication.Init();

        try
        {
            Migrate();
        }
        catch (Exception ex)
        {
            // TODO: This stops the application completely, figure out how to make it not do that.
            AnsiConsole.WriteLine( $"Something failed during Migration:\n{ex.Message}" );
        }

        if (stoppingToken.IsCancellationRequested)
        {
            return Task.FromCanceled( stoppingToken );
        }

        return Task.CompletedTask;
    }
}
