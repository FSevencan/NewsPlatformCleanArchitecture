using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SubscribleRepository : EfRepositoryBase<Subscrible, Guid, BaseDbContext>, ISubscribleRepository
{
    public SubscribleRepository(BaseDbContext context) : base(context)
    {
    }
}