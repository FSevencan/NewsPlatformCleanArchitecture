using Application.Features.PollVotes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.PollVotes.Rules;

public class PollVoteBusinessRules : BaseBusinessRules
{
    private readonly IPollVoteRepository _pollVoteRepository;

    public PollVoteBusinessRules(IPollVoteRepository pollVoteRepository)
    {
        _pollVoteRepository = pollVoteRepository;
    }

    public Task PollVoteShouldExistWhenSelected(PollVote? pollVote)
    {
        if (pollVote == null)
            throw new BusinessException(PollVotesBusinessMessages.PollVoteNotExists);
        return Task.CompletedTask;
    }

    public async Task PollVoteIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        PollVote? pollVote = await _pollVoteRepository.GetAsync(
            predicate: pv => pv.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PollVoteShouldExistWhenSelected(pollVote);
    }
}