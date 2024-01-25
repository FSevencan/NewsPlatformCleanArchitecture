using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Articles;

public interface IArticlesService
{
    Task<Article?> GetAsync(
        Expression<Func<Article, bool>> predicate,
        Func<IQueryable<Article>, IIncludableQueryable<Article, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Article>?> GetListAsync(
        Expression<Func<Article, bool>>? predicate = null,
        Func<IQueryable<Article>, IOrderedQueryable<Article>>? orderBy = null,
        Func<IQueryable<Article>, IIncludableQueryable<Article, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Article> AddAsync(Article article);
    Task<Article> UpdateAsync(Article article);
    Task<Article> DeleteAsync(Article article, bool permanent = false);
}
