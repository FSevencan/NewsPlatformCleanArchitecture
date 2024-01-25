using Application.Features.PollOptions.Commands.Create;
using Application.Features.PollOptions.Commands.Delete;
using Application.Features.PollOptions.Commands.Update;
using Application.Features.PollOptions.Queries.GetById;
using Application.Features.PollOptions.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.PollOptions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PollOption, CreatePollOptionCommand>().ReverseMap();
        CreateMap<PollOption, CreatedPollOptionResponse>().ReverseMap();
        CreateMap<PollOption, UpdatePollOptionCommand>().ReverseMap();
        CreateMap<PollOption, UpdatedPollOptionResponse>().ReverseMap();
        CreateMap<PollOption, DeletePollOptionCommand>().ReverseMap();
        CreateMap<PollOption, DeletedPollOptionResponse>().ReverseMap();
        CreateMap<PollOption, GetByIdPollOptionResponse>().ReverseMap();
        CreateMap<PollOption, GetListPollOptionListItemDto>().ReverseMap();
        CreateMap<PollOption, PollOptionListDto>().ReverseMap();


        CreateMap<IPaginate<PollOption>, GetListResponse<GetListPollOptionListItemDto>>().ReverseMap();
    }
}