using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PollOptions;

public interface IPollOptionsService
{
    Task<PollOption?> GetAsync(
        Expression<Func<PollOption, bool>> predicate,
        Func<IQueryable<PollOption>, IIncludableQueryable<PollOption, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<PollOption>?> GetListAsync(
        Expression<Func<PollOption, bool>>? predicate = null,
        Func<IQueryable<PollOption>, IOrderedQueryable<PollOption>>? orderBy = null,
        Func<IQueryable<PollOption>, IIncludableQueryable<PollOption, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<PollOption> AddAsync(PollOption pollOption);
    Task<PollOption> UpdateAsync(PollOption pollOption);
    Task<PollOption> DeleteAsync(PollOption pollOption, bool permanent = false);
}
