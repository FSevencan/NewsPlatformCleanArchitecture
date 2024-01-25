using Application.Features.Subscribles.Commands.Create;
using Application.Features.Subscribles.Commands.Delete;
using Application.Features.Subscribles.Commands.Update;
using Application.Features.Subscribles.Queries.GetById;
using Application.Features.Subscribles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Subscribles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Subscrible, CreateSubscribleCommand>().ReverseMap();
        CreateMap<Subscrible, CreatedSubscribleResponse>().ReverseMap();
        CreateMap<Subscrible, UpdateSubscribleCommand>().ReverseMap();
        CreateMap<Subscrible, UpdatedSubscribleResponse>().ReverseMap();
        CreateMap<Subscrible, DeleteSubscribleCommand>().ReverseMap();
        CreateMap<Subscrible, DeletedSubscribleResponse>().ReverseMap();
        CreateMap<Subscrible, GetByIdSubscribleResponse>().ReverseMap();
        CreateMap<Subscrible, GetListSubscribleListItemDto>().ReverseMap();
        CreateMap<IPaginate<Subscrible>, GetListResponse<GetListSubscribleListItemDto>>().ReverseMap();
    }
}