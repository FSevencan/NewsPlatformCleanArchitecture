using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SubCategories;

public interface ISubCategoriesService
{
    Task<SubCategory?> GetAsync(
        Expression<Func<SubCategory, bool>> predicate,
        Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SubCategory>?> GetListAsync(
        Expression<Func<SubCategory, bool>>? predicate = null,
        Func<IQueryable<SubCategory>, IOrderedQueryable<SubCategory>>? orderBy = null,
        Func<IQueryable<SubCategory>, IIncludableQueryable<SubCategory, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SubCategory> AddAsync(SubCategory subCategory);
    Task<SubCategory> UpdateAsync(SubCategory subCategory);
    Task<SubCategory> DeleteAsync(SubCategory subCategory, bool permanent = false);
}
