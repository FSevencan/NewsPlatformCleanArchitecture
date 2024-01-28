using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class PollRepository : EfRepositoryBase<Poll, Guid, BaseDbContext>, IPollRepository
{
    public PollRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<Poll?> GetLatestPollAsync(CancellationToken cancellationToken)
    {
        return await Context.Polls
            .OrderByDescending(p => p.CreatedDate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}