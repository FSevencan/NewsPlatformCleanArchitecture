using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Subscribles;

public interface ISubscriblesService
{
    Task<Subscrible?> GetAsync(
        Expression<Func<Subscrible, bool>> predicate,
        Func<IQueryable<Subscrible>, IIncludableQueryable<Subscrible, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Subscrible>?> GetListAsync(
        Expression<Func<Subscrible, bool>>? predicate = null,
        Func<IQueryable<Subscrible>, IOrderedQueryable<Subscrible>>? orderBy = null,
        Func<IQueryable<Subscrible>, IIncludableQueryable<Subscrible, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Subscrible> AddAsync(Subscrible subscrible);
    Task<Subscrible> UpdateAsync(Subscrible subscrible);
    Task<Subscrible> DeleteAsync(Subscrible subscrible, bool permanent = false);
}
