using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.Categories.Queries.GetArticlesByCategory;
using Application.Features.Categories.Queries.GetCategoryTree;

namespace Application.Features.Categories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, CreatedCategoryResponse>().ReverseMap();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();
        CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
        CreateMap<Category, DeletedCategoryResponse>().ReverseMap();
        CreateMap<Category, GetByIdCategoryResponse>().ReverseMap();
        CreateMap<Category, GetCategoryDto>().ReverseMap();

        CreateMap<Category, GetListCategoryListItemDto>()
        .ForMember(dest => dest.TotalArticleCount, opt => opt.MapFrom(src => src.SubCategories.Sum(sc => sc.Articles.Count)));

        CreateMap<IPaginate<Category>, GetListResponse<GetListCategoryListItemDto>>().ReverseMap();
        CreateMap<IPaginate<Category>, GetListResponse<GetCategoryDto>>().ReverseMap();

       
    }
}