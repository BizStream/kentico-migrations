using BizStream.Migrations.Models;
using BizStream.Migrations.Options;
using AutoMapper;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.CMS;
using Microsoft.Extensions.Options;

namespace BizStream.Migrations.Abstractions;
public abstract class FolderRepository : SqlToTreeNodeRepository<FolderModel, Folder>
{
    public FolderRepository( IOptions<ExportOptions> exportOptions, IMapper mapper ) 
        : base( exportOptions, mapper )
    {
    }
}
