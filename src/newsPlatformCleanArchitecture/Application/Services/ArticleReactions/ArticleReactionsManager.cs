using Application.Features.ArticleReactions.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ArticleReactions;

public class ArticleReactionsManager : IArticleReactionsService
{
    private readonly IArticleReactionRepository _articleReactionRepository;
    private readonly ArticleReactionBusinessRules _articleReactionBusinessRules;

    public ArticleReactionsManager(IArticleReactionRepository articleReactionRepository, ArticleReactionBusinessRules articleReactionBusinessRules)
    {
        _articleReactionRepository = articleReactionRepository;
        _articleReactionBusinessRules = articleReactionBusinessRules;
    }

    public async Task<ArticleReaction?> GetAsync(
        Expression<Func<ArticleReaction, bool>> predicate,
        Func<IQueryable<ArticleReaction>, IIncludableQueryable<ArticleReaction, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ArticleReaction? articleReaction = await _articleReactionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return articleReaction;
    }

    public async Task<IPaginate<ArticleReaction>?> GetListAsync(
        Expression<Func<ArticleReaction, bool>>? predicate = null,
        Func<IQueryable<ArticleReaction>, IOrderedQueryable<ArticleReaction>>? orderBy = null,
        Func<IQueryable<ArticleReaction>, IIncludableQueryable<ArticleReaction, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ArticleReaction> articleReactionList = await _articleReactionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return articleReactionList;
    }

    public async Task<ArticleReaction> AddAsync(ArticleReaction articleReaction)
    {
        ArticleReaction addedArticleReaction = await _articleReactionRepository.AddAsync(articleReaction);

        return addedArticleReaction;
    }

    public async Task<ArticleReaction> UpdateAsync(ArticleReaction articleReaction)
    {
        ArticleReaction updatedArticleReaction = await _articleReactionRepository.UpdateAsync(articleReaction);

        return updatedArticleReaction;
    }

    public async Task<ArticleReaction> DeleteAsync(ArticleReaction articleReaction, bool permanent = false)
    {
        ArticleReaction deletedArticleReaction = await _articleReactionRepository.DeleteAsync(articleReaction);

        return deletedArticleReaction;
    }
}
