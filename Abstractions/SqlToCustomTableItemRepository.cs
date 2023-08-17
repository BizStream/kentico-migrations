using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using AutoMapper;
using CMS.CustomTables;
using Microsoft.Extensions.Options;

namespace BizStream.Migrations.Abstractions;

/// <summary>
/// Repository used for migrations that export <see cref="CustomTableItem"> fields from a SQL command, and insert
/// new <see cref="CustomTableItem"/>'s with data from those fields.
/// </summary>
/// <typeparam name="TModel">A model used with Dapper for typing the results of a SQL query.</typeparam>
/// <typeparam name="TNode">The child model of a Kentico <see cref="CustomTableItem"/> being inserted into the content tree.</typeparam>
public abstract class SqlToCustomTableItemRepository<TModel, TKenticoItem> : SqlToKenticoItemRepository<TModel, TKenticoItem>
    where TModel : KenticoItemModel
    where TKenticoItem : CustomTableItem, new()
{
    public SqlToCustomTableItemRepository(
        IOptions<ExportOptions> exportOptions,
        IMapper mapper )
        : base( exportOptions, mapper )
    {
    }
}
