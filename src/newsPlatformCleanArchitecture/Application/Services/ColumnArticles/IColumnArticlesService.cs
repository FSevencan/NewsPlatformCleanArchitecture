using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ColumnArticles;

public interface IColumnArticlesService
{
    Task<ColumnArticle?> GetAsync(
        Expression<Func<ColumnArticle, bool>> predicate,
        Func<IQueryable<ColumnArticle>, IIncludableQueryable<ColumnArticle, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ColumnArticle>?> GetListAsync(
        Expression<Func<ColumnArticle, bool>>? predicate = null,
        Func<IQueryable<ColumnArticle>, IOrderedQueryable<ColumnArticle>>? orderBy = null,
        Func<IQueryable<ColumnArticle>, IIncludableQueryable<ColumnArticle, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ColumnArticle> AddAsync(ColumnArticle columnArticle);
    Task<ColumnArticle> UpdateAsync(ColumnArticle columnArticle);
    Task<ColumnArticle> DeleteAsync(ColumnArticle columnArticle, bool permanent = false);
}
