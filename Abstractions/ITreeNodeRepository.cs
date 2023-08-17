using BizStream.Migrations.Models;
using CMS.DocumentEngine;

namespace BizStream.Migrations.Abstractions;

/// <summary>
/// Repository interface used only when inserting <see cref="TreeNode"/>'s into the Kentico content tree.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <typeparam name="TNode"></typeparam>
public interface ITreeNodeRepository<TModel, TNode>
    where TModel : TreeNodeModel
    where TNode : TreeNode
{
    public void Insert( TModel model, TNode node );
}
