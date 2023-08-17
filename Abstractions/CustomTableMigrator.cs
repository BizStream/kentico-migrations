using System.Reflection;
using BizStream.Migrations.Models;
using CMS.CustomTables;
using Spectre.Console;

namespace BizStream.Migrations.Abstractions;
public class CustomTableMigrator<TRepo, TModel, TCustomTableItem> : KenticoTableMigrator<TRepo, TModel, TCustomTableItem>
    where TRepo : SqlToCustomTableItemRepository<TModel, TCustomTableItem>
    where TModel : KenticoItemModel
    where TCustomTableItem : CustomTableItem, new()
{
    public CustomTableMigrator(TRepo repo)
        : base(repo)
    {
    }

    public override void PreMigrate()
    {
        var classNameField = typeof( TCustomTableItem ).GetField( "CLASS_NAME", BindingFlags.Public | BindingFlags.Static );
        var className = classNameField?.GetValue( new TCustomTableItem() )?.ToString() ?? string.Empty;

        if ( className != string.Empty )
        {
            AnsiConsole.WriteLine($"Clearing records from {className}");
            CustomTableItemProvider.DeleteItems( className );
        }
        else
        {
            AnsiConsole.WriteLine( $"Invalid class name {className} for {typeof( TCustomTableItem ).Name}" );
        }
    }
}
