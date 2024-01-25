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

namespace Application.Features.PollVotes.Commands.Create;

public class CreatePollVoteCommand : IRequest<CreatedPollVoteResponse>/*, ISecuredRequest*/, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid PollId { get; set; }
    public Guid PollOptionId { get; set; }
    public string VoterIdentifier { get; set; }


    public string[] Roles => new[] { Admin, Write, PollVotesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPollVotes";

    public class CreatePollVoteCommandHandler : IRequestHandler<CreatePollVoteCommand, CreatedPollVoteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollVoteRepository _pollVoteRepository;
        private readonly IPollOptionRepository _pollOptionRepository;
        private readonly PollVoteBusinessRules _pollVoteBusinessRules;

        public CreatePollVoteCommandHandler(IMapper mapper, IPollVoteRepository pollVoteRepository,
                                       IPollOptionRepository pollOptionRepository, PollVoteBusinessRules pollVoteBusinessRules)
        {
            _mapper = mapper;
            _pollVoteRepository = pollVoteRepository;
            _pollOptionRepository = pollOptionRepository;
            _pollVoteBusinessRules = pollVoteBusinessRules;
        }

        public async Task<CreatedPollVoteResponse> Handle(CreatePollVoteCommand request, CancellationToken cancellationToken)
        {
            PollVote pollVote = _mapper.Map<PollVote>(request);

            await _pollVoteRepository.AddAsync(pollVote);

            // Ýlgili PollOption'ý bul
            var pollOption = await _pollOptionRepository.GetAsync(
                po => po.Id == request.PollOptionId,
                cancellationToken: cancellationToken
            );

            if (pollOption != null)
            {
                pollOption.VoteCount++;
                await _pollOptionRepository.UpdateAsync(pollOption); 
            }

            CreatedPollVoteResponse response = _mapper.Map<CreatedPollVoteResponse>(pollVote);
            return response;
        }
    }
}