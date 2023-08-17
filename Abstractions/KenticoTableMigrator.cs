using BizStream.Migrations.Models;
using CMS.DataEngine;
using Spectre.Console;

namespace BizStream.Migrations.Abstractions;

/// <summary>
/// Abstract base instance of an <see cref="IMigrator"/> that handles rendering progress to the console
/// </summary>
/// <typeparam name="TRepo">Custom Table Repository type</typeparam>
/// <typeparam name="TModel">Model mapped from the source DB</typeparam>
/// <typeparam name="TKenticoItem">Destination custom table item type</typeparam>
public abstract class KenticoTableMigrator<TRepo, TModel, TKenticoItem> : IMigrator
    where TRepo : SqlToKenticoItemRepository<TModel, TKenticoItem>
    where TModel : KenticoItemModel
    where TKenticoItem : IInfo
{
    protected readonly TRepo repo;

    public KenticoTableMigrator(TRepo repo)
        => this.repo = repo;

    /// <summary>
    /// Runs pre-migrate task(s). This can be implemented in child classes.
    /// </summary>
    public virtual void PreMigrate()
    {
    }

    public virtual void Migrate()
    {
        PreMigrate();

        var items = repo.RetrieveAll();

        AnsiConsole.Progress()
            .Columns(new ProgressColumn[] 
            {
                new TaskDescriptionColumn(),    // Task description
                new ProgressBarColumn(),        // Progress bar
                new PercentageColumn(),         // Percentage
                new RemainingTimeColumn(),      // Remaining time
                new SpinnerColumn(),            // Spinner
            })
            .Start(ctx =>
            {
                var task = ctx.AddTask( $"Migrating {typeof(TKenticoItem).Name} records", maxValue: items.Count() );

                foreach (var m in items)
                {
                    repo.Insert( m );
                    task.Increment( 1 );
                }
            } );
    }
}
