using Application.Features.Articles.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Articles.Rules;

public class ArticleBusinessRules : BaseBusinessRules
{
    private readonly IArticleRepository _articleRepository;

    public ArticleBusinessRules(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public Task ArticleShouldExistWhenSelected(Article? article)
    {
        if (article == null)
            throw new BusinessException(ArticlesBusinessMessages.ArticleNotExists);
        return Task.CompletedTask;
    }

    public async Task ArticleIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Article? article = await _articleRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ArticleShouldExistWhenSelected(article);
    }
}