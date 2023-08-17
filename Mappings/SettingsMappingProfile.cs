using BizStream.Migrations.Models;
using AutoMapper;
using CMS.DataEngine;

namespace BizStream.Migrations.Mappings;
public class SettingsMappingProfile : Profile
{
    public SettingsMappingProfile()
    {
        CreateMap<SettingsCategoryModel, SettingsCategoryInfo>()
            .ForMember( info => info.CategoryIsCustom, opt => opt.MapFrom( model => true ) );

        CreateMap<SettingsKeyModel, SettingsKeyInfo>()
            .ForMember( info => info.KeyIsCustom, opt => opt.MapFrom( model => true ) );
    }
}
