using AutoMapper;

using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Options;

using Microsoft.Extensions.Options;

namespace BizStream.Migrations.Repositories;

public class ArticleFolderRepository : FolderRepository
{
    protected override string SqlQuery => @"SELECT 
	                                             t.NodeName
	                                            ,t.NodeAlias
	                                            ,'/Articles/' + NodeAlias as 'NodeAliasPath'
	                                            ,'/Articles' as 'ParentNodeAliasPath'
                                            FROM View_CMS_Tree_Joined t
                                            WHERE ClassName = 'CMS.Folder'
                                            AND NodeAliasPath LIKE '/TEST-Articles/%'";
    public ArticleFolderRepository(IOptions<ExportOptions> exportOptions, IMapper mapper) 
        : base(exportOptions, mapper)
    {
    }
}
