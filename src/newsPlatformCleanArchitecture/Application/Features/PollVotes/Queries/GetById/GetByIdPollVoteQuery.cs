using Application.Features.PollVotes.Constants;
using Application.Features.PollVotes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.PollVotes.Constants.PollVotesOperationClaims;

namespace Application.Features.PollVotes.Queries.GetById;

public class GetByIdPollVoteQuery : IRequest<GetByIdPollVoteResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPollVoteQueryHandler : IRequestHandler<GetByIdPollVoteQuery, GetByIdPollVoteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollVoteRepository _pollVoteRepository;
        private readonly PollVoteBusinessRules _pollVoteBusinessRules;

        public GetByIdPollVoteQueryHandler(IMapper mapper, IPollVoteRepository pollVoteRepository, PollVoteBusinessRules pollVoteBusinessRules)
        {
            _mapper = mapper;
            _pollVoteRepository = pollVoteRepository;
            _pollVoteBusinessRules = pollVoteBusinessRules;
        }

        public async Task<GetByIdPollVoteResponse> Handle(GetByIdPollVoteQuery request, CancellationToken cancellationToken)
        {
            PollVote? pollVote = await _pollVoteRepository.GetAsync(predicate: pv => pv.Id == request.Id, cancellationToken: cancellationToken);
            await _pollVoteBusinessRules.PollVoteShouldExistWhenSelected(pollVote);

            GetByIdPollVoteResponse response = _mapper.Map<GetByIdPollVoteResponse>(pollVote);
            return response;
        }
    }
}