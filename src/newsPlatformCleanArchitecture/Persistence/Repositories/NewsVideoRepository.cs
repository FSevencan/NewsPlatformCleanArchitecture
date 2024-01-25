using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class NewsVideoRepository : EfRepositoryBase<NewsVideo, Guid, BaseDbContext>, INewsVideoRepository
{
    public NewsVideoRepository(BaseDbContext context) : base(context)
    {
    }
}