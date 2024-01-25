using Application.Features.PollVotes.Constants;
using Application.Features.PollVotes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.PollVotes.Constants.PollVotesOperationClaims;

namespace Application.Features.PollVotes.Commands.Update;

public class UpdatePollVoteCommand : IRequest<UpdatedPollVoteResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public Guid PollOptionId { get; set; }
    public string VoterIdentifier { get; set; }
  

    public string[] Roles => new[] { Admin, Write, PollVotesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPollVotes";

    public class UpdatePollVoteCommandHandler : IRequestHandler<UpdatePollVoteCommand, UpdatedPollVoteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollVoteRepository _pollVoteRepository;
        private readonly PollVoteBusinessRules _pollVoteBusinessRules;

        public UpdatePollVoteCommandHandler(IMapper mapper, IPollVoteRepository pollVoteRepository,
                                         PollVoteBusinessRules pollVoteBusinessRules)
        {
            _mapper = mapper;
            _pollVoteRepository = pollVoteRepository;
            _pollVoteBusinessRules = pollVoteBusinessRules;
        }

        public async Task<UpdatedPollVoteResponse> Handle(UpdatePollVoteCommand request, CancellationToken cancellationToken)
        {
            PollVote? pollVote = await _pollVoteRepository.GetAsync(predicate: pv => pv.Id == request.Id, cancellationToken: cancellationToken);
            await _pollVoteBusinessRules.PollVoteShouldExistWhenSelected(pollVote);
            pollVote = _mapper.Map(request, pollVote);

            await _pollVoteRepository.UpdateAsync(pollVote!);

            UpdatedPollVoteResponse response = _mapper.Map<UpdatedPollVoteResponse>(pollVote);
            return response;
        }
    }
}