using Application.Features.PollVotes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PollVotes;

public class PollVotesManager : IPollVotesService
{
    private readonly IPollVoteRepository _pollVoteRepository;
    private readonly PollVoteBusinessRules _pollVoteBusinessRules;

    public PollVotesManager(IPollVoteRepository pollVoteRepository, PollVoteBusinessRules pollVoteBusinessRules)
    {
        _pollVoteRepository = pollVoteRepository;
        _pollVoteBusinessRules = pollVoteBusinessRules;
    }

    public async Task<PollVote?> GetAsync(
        Expression<Func<PollVote, bool>> predicate,
        Func<IQueryable<PollVote>, IIncludableQueryable<PollVote, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        PollVote? pollVote = await _pollVoteRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return pollVote;
    }

    public async Task<IPaginate<PollVote>?> GetListAsync(
        Expression<Func<PollVote, bool>>? predicate = null,
        Func<IQueryable<PollVote>, IOrderedQueryable<PollVote>>? orderBy = null,
        Func<IQueryable<PollVote>, IIncludableQueryable<PollVote, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<PollVote> pollVoteList = await _pollVoteRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return pollVoteList;
    }

    public async Task<PollVote> AddAsync(PollVote pollVote)
    {
        PollVote addedPollVote = await _pollVoteRepository.AddAsync(pollVote);

        return addedPollVote;
    }

    public async Task<PollVote> UpdateAsync(PollVote pollVote)
    {
        PollVote updatedPollVote = await _pollVoteRepository.UpdateAsync(pollVote);

        return updatedPollVote;
    }

    public async Task<PollVote> DeleteAsync(PollVote pollVote, bool permanent = false)
    {
        PollVote deletedPollVote = await _pollVoteRepository.DeleteAsync(pollVote);

        return deletedPollVote;
    }
}
