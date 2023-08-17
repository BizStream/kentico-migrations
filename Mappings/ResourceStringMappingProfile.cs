using BizStream.Migrations.Models;
using AutoMapper;
using CMS.Localization;

namespace BizStream.Migrations.Mappings;
public class ResourceStringMappingProfile: Profile
{
    public ResourceStringMappingProfile() 
    {
        CreateMap<ResourceStringModel, ResourceStringInfo>();
    }   
}
