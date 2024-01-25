using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ArticleTags;

public interface IArticleTagsService
{
    Task<ArticleTag?> GetAsync(
        Expression<Func<ArticleTag, bool>> predicate,
        Func<IQueryable<ArticleTag>, IIncludableQueryable<ArticleTag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ArticleTag>?> GetListAsync(
        Expression<Func<ArticleTag, bool>>? predicate = null,
        Func<IQueryable<ArticleTag>, IOrderedQueryable<ArticleTag>>? orderBy = null,
        Func<IQueryable<ArticleTag>, IIncludableQueryable<ArticleTag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ArticleTag> AddAsync(ArticleTag articleTag);
    Task<ArticleTag> UpdateAsync(ArticleTag articleTag);
    Task<ArticleTag> DeleteAsync(ArticleTag articleTag, bool permanent = false);
}
