using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Polls;

public interface IPollsService
{
    Task<Poll?> GetAsync(
        Expression<Func<Poll, bool>> predicate,
        Func<IQueryable<Poll>, IIncludableQueryable<Poll, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Poll>?> GetListAsync(
        Expression<Func<Poll, bool>>? predicate = null,
        Func<IQueryable<Poll>, IOrderedQueryable<Poll>>? orderBy = null,
        Func<IQueryable<Poll>, IIncludableQueryable<Poll, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Poll> AddAsync(Poll poll);
    Task<Poll> UpdateAsync(Poll poll);
    Task<Poll> DeleteAsync(Poll poll, bool permanent = false);
}
