using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IArticleRepository : IAsyncRepository<Article, Guid>, IRepository<Article, Guid>
{
}