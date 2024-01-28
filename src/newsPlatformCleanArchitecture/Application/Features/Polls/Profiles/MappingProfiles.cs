using Application.Features.Polls.Commands.Create;
using Application.Features.Polls.Commands.Delete;
using Application.Features.Polls.Commands.Update;
using Application.Features.Polls.Queries.GetById;
using Application.Features.Polls.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.Polls.Queries.GetLatestPoll;

namespace Application.Features.Polls.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Poll, CreatePollCommand>().ReverseMap();
        CreateMap<Poll, CreatedPollResponse>().ReverseMap();
        CreateMap<Poll, UpdatePollCommand>().ReverseMap();
        CreateMap<Poll, UpdatedPollResponse>().ReverseMap();
        CreateMap<Poll, DeletePollCommand>().ReverseMap();
        CreateMap<Poll, DeletedPollResponse>().ReverseMap();
        CreateMap<Poll, GetByIdPollResponse>().ReverseMap();
        CreateMap<Poll, GetListPollListItemDto>().ReverseMap();

        CreateMap<Poll, GetLatestPollResponse>().ReverseMap();
       
        CreateMap<IPaginate<Poll>, GetListResponse<GetListPollListItemDto>>().ReverseMap();
    }
}