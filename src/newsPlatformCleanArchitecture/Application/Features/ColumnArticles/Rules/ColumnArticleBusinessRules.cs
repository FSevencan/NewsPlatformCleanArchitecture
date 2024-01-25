using Application.Features.ColumnArticles.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ColumnArticles.Rules;

public class ColumnArticleBusinessRules : BaseBusinessRules
{
    private readonly IColumnArticleRepository _columnArticleRepository;

    public ColumnArticleBusinessRules(IColumnArticleRepository columnArticleRepository)
    {
        _columnArticleRepository = columnArticleRepository;
    }

    public Task ColumnArticleShouldExistWhenSelected(ColumnArticle? columnArticle)
    {
        if (columnArticle == null)
            throw new BusinessException(ColumnArticlesBusinessMessages.ColumnArticleNotExists);
        return Task.CompletedTask;
    }

    public async Task ColumnArticleIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ColumnArticle? columnArticle = await _columnArticleRepository.GetAsync(
            predicate: ca => ca.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ColumnArticleShouldExistWhenSelected(columnArticle);
    }
}