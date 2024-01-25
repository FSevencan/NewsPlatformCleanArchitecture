using Application.Features.ColumnArticles.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ColumnArticles;

public class ColumnArticlesManager : IColumnArticlesService
{
    private readonly IColumnArticleRepository _columnArticleRepository;
    private readonly ColumnArticleBusinessRules _columnArticleBusinessRules;

    public ColumnArticlesManager(IColumnArticleRepository columnArticleRepository, ColumnArticleBusinessRules columnArticleBusinessRules)
    {
        _columnArticleRepository = columnArticleRepository;
        _columnArticleBusinessRules = columnArticleBusinessRules;
    }

    public async Task<ColumnArticle?> GetAsync(
        Expression<Func<ColumnArticle, bool>> predicate,
        Func<IQueryable<ColumnArticle>, IIncludableQueryable<ColumnArticle, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ColumnArticle? columnArticle = await _columnArticleRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return columnArticle;
    }

    public async Task<IPaginate<ColumnArticle>?> GetListAsync(
        Expression<Func<ColumnArticle, bool>>? predicate = null,
        Func<IQueryable<ColumnArticle>, IOrderedQueryable<ColumnArticle>>? orderBy = null,
        Func<IQueryable<ColumnArticle>, IIncludableQueryable<ColumnArticle, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ColumnArticle> columnArticleList = await _columnArticleRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return columnArticleList;
    }

    public async Task<ColumnArticle> AddAsync(ColumnArticle columnArticle)
    {
        ColumnArticle addedColumnArticle = await _columnArticleRepository.AddAsync(columnArticle);

        return addedColumnArticle;
    }

    public async Task<ColumnArticle> UpdateAsync(ColumnArticle columnArticle)
    {
        ColumnArticle updatedColumnArticle = await _columnArticleRepository.UpdateAsync(columnArticle);

        return updatedColumnArticle;
    }

    public async Task<ColumnArticle> DeleteAsync(ColumnArticle columnArticle, bool permanent = false)
    {
        ColumnArticle deletedColumnArticle = await _columnArticleRepository.DeleteAsync(columnArticle);

        return deletedColumnArticle;
    }
}
