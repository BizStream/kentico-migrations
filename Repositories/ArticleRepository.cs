using AutoMapper;

using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Models;
using BizStream.Migrations.Options;

using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.MySite;

using Microsoft.Extensions.Options;

namespace BizStream.Migrations.Repositories;

public class ArticleRepository : SqlToTreeNodeRepository<ArticleModel, Article>
{
    protected override string SqlQuery => @"SELECT 
												 t.NodeName
												,t.NodeAlias
												,REPLACE(t.NodeAliasPath, 'TEST-Articles', 'Articles' ) as 'NodeAliasPath'
												,REPLACE(p.NodeAliasPath, 'TEST-Articles', 'Articles' ) as 'ParentNodeAliasPath'
												,a.ArticleTeaserImage
												,a.ArticleTeaserText
												,a.ArticleText
											FROM View_CMS_Tree_Joined t
											INNER JOIN View_CMS_Tree_Joined p
											ON t.NodeParentID = p.NodeID
											INNER JOIN CONTENT_Article a
											ON a.ArticleID = t.DocumentForeignKeyValue
											WHERE t.ClassName = 'CMS.Article'";

    public ArticleRepository(IOptions<ExportOptions> exportOptions, IMapper mapper)
        : base(exportOptions, mapper)
    {
    }
}

