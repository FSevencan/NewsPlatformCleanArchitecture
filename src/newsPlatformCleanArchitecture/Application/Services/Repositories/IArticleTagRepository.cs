using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IArticleTagRepository : IAsyncRepository<ArticleTag, Guid>, IRepository<ArticleTag, Guid>
{
}