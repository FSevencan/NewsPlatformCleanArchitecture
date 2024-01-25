using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PollRepository : EfRepositoryBase<Poll, Guid, BaseDbContext>, IPollRepository
{
    public PollRepository(BaseDbContext context) : base(context)
    {
    }
}