namespace BizStream.Migrations.Models;

/// <summary>
/// Model used for migrations where the import step(s) insert(s) TreeNodes.
/// </summary>
public class TreeNodeModel
{
    required public string ParentNodeAliasPath { get; set; }
    required public string Name { get; set; }
    required public string NodeAlias { get; set; }
    required public string NodeAliasPath { get; set; }
    required public string MetaTitle { get; set; }
    required public string MetaDescription { get; set; }
    required public string MetaKeywords { get; set; }
}
