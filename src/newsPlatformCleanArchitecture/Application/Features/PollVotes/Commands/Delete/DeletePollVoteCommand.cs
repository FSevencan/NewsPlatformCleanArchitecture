using Application.Features.PollVotes.Constants;
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

namespace Application.Features.PollVotes.Commands.Delete;

public class DeletePollVoteCommand : IRequest<DeletedPollVoteResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PollVotesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPollVotes";

    public class DeletePollVoteCommandHandler : IRequestHandler<DeletePollVoteCommand, DeletedPollVoteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPollVoteRepository _pollVoteRepository;
        private readonly PollVoteBusinessRules _pollVoteBusinessRules;

        public DeletePollVoteCommandHandler(IMapper mapper, IPollVoteRepository pollVoteRepository,
                                         PollVoteBusinessRules pollVoteBusinessRules)
        {
            _mapper = mapper;
            _pollVoteRepository = pollVoteRepository;
            _pollVoteBusinessRules = pollVoteBusinessRules;
        }

        public async Task<DeletedPollVoteResponse> Handle(DeletePollVoteCommand request, CancellationToken cancellationToken)
        {
            PollVote? pollVote = await _pollVoteRepository.GetAsync(predicate: pv => pv.Id == request.Id, cancellationToken: cancellationToken);
            await _pollVoteBusinessRules.PollVoteShouldExistWhenSelected(pollVote);

            await _pollVoteRepository.DeleteAsync(pollVote!);

            DeletedPollVoteResponse response = _mapper.Map<DeletedPollVoteResponse>(pollVote);
            return response;
        }
    }
}