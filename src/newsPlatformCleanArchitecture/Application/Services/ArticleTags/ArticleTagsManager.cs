using Application.Features.ArticleTags.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ArticleTags;

public class ArticleTagsManager : IArticleTagsService
{
    private readonly IArticleTagRepository _articleTagRepository;
    private readonly ArticleTagBusinessRules _articleTagBusinessRules;

    public ArticleTagsManager(IArticleTagRepository articleTagRepository, ArticleTagBusinessRules articleTagBusinessRules)
    {
        _articleTagRepository = articleTagRepository;
        _articleTagBusinessRules = articleTagBusinessRules;
    }

    public async Task<ArticleTag?> GetAsync(
        Expression<Func<ArticleTag, bool>> predicate,
        Func<IQueryable<ArticleTag>, IIncludableQueryable<ArticleTag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ArticleTag? articleTag = await _articleTagRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return articleTag;
    }

    public async Task<IPaginate<ArticleTag>?> GetListAsync(
        Expression<Func<ArticleTag, bool>>? predicate = null,
        Func<IQueryable<ArticleTag>, IOrderedQueryable<ArticleTag>>? orderBy = null,
        Func<IQueryable<ArticleTag>, IIncludableQueryable<ArticleTag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ArticleTag> articleTagList = await _articleTagRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return articleTagList;
    }

    public async Task<ArticleTag> AddAsync(ArticleTag articleTag)
    {
        ArticleTag addedArticleTag = await _articleTagRepository.AddAsync(articleTag);

        return addedArticleTag;
    }

    public async Task<ArticleTag> UpdateAsync(ArticleTag articleTag)
    {
        ArticleTag updatedArticleTag = await _articleTagRepository.UpdateAsync(articleTag);

        return updatedArticleTag;
    }

    public async Task<ArticleTag> DeleteAsync(ArticleTag articleTag, bool permanent = false)
    {
        ArticleTag deletedArticleTag = await _articleTagRepository.DeleteAsync(articleTag);

        return deletedArticleTag;
    }
}
