using Application.Features.ArticleTags.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ArticleTags.Rules;

public class ArticleTagBusinessRules : BaseBusinessRules
{
    private readonly IArticleTagRepository _articleTagRepository;

    public ArticleTagBusinessRules(IArticleTagRepository articleTagRepository)
    {
        _articleTagRepository = articleTagRepository;
    }

    public Task ArticleTagShouldExistWhenSelected(ArticleTag? articleTag)
    {
        if (articleTag == null)
            throw new BusinessException(ArticleTagsBusinessMessages.ArticleTagNotExists);
        return Task.CompletedTask;
    }

    public async Task ArticleTagIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ArticleTag? articleTag = await _articleTagRepository.GetAsync(
            predicate: at => at.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ArticleTagShouldExistWhenSelected(articleTag);
    }
}