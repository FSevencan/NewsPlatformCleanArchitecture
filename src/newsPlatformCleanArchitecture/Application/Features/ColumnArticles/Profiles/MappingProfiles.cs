using Application.Features.ColumnArticles.Commands.Create;
using Application.Features.ColumnArticles.Commands.Delete;
using Application.Features.ColumnArticles.Commands.Update;
using Application.Features.ColumnArticles.Queries.GetById;
using Application.Features.ColumnArticles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

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
        CreateMap<ColumnArticle, GetListColumnArticleListItemDto>().ReverseMap();
        CreateMap<IPaginate<ColumnArticle>, GetListResponse<GetListColumnArticleListItemDto>>().ReverseMap();
    }
}