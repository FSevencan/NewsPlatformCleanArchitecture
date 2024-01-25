using Application.Features.PollVotes.Commands.Create;
using Application.Features.PollVotes.Commands.Delete;
using Application.Features.PollVotes.Commands.Update;
using Application.Features.PollVotes.Queries.GetById;
using Application.Features.PollVotes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.PollVotes.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PollVote, CreatePollVoteCommand>().ReverseMap();
        CreateMap<PollVote, CreatedPollVoteResponse>().ReverseMap();
        CreateMap<PollVote, UpdatePollVoteCommand>().ReverseMap();
        CreateMap<PollVote, UpdatedPollVoteResponse>().ReverseMap();
        CreateMap<PollVote, DeletePollVoteCommand>().ReverseMap();
        CreateMap<PollVote, DeletedPollVoteResponse>().ReverseMap();
        CreateMap<PollVote, GetByIdPollVoteResponse>().ReverseMap();
        CreateMap<PollVote, GetListPollVoteListItemDto>().ReverseMap();
        CreateMap<IPaginate<PollVote>, GetListResponse<GetListPollVoteListItemDto>>().ReverseMap();
    }
}