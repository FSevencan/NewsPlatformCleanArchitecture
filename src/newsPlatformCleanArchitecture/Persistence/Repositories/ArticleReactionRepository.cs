using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ArticleReactionRepository : EfRepositoryBase<ArticleReaction, Guid, BaseDbContext>, IArticleReactionRepository
{
    public ArticleReactionRepository(BaseDbContext context) : base(context)
    {
    }
}