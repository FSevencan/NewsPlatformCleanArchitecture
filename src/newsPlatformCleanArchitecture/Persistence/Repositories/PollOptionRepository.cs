using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PollOptionRepository : EfRepositoryBase<PollOption, Guid, BaseDbContext>, IPollOptionRepository
{
    public PollOptionRepository(BaseDbContext context) : base(context)
    {
    }
}