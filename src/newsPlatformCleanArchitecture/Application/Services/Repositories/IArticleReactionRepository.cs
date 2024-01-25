using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IArticleReactionRepository : IAsyncRepository<ArticleReaction, Guid>, IRepository<ArticleReaction, Guid>
{
}