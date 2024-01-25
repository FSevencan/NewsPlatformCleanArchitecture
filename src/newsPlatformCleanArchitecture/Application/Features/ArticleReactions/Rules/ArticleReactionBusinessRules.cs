using Application.Features.ArticleReactions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ArticleReactions.Rules;

public class ArticleReactionBusinessRules : BaseBusinessRules
{
    private readonly IArticleReactionRepository _articleReactionRepository;

    public ArticleReactionBusinessRules(IArticleReactionRepository articleReactionRepository)
    {
        _articleReactionRepository = articleReactionRepository;
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
}