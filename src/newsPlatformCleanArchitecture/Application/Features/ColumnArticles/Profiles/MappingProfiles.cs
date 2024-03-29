using Application.Features.ColumnArticles.Commands.Create;
using Application.Features.ColumnArticles.Commands.Delete;
using Application.Features.ColumnArticles.Commands.Update;
using Application.Features.ColumnArticles.Queries.GetById;
using Application.Features.ColumnArticles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.ColumnArticles.Queries.GetBySlug;

namespace Application.Features.ColumnArticles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ColumnArticle, CreateColumnArticleCommand>().ReverseMap();
        CreateMap<ColumnArticle, CreatedColumnArticleResponse>().ReverseMap();
        CreateMap<ColumnArticle, UpdateColumnArticleCommand>().ReverseMap();
        CreateMap<ColumnArticle, UpdatedColumnArticleResponse>().ReverseMap();
        CreateMap<ColumnArticle, DeleteColumnArticleCommand>().ReverseMap();
        CreateMap<ColumnArticle, DeletedColumnArticleResponse>().ReverseMap();
        CreateMap<ColumnArticle, GetByIdColumnArticleResponse>().ReverseMap();
        CreateMap<ColumnArticle, GetColumnArticleBySlugResponse>().ReverseMap();

        CreateMap<ColumnArticle, GetListColumnArticleListItemDto>()
       .ForMember(dest => dest.ColumnistName, opt => opt.MapFrom(src => src.Columnist.Name))
       .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<IPaginate<ColumnArticle>, GetListResponse<GetListColumnArticleListItemDto>>().ReverseMap();
    }
}