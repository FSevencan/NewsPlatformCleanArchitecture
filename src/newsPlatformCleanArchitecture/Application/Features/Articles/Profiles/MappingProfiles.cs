using Application.Features.Articles.Commands.Create;
using Application.Features.Articles.Commands.Delete;
using Application.Features.Articles.Commands.Update;
using Application.Features.Articles.Queries.GetById;
using Application.Features.Articles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.Articles.Queries.GetLatestArticlesByCategory;
using Application.Features.Categories.Queries.GetArticlesByCategory;

namespace Application.Features.Articles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Article, CreateArticleCommand>().ReverseMap();
        CreateMap<Article, CreatedArticleResponse>().ReverseMap();
        CreateMap<Article, UpdateArticleCommand>().ReverseMap();
        CreateMap<Article, UpdatedArticleResponse>().ReverseMap();
        CreateMap<Article, DeleteArticleCommand>().ReverseMap();
        CreateMap<Article, DeletedArticleResponse>().ReverseMap();
        CreateMap<Article, GetArticleBySlugResponse>()
       .ForMember(dest => dest.Tags, opt => opt.MapFrom(t => t.ArticleTags.Select(tags => tags.Tag).ToList()));

        CreateMap<Article, GetLastArticleByCategoryItemDto>().ReverseMap();

        CreateMap<Article, GetArticleByCategoryListDto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
             .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name))
             .ForMember(dest => dest.FeaturedImage, opt => opt.MapFrom(src => src.FeaturedImage))
             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
             
             ;

        CreateMap<Article, GetListArticleListItemDto>().ReverseMap();

        CreateMap<IPaginate<Article>, GetListResponse<GetListArticleListItemDto>>().ReverseMap();

    }
}

