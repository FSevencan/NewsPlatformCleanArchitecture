using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PollVotes;

public interface IPollVotesService
{
    Task<PollVote?> GetAsync(
        Expression<Func<PollVote, bool>> predicate,
        Func<IQueryable<PollVote>, IIncludableQueryable<PollVote, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<PollVote>?> GetListAsync(
        Expression<Func<PollVote, bool>>? predicate = null,
        Func<IQueryable<PollVote>, IOrderedQueryable<PollVote>>? orderBy = null,
        Func<IQueryable<PollVote>, IIncludableQueryable<PollVote, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<PollVote> AddAsync(PollVote pollVote);
    Task<PollVote> UpdateAsync(PollVote pollVote);
    Task<PollVote> DeleteAsync(PollVote pollVote, bool permanent = false);
}
