namespace BizStream.Migrations.Models;

/// <summary>
/// Model used for migrations where the import step(s) insert(s) TreeNodes.
/// </summary>
public class TreeNodeModel
{
    required public string ParentNodeAliasPath { get; set; }
    required public string NodeName { get; set; }
    required public string NodeAlias { get; set; }
    required public string NodeAliasPath { get; set; }
}
