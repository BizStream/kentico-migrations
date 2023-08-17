using AutoMapper;

using BizStream.Migrations.Models;

using CMS.DocumentEngine.Types.MySite;

namespace BizStream.Migrations.Mappings;

public class ArticleMappingProfile : Profile
{
    public ArticleMappingProfile() 
    {
        CreateMap<ArticleModel, Article>();
    }
}

