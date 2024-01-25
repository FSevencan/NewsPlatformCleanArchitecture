using Application.Features.ArticleTags.Commands.Create;
using Application.Features.ArticleTags.Commands.Delete;
using Application.Features.ArticleTags.Commands.Update;
using Application.Features.ArticleTags.Queries.GetById;
using Application.Features.ArticleTags.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ArticleTags.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ArticleTag, CreateArticleTagCommand>().ReverseMap();
        CreateMap<ArticleTag, CreatedArticleTagResponse>().ReverseMap();
        CreateMap<ArticleTag, UpdateArticleTagCommand>().ReverseMap();
        CreateMap<ArticleTag, UpdatedArticleTagResponse>().ReverseMap();
        CreateMap<ArticleTag, DeleteArticleTagCommand>().ReverseMap();
        CreateMap<ArticleTag, DeletedArticleTagResponse>().ReverseMap();
        CreateMap<ArticleTag, GetByIdArticleTagResponse>().ReverseMap();
        CreateMap<ArticleTag, GetListArticleTagListItemDto>().ReverseMap();
        CreateMap<IPaginate<ArticleTag>, GetListResponse<GetListArticleTagListItemDto>>().ReverseMap();
    }
}