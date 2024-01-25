using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ArticleReactions;

public interface IArticleReactionsService
{
    Task<ArticleReaction?> GetAsync(
        Expression<Func<ArticleReaction, bool>> predicate,
        Func<IQueryable<ArticleReaction>, IIncludableQueryable<ArticleReaction, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ArticleReaction>?> GetListAsync(
        Expression<Func<ArticleReaction, bool>>? predicate = null,
        Func<IQueryable<ArticleReaction>, IOrderedQueryable<ArticleReaction>>? orderBy = null,
        Func<IQueryable<ArticleReaction>, IIncludableQueryable<ArticleReaction, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ArticleReaction> AddAsync(ArticleReaction articleReaction);
    Task<ArticleReaction> UpdateAsync(ArticleReaction articleReaction);
    Task<ArticleReaction> DeleteAsync(ArticleReaction articleReaction, bool permanent = false);
}
