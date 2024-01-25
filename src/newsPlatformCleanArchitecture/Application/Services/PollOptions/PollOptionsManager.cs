using Application.Features.PollOptions.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PollOptions;

public class PollOptionsManager : IPollOptionsService
{
    private readonly IPollOptionRepository _pollOptionRepository;
    private readonly PollOptionBusinessRules _pollOptionBusinessRules;

    public PollOptionsManager(IPollOptionRepository pollOptionRepository, PollOptionBusinessRules pollOptionBusinessRules)
    {
        _pollOptionRepository = pollOptionRepository;
        _pollOptionBusinessRules = pollOptionBusinessRules;
    }

    public async Task<PollOption?> GetAsync(
        Expression<Func<PollOption, bool>> predicate,
        Func<IQueryable<PollOption>, IIncludableQueryable<PollOption, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        PollOption? pollOption = await _pollOptionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return pollOption;
    }

    public async Task<IPaginate<PollOption>?> GetListAsync(
        Expression<Func<PollOption, bool>>? predicate = null,
        Func<IQueryable<PollOption>, IOrderedQueryable<PollOption>>? orderBy = null,
        Func<IQueryable<PollOption>, IIncludableQueryable<PollOption, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<PollOption> pollOptionList = await _pollOptionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return pollOptionList;
    }

    public async Task<PollOption> AddAsync(PollOption pollOption)
    {
        PollOption addedPollOption = await _pollOptionRepository.AddAsync(pollOption);

        return addedPollOption;
    }

    public async Task<PollOption> UpdateAsync(PollOption pollOption)
    {
        PollOption updatedPollOption = await _pollOptionRepository.UpdateAsync(pollOption);

        return updatedPollOption;
    }

    public async Task<PollOption> DeleteAsync(PollOption pollOption, bool permanent = false)
    {
        PollOption deletedPollOption = await _pollOptionRepository.DeleteAsync(pollOption);

        return deletedPollOption;
    }
}
