using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Columnists;

public interface IColumnistsService
{
    Task<Columnist?> GetAsync(
        Expression<Func<Columnist, bool>> predicate,
        Func<IQueryable<Columnist>, IIncludableQueryable<Columnist, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Columnist>?> GetListAsync(
        Expression<Func<Columnist, bool>>? predicate = null,
        Func<IQueryable<Columnist>, IOrderedQueryable<Columnist>>? orderBy = null,
        Func<IQueryable<Columnist>, IIncludableQueryable<Columnist, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Columnist> AddAsync(Columnist columnist);
    Task<Columnist> UpdateAsync(Columnist columnist);
    Task<Columnist> DeleteAsync(Columnist columnist, bool permanent = false);
}
