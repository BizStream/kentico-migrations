using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using AutoMapper;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Routing;
using CMS.DocumentEngine.Routing.Internal;
using Microsoft.Extensions.Options;
using static Spectre.Console.AnsiConsole;

namespace BizStream.Migrations.Abstractions;

/// <summary>
/// Repository used for migrations that export <see cref="TreeNode"> fields from a SQL command, and insert
/// new <see cref="TreeNode"/>'s with data from those fields.
/// </summary>
/// <typeparam name="TModel">A model used with Dapper for typing the results of a SQL query.</typeparam>
/// <typeparam name="TNode">The child model of a Kentico <see cref="TreeNode"/> being inserted into the content tree.</typeparam>
public abstract class SqlToTreeNodeRepository<TModel, TNode> : SqlRepository<TModel>, ITreeNodeRepository<TModel, TNode>
    where TModel : TreeNodeModel
    where TNode : TreeNode
{
    private readonly IMapper mapper;

    public SqlToTreeNodeRepository(
        IOptions<ExportOptions> exportOptions,
        IMapper mapper
        )
        : base( exportOptions )
    {
        this.mapper = mapper;
    }


    /// <summary>
    /// Inserts the passed-in <see cref="TreeNode"/> into the content, with overlapping fields mapped from the model.
    /// </summary>
    /// <param name="model">The model from which the inserted node pulls its properties.</param>
    /// <param name="node">The TreeNode which will be inserted.</param>
    public virtual void Insert( TModel model, TNode node )
    {
        var parent = DocumentHelper.GetDocuments().WhereEquals( nameof( TreeNode.NodeAliasPath ), model.ParentNodeAliasPath ).FirstOrDefault();

        if (parent != null)
        {
            node = mapper.Map<TNode>( model );

            try
            {
                node.Insert( parent );

                UpdateUrlSlug( node );

                WriteLine( $"Successfully Inserted Node: {node.NodeName} under Parent: {node.Parent.NodeName}" );
            }
            catch (Exception ex)
            {
                WriteLine( $"Encountered an error while inserting node \"{node.NodeName}\" \n\n {ex}" );
            }
        } 
        else
        {
            WriteLine( $"Parent not found for {node.NodeName}" );
        }
    }


    /// <summary>
    /// Updates the URL Slug to the NodeAlias of the passed-in <see cref="TreeNode">.
    /// This is only done when there is a mismatch between NodeAlias and UrlSlug of any given node upon insertion.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> of which we are updating the URL slug.</param>
    protected void UpdateUrlSlug( TreeNode node )
    {
        if (node.HasUrl() && node.NodeAlias.ToLowerInvariant() != node.GetPageUrlPath().Slug.ToLowerInvariant())
        {
            PageUrlPathSlugUpdater pageUrlPathSlugUpdater = new PageUrlPathSlugUpdater( node, node.DocumentCulture );

            pageUrlPathSlugUpdater.TryUpdate( node.NodeAlias, out var collidingPaths );

            if (collidingPaths.Count() == 0)
            {
               WriteLine( $"**** Updated slug URL to NodeAliasPath for node: {node.NodeName}" );
            }
            else
            {
               WriteLine( $"Failed to update slug URL to NodeAlias for node: {node.NodeName} -- URL slug already in use." );
            }
        }
    }
}
