using AutoMapper;

using BizStream.Migrations.Models;

using CMS.DocumentEngine;

namespace BizStream.Migrations.Mappings;

public class TreeNodeMappingProfile : Profile
{
    public TreeNodeMappingProfile()
    {
        CreateMap<TreeNodeModel, TreeNode>();
    }
}

