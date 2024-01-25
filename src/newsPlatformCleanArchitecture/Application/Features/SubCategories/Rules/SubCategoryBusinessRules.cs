using Application.Features.SubCategories.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.SubCategories.Rules;

public class SubCategoryBusinessRules : BaseBusinessRules
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public SubCategoryBusinessRules(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public Task SubCategoryShouldExistWhenSelected(SubCategory? subCategory)
    {
        if (subCategory == null)
            throw new BusinessException(SubCategoriesBusinessMessages.SubCategoryNotExists);
        return Task.CompletedTask;
    }

    public async Task SubCategoryIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SubCategory? subCategory = await _subCategoryRepository.GetAsync(
            predicate: sc => sc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SubCategoryShouldExistWhenSelected(subCategory);
    }
}