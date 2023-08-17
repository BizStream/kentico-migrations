using BizStream.Migrations.Models;
using AutoMapper;
using CMS.DocumentEngine.Types.CMS;

namespace BizStream.Migrations.Mappings;
public class FolderMappingProfile : Profile
{
    public FolderMappingProfile()
    {
        CreateMap<FolderModel, Folder>()
            .ForMember( dest => dest.NodeName, opt => opt.MapFrom( model => model.Name ) )
            .ForMember( dest => dest.DocumentName, opt => opt.MapFrom( model => model.Name ) );

    }
}
