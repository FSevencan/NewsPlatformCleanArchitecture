using Application.Features.Advertisements.Commands.Create;
using Application.Features.Advertisements.Commands.Delete;
using Application.Features.Advertisements.Commands.Update;
using Application.Features.Advertisements.Queries.GetById;
using Application.Features.Advertisements.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Advertisements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Advertisement, CreateAdvertisementCommand>().ReverseMap();
        CreateMap<Advertisement, CreatedAdvertisementResponse>().ReverseMap();
        CreateMap<Advertisement, UpdateAdvertisementCommand>().ReverseMap();
        CreateMap<Advertisement, UpdatedAdvertisementResponse>().ReverseMap();
        CreateMap<Advertisement, DeleteAdvertisementCommand>().ReverseMap();
        CreateMap<Advertisement, DeletedAdvertisementResponse>().ReverseMap();
        CreateMap<Advertisement, GetByIdAdvertisementResponse>().ReverseMap();
        CreateMap<Advertisement, GetListAdvertisementListItemDto>().ReverseMap();
        CreateMap<IPaginate<Advertisement>, GetListResponse<GetListAdvertisementListItemDto>>().ReverseMap();
    }
}