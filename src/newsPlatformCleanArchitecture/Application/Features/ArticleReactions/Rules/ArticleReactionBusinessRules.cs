using Application.Features.ArticleReactions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ArticleReactions.Rules;

public class ArticleReactionBusinessRules : BaseBusinessRules
{
    private readonly IArticleReactionRepository _articleReactionRepository;
    private readonly IArticleRepository _articleRepository;

    public ArticleReactionBusinessRules(IArticleReactionRepository articleReactionRepository, IArticleRepository articleRepository)
    {
        _articleReactionRepository = articleReactionRepository;
        _articleRepository = articleRepository;
    }

    public Task ArticleReactionShouldExistWhenSelected(ArticleReaction? articleReaction)
    {
        if (articleReaction == null)
            throw new BusinessException(ArticleReactionsBusinessMessages.ArticleReactionNotExists);
        return Task.CompletedTask;
    }

    public async Task ArticleReactionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ArticleReaction? articleReaction = await _articleReactionRepository.GetAsync(
            predicate: ar => ar.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ArticleReactionShouldExistWhenSelected(articleReaction);
    }

    // Mevcut tepkinin olmamasýný saðla
    public async Task ValidateReactionDoesNotExist(Guid articleId, string voterIdentifier, CancellationToken cancellationToken)
    {
        var existingReaction = await _articleReactionRepository.GetAsync(
            ar => ar.ArticleId == articleId && ar.VoterIdentifier == voterIdentifier,
            cancellationToken: cancellationToken
        );
        if (existingReaction != null)
            throw new BusinessException("Kullanýcý bu habere zaten tepki vermiþ.");
    }

    // Makale tepki sayýlarýný güncelle
    public async Task UpdateArticleReactionCounts(Guid articleId, bool isLiked, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetAsync(a => a.Id == articleId, cancellationToken: cancellationToken);
        if (article == null)
            throw new BusinessException("Haber bulunamadý.");

        if (isLiked)
        {
            article.TotalLikes = (article.TotalLikes ?? 0) + 1;
        }
        else
        {
            article.TotalDislikes = (article.TotalDislikes ?? 0) + 1;
        }

        await _articleRepository.UpdateAsync(article);
    }

    // Makale tepkisi silindiðinde beðeni/beðenmeme sayýlarýný günceller.
    public async Task UpdateArticleReactionCountsOnDelete(Guid articleId, bool isLiked, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetAsync(a => a.Id == articleId, cancellationToken: cancellationToken);
        if (article == null)
            throw new BusinessException("Haber bulunamadý.");

        if (isLiked && article.TotalLikes > 0)
        {
            article.TotalLikes -= 1;
        }
        else if (!isLiked && article.TotalDislikes > 0)
        {
            article.TotalDislikes -= 1;
        }

        await _articleRepository.UpdateAsync(article);
    }

   // Makale tepkisi deðiþtirildiðinde beðeni/beðenmeme sayýlarýný günceller.
    public async Task UpdateArticleReactionCountsOnUpdate(Article article, ArticleReaction articleReaction, bool isLiked, CancellationToken cancellationToken)
    {
        if (articleReaction.IsLiked != isLiked)
        {
            if (isLiked)
            {
                article.TotalLikes = (article.TotalLikes ?? 0) + 1;
                article.TotalDislikes = article.TotalDislikes > 0 ? article.TotalDislikes - 1 : 0;
            }
            else
            {
                article.TotalDislikes = (article.TotalDislikes ?? 0) + 1;
                article.TotalLikes = article.TotalLikes > 0 ? article.TotalLikes - 1 : 0;
            }
        }

        await _articleRepository.UpdateAsync(article);
    }

}