using Application.Features.PollVotes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.PollVotes.Constants.PollVotesOperationClaims;

namespace Application.Features.PollVotes.Queries.GetList;

public class GetListPollVoteQuery : IRequest<GetListResponse<GetListPollVoteListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPollVotes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPollVotes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPollVoteQueryHandler : IRequestHandler<GetListPollVoteQuery, GetListResponse<GetListPollVoteListItemDto>>
    {
        private readonly IPollVoteRepository _pollVoteRepository;
        private readonly IMapper _mapper;

        public GetListPollVoteQueryHandler(IPollVoteRepository pollVoteRepository, IMapper mapper)
        {
            _pollVoteRepository = pollVoteRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPollVoteListItemDto>> Handle(GetListPollVoteQuery request, CancellationToken cancellationToken)
        {
            IPaginate<PollVote> pollVotes = await _pollVoteRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPollVoteListItemDto> response = _mapper.Map<GetListResponse<GetListPollVoteListItemDto>>(pollVotes);
            return response;
        }
    }
}