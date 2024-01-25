using Application.Features.Polls.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Polls;

public class PollsManager : IPollsService
{
    private readonly IPollRepository _pollRepository;
    private readonly PollBusinessRules _pollBusinessRules;

    public PollsManager(IPollRepository pollRepository, PollBusinessRules pollBusinessRules)
    {
        _pollRepository = pollRepository;
        _pollBusinessRules = pollBusinessRules;
    }

    public async Task<Poll?> GetAsync(
        Expression<Func<Poll, bool>> predicate,
        Func<IQueryable<Poll>, IIncludableQueryable<Poll, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Poll? poll = await _pollRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return poll;
    }

    public async Task<IPaginate<Poll>?> GetListAsync(
        Expression<Func<Poll, bool>>? predicate = null,
        Func<IQueryable<Poll>, IOrderedQueryable<Poll>>? orderBy = null,
        Func<IQueryable<Poll>, IIncludableQueryable<Poll, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Poll> pollList = await _pollRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return pollList;
    }

    public async Task<Poll> AddAsync(Poll poll)
    {
        Poll addedPoll = await _pollRepository.AddAsync(poll);

        return addedPoll;
    }

    public async Task<Poll> UpdateAsync(Poll poll)
    {
        Poll updatedPoll = await _pollRepository.UpdateAsync(poll);

        return updatedPoll;
    }

    public async Task<Poll> DeleteAsync(Poll poll, bool permanent = false)
    {
        Poll deletedPoll = await _pollRepository.DeleteAsync(poll);

        return deletedPoll;
    }
}
