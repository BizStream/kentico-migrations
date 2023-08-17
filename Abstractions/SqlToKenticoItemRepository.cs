using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using AutoMapper;
using CMS.DataEngine;
using CMS.Ecommerce;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace BizStream.Migrations.Abstractions;

/// <summary>
/// Repository used for migrations that export <see cref="IInfo"> fields from a SQL command, and insert
/// new <see cref="IInfo"/>'s with data from those fields.
/// </summary>
/// <typeparam name="TModel">A model used with Dapper for typing the results of a SQL query.</typeparam>
/// <typeparam name="TKenticoItem">The model of a Kentico object being inserted into the content tree, custom table or other Kentico data table.</typeparam>
public abstract class SqlToKenticoItemRepository<TModel, TKenticoItem> : SqlRepository<TModel>, IKenticoItemRepository<TModel, TKenticoItem>
    where TModel : KenticoItemModel
    where TKenticoItem : IInfo
{
    private readonly IMapper mapper;

    public SqlToKenticoItemRepository(
        IOptions<ExportOptions> exportOptions,
        IMapper mapper
        )
        : base( exportOptions )
    {
        this.mapper = mapper;
    }

    /// <summary>
    /// Inserts the passed-in <see cref="TKenticoItem"/> into the content, with overlapping fields mapped from the model.
    /// </summary>
    /// <param name="model">The model from which the inserted node/object pulls its properties.</param>
    public virtual void Insert( TModel model )
    {
        var item = mapper.Map<TKenticoItem>( model );

        try
        {
            item.Insert();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine( $"Encountered an error while inserting item \"{item.TypeInfo.ObjectClassName}\" \n\n {ex}" );
        }
    }
}
