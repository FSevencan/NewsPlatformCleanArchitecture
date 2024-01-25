using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ArticleTagRepository : EfRepositoryBase<ArticleTag, Guid, BaseDbContext>, IArticleTagRepository
{
    public ArticleTagRepository(BaseDbContext context) : base(context)
    {
    }
}