using Application.Features.Articles.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Articles;

public class ArticlesManager : IArticlesService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ArticleBusinessRules _articleBusinessRules;

    public ArticlesManager(IArticleRepository articleRepository, ArticleBusinessRules articleBusinessRules)
    {
        _articleRepository = articleRepository;
        _articleBusinessRules = articleBusinessRules;
    }

    public async Task<Article?> GetAsync(
        Expression<Func<Article, bool>> predicate,
        Func<IQueryable<Article>, IIncludableQueryable<Article, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Article? article = await _articleRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return article;
    }

    public async Task<IPaginate<Article>?> GetListAsync(
        Expression<Func<Article, bool>>? predicate = null,
        Func<IQueryable<Article>, IOrderedQueryable<Article>>? orderBy = null,
        Func<IQueryable<Article>, IIncludableQueryable<Article, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Article> articleList = await _articleRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return articleList;
    }

    public async Task<Article> AddAsync(Article article)
    {
        Article addedArticle = await _articleRepository.AddAsync(article);

        return addedArticle;
    }

    public async Task<Article> UpdateAsync(Article article)
    {
        Article updatedArticle = await _articleRepository.UpdateAsync(article);

        return updatedArticle;
    }

    public async Task<Article> DeleteAsync(Article article, bool permanent = false)
    {
        Article deletedArticle = await _articleRepository.DeleteAsync(article);

        return deletedArticle;
    }
}
