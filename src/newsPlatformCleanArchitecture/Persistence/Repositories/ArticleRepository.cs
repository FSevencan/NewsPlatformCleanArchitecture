using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ArticleRepository : EfRepositoryBase<Article, Guid, BaseDbContext>, IArticleRepository
{
    public ArticleRepository(BaseDbContext context) : base(context)
    {
    }
}