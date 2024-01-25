using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AdvertisementRepository : EfRepositoryBase<Advertisement, Guid, BaseDbContext>, IAdvertisementRepository
{
    public AdvertisementRepository(BaseDbContext context) : base(context)
    {
    }
}