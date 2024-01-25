using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PollVoteRepository : EfRepositoryBase<PollVote, Guid, BaseDbContext>, IPollVoteRepository
{
    public PollVoteRepository(BaseDbContext context) : base(context)
    {
    }
}