using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface INewsVideoRepository : IAsyncRepository<NewsVideo, Guid>, IRepository<NewsVideo, Guid>
{
}