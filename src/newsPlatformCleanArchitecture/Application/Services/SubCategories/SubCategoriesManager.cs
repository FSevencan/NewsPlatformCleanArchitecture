using Application.Features.SubCategories.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SubCategories;

public class SubCategoriesManager : ISubCategoriesService
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly SubCategoryBusinessRules _subCategoryBusinessRules;

    public SubCategoriesManager(ISubCategoryRepository subCategoryRepository, SubCategoryBusinessRules subCategoryBusinessRules)
    {
        _subCategoryRepository = subCategoryRepository;
        _subCategoryBusinessRules = subCategoryBusinessRules;
    }

    public async Task<SubCategory?> GetAsync(
        Expression<Func<SubCategory, bool>> predicate,
        Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SubCategory? subCategory = await _subCategoryRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return subCategory;
    }

    public async Task<IPaginate<SubCategory>?> GetListAsync(
        Expression<Func<SubCategory, bool>>? predicate = null,
        Func<IQueryable<SubCategory>, IOrderedQueryable<SubCategory>>? orderBy = null,
        Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SubCategory> subCategoryList = await _subCategoryRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return subCategoryList;
    }

    public async Task<SubCategory> AddAsync(SubCategory subCategory)
    {
        SubCategory addedSubCategory = await _subCategoryRepository.AddAsync(subCategory);

        return addedSubCategory;
    }

    public async Task<SubCategory> UpdateAsync(SubCategory subCategory)
    {
        SubCategory updatedSubCategory = await _subCategoryRepository.UpdateAsync(subCategory);

        return updatedSubCategory;
    }

    public async Task<SubCategory> DeleteAsync(SubCategory subCategory, bool permanent = false)
    {
        SubCategory deletedSubCategory = await _subCategoryRepository.DeleteAsync(subCategory);

        return deletedSubCategory;
    }
}
